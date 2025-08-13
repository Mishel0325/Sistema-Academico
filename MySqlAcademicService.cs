using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Sistema_Academico
{
    public class MySqlAcademicService
    {
        // ======================== CONEXIÓN ========================
        private readonly string _cs =
            "Server=127.0.0.1;Port=3306;Database=sistema_academico;Uid=root;Pwd=;SslMode=None;";

        public MySqlAcademicService() { }
        public MySqlAcademicService(string connectionString) => _cs = connectionString;

        private async Task<MySqlConnection> AbrirConexionAsync()
        {
            var cn = new MySqlConnection(_cs);
            await cn.OpenAsync();
            return cn;
        }

        // ======================== UTILIDADES ======================
        private static async Task<bool> ColumnaExisteAsync(MySqlConnection cn, string tabla, string columna)
        {
            const string sql = @"
                SELECT 1
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = DATABASE()
                  AND TABLE_NAME = @t
                  AND COLUMN_NAME = @c
                LIMIT 1;";
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@t", tabla);
            cmd.Parameters.AddWithValue("@c", columna);
            return (await cmd.ExecuteScalarAsync()) != null;
        }

        public static string GenerarPasswordTemporal(int length = 8)
        {
            const string alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789@$#";
            using var rng = RandomNumberGenerator.Create();
            var data = new byte[length];
            rng.GetBytes(data);
            var sb = new StringBuilder(length);
            foreach (var b in data) sb.Append(alphabet[b % alphabet.Length]);
            return sb.ToString();
        }

        // ========================== LOGIN =========================
        /// <summary>
        /// Login “simple” (compatibilidad). Intenta con hash y, si encuentra
        /// la contraseña en claro, la migra automáticamente a SHA-256.
        /// </summary>
        public async Task<(int id, string nombre, int rolId, string rolNombre)?> LoginAsync(string usuario, string contraseña)
        {
            using var cn = await AbrirConexionAsync();

            // 1) Intento con HASH (UPPER(SHA2))
            const string q1 = @"
                SELECT u.id, CONCAT(u.nombres,' ',u.apellidos) nombre, r.id rolId, r.nombre rolNombre
                FROM usuarios u
                JOIN roles r ON r.id = u.rol_id
                WHERE (u.correo=@u OR u.cedula=@u)
                  AND u.estado=1
                  AND u.contraseña = UPPER(SHA2(@p,256))
                LIMIT 1;";
            using (var cmd = new MySqlCommand(q1, cn))
            {
                cmd.Parameters.AddWithValue("@u", usuario.Trim());
                cmd.Parameters.AddWithValue("@p", contraseña);
                using var rd = await cmd.ExecuteReaderAsync();
                if (await rd.ReadAsync())
                    return (rd.GetInt32("id"), rd.GetString("nombre"), rd.GetInt32("rolId"), rd.GetString("rolNombre"));
            }

            // 2) Intento legacy en claro: si coincide, migro a SHA-256
            const string q2 = @"
                SELECT u.id, CONCAT(u.nombres,' ',u.apellidos) nombre, r.id rolId, r.nombre rolNombre
                FROM usuarios u
                JOIN roles r ON r.id = u.rol_id
                WHERE (u.correo=@u OR u.cedula=@u)
                  AND u.estado=1
                  AND u.contraseña = @p
                LIMIT 1;";
            using (var cmd = new MySqlCommand(q2, cn))
            {
                cmd.Parameters.AddWithValue("@u", usuario.Trim());
                cmd.Parameters.AddWithValue("@p", contraseña);
                using var rd = await cmd.ExecuteReaderAsync();
                if (await rd.ReadAsync())
                {
                    int id = rd.GetInt32("id");
                    string nombre = rd.GetString("nombre");
                    int rolId = rd.GetInt32("rolId");
                    string rolNombre = rd.GetString("rolNombre");
                    rd.Close();

                    using var up = new MySqlCommand("UPDATE usuarios SET contraseña=UPPER(SHA2(@p,256)) WHERE id=@id;", cn);
                    up.Parameters.AddWithValue("@p", contraseña);
                    up.Parameters.AddWithValue("@id", id);
                    await up.ExecuteNonQueryAsync();

                    return (id, nombre, rolId, rolNombre);
                }
            }

            return null;
        }

        /// <summary>
        /// Login que además devuelve si debe cambiar la contraseña (mustChange).
        /// Funciona aunque la columna must_change no exista (devuelve false).
        /// </summary>
        public async Task<(int id, string nombre, int rolId, string rolNombre, bool mustChange)?>
            LoginWithMustChangeAsync(string usuario, string contraseña)
        {
            using var cn = await AbrirConexionAsync();
            bool tieneMust = await ColumnaExisteAsync(cn, "usuarios", "must_change");

            var sql = new StringBuilder();
            sql.Append(@"
                SELECT u.id,
                       CONCAT(u.nombres,' ',u.apellidos) AS nombre,
                       r.id AS rolId,
                       r.nombre AS rolNombre");
            sql.Append(tieneMust ? ", u.must_change" : ", 0 AS must_change");
            sql.Append(@"
                FROM usuarios u
                JOIN roles r ON r.id = u.rol_id
                WHERE (u.correo = @u OR u.cedula = @u)
                  AND (
                        u.contraseña = UPPER(SHA2(@p,256))  -- hash
                     OR u.contraseña = @p                  -- legado
                     OR u.contraseña = MD5(@p)             -- legado MD5
                  )
                  AND (u.estado = 1 OR u.estado IS NULL)
                LIMIT 1;");

            using var cmd = new MySqlCommand(sql.ToString(), cn);
            cmd.Parameters.AddWithValue("@u", usuario.Trim());
            cmd.Parameters.AddWithValue("@p", contraseña);

            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                return (rd.GetInt32("id"),
                        rd.GetString("nombre"),
                        rd.GetInt32("rolId"),
                        rd.GetString("rolNombre"),
                        rd.GetBoolean("must_change"));
            }
            return null;
        }

        // ==================== USUARIOS (ADMIN) ====================
        public async Task<List<(int id, string nombre)>> GetRolesAsync()
        {
            const string sql = "SELECT id, nombre FROM roles ORDER BY id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            using var rd = await cmd.ExecuteReaderAsync();
            var list = new List<(int, string)>();
            while (await rd.ReadAsync()) list.Add((rd.GetInt32("id"), rd.GetString("nombre")));
            return list;
        }

        public async Task<DataTable> BuscarUsuariosAsync(string? texto)
        {
            const string sql = @"
                SELECT u.id, u.cedula, u.nombres, u.apellidos, u.correo,
                       r.nombre AS rol, u.rol_id, u.estado, u.fecha_creacion
                FROM usuarios u
                JOIN roles r ON r.id = u.rol_id
                WHERE (@t IS NULL OR @t = '' 
                       OR u.cedula    LIKE CONCAT('%', @t, '%') 
                       OR u.nombres   LIKE CONCAT('%', @t, '%') 
                       OR u.apellidos LIKE CONCAT('%', @t, '%') 
                       OR u.correo    LIKE CONCAT('%', @t, '%'))
                ORDER BY u.id DESC;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@t", texto ?? "");
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        /// <summary>
        /// Inserta usuario con contraseña temporal (hash) y must_change=1 si existe.
        /// Devuelve (id, contraseñaTemporal).
        /// </summary>
        public async Task<(int id, string tempPass)> InsertUsuarioConTempAsync(
            string cedula, string nombres, string apellidos, string correo, int rolId, bool activo)
        {
            using var cn = await AbrirConexionAsync();
            bool tieneMust = await ColumnaExisteAsync(cn, "usuarios", "must_change");

            var temp = GenerarPasswordTemporal();

            string sql = tieneMust
                ? @"INSERT INTO usuarios (cedula, nombres, apellidos, correo, contraseña, must_change, rol_id, estado, fecha_creacion)
                    VALUES (@ced,@nom,@ape,@cor,UPPER(SHA2(@pass,256)),1,@rol,@est,NOW());
                    SELECT LAST_INSERT_ID();"
                : @"INSERT INTO usuarios (cedula, nombres, apellidos, correo, contraseña, rol_id, estado, fecha_creacion)
                    VALUES (@ced,@nom,@ape,@cor,UPPER(SHA2(@pass,256)),@rol,@est,NOW());
                    SELECT LAST_INSERT_ID();";

            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ced", cedula.Trim());
            cmd.Parameters.AddWithValue("@nom", nombres.Trim());
            cmd.Parameters.AddWithValue("@ape", apellidos.Trim());
            cmd.Parameters.AddWithValue("@cor", correo.Trim());
            cmd.Parameters.AddWithValue("@pass", temp);
            cmd.Parameters.AddWithValue("@rol", rolId);
            cmd.Parameters.AddWithValue("@est", activo ? 1 : 0);
            var id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return (id, temp);
        }

        /// <summary>
        /// Inserta usuario con contraseña provista (hash). Puedes marcar must_change si existe la columna.
        /// </summary>
        public async Task<int> InsertUsuarioAsync(
            string cedula, string nombres, string apellidos, string correo,
            string passwordPlano, int rolId, bool activo, bool mustChange = false)
        {
            using var cn = await AbrirConexionAsync();
            bool tieneMust = await ColumnaExisteAsync(cn, "usuarios", "must_change");

            string sql = tieneMust
                ? @"INSERT INTO usuarios (cedula, nombres, apellidos, correo, contraseña, must_change, rol_id, estado, fecha_creacion)
                    VALUES (@ced,@nom,@ape,@cor,UPPER(SHA2(@pass,256)),@must,@rol,@est,NOW());
                    SELECT LAST_INSERT_ID();"
                : @"INSERT INTO usuarios (cedula, nombres, apellidos, correo, contraseña, rol_id, estado, fecha_creacion)
                    VALUES (@ced,@nom,@ape,@cor,UPPER(SHA2(@pass,256)),@rol,@est,NOW());
                    SELECT LAST_INSERT_ID();";

            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ced", cedula);
            cmd.Parameters.AddWithValue("@nom", nombres);
            cmd.Parameters.AddWithValue("@ape", apellidos);
            cmd.Parameters.AddWithValue("@cor", correo);
            cmd.Parameters.AddWithValue("@pass", passwordPlano);
            if (tieneMust) cmd.Parameters.AddWithValue("@must", mustChange ? 1 : 0);
            cmd.Parameters.AddWithValue("@rol", rolId);
            cmd.Parameters.AddWithValue("@est", activo ? 1 : 0);
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task UpdateUsuarioAsync(int id, string cedula, string nombres, string apellidos, string correo, int rolId, bool activo)
        {
            const string sql = @"
                UPDATE usuarios 
                   SET cedula=@ced, nombres=@nom, apellidos=@ape, correo=@cor, rol_id=@rol, estado=@est
                 WHERE id=@id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ced", cedula);
            cmd.Parameters.AddWithValue("@nom", nombres);
            cmd.Parameters.AddWithValue("@ape", apellidos);
            cmd.Parameters.AddWithValue("@cor", correo);
            cmd.Parameters.AddWithValue("@rol", rolId);
            cmd.Parameters.AddWithValue("@est", activo ? 1 : 0);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            const string sql = "DELETE FROM usuarios WHERE id=@id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Resetea a una contraseña temporal (hash). Si existen columnas, marca must_change=1 y setea pass_updated_at.
        /// Devuelve la nueva contraseña temporal.
        /// </summary>
        public async Task<string> ResetPasswordTemporalAsync(int id)
        {
            using var cn = await AbrirConexionAsync();
            bool tieneMust = await ColumnaExisteAsync(cn, "usuarios", "must_change");
            bool tieneUpd = await ColumnaExisteAsync(cn, "usuarios", "pass_updated_at");

            var temp = GenerarPasswordTemporal();

            var sb = new StringBuilder("UPDATE usuarios SET contraseña = UPPER(SHA2(@p,256))");
            if (tieneMust) sb.Append(", must_change = 1");
            if (tieneUpd) sb.Append(", pass_updated_at = NOW()");
            sb.Append(" WHERE id=@id;");

            using var cmd = new MySqlCommand(sb.ToString(), cn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@p", temp);
            await cmd.ExecuteNonQueryAsync();
            return temp;
        }

        /// <summary>
        /// Resetea a un valor dado (hash). Por defecto forzará cambio si existe must_change.
        /// </summary>
        public async Task ResetPasswordAsync(int id, string nuevo = "123456", bool mustChange = true)
        {
            using var cn = await AbrirConexionAsync();
            bool tieneMust = await ColumnaExisteAsync(cn, "usuarios", "must_change");
            bool tieneUpd = await ColumnaExisteAsync(cn, "usuarios", "pass_updated_at");

            var sb = new StringBuilder("UPDATE usuarios SET contraseña = UPPER(SHA2(@p,256))");
            if (tieneMust) sb.Append(", must_change = @m");
            if (tieneUpd) sb.Append(", pass_updated_at = NOW()");
            sb.Append(" WHERE id=@id;");

            using var cmd = new MySqlCommand(sb.ToString(), cn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@p", nuevo);
            if (tieneMust) cmd.Parameters.AddWithValue("@m", mustChange ? 1 : 0);
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Cambia la contraseña validando la actual (acepta claro/MD5/SHA2).
        /// Guarda la nueva con SHA-256 y apaga must_change si existe.
        /// </summary>
        public async Task<bool> CambiarPasswordAsync(int usuarioId, string passActual, string passNueva)
        {
            using var cn = await AbrirConexionAsync();

            const string sel = @"
                SELECT 1 FROM usuarios 
                 WHERE id=@id AND (
                       contraseña = @a
                    OR contraseña = UPPER(SHA2(@a,256))
                    OR contraseña = MD5(@a)
                 ) LIMIT 1;";
            using (var v = new MySqlCommand(sel, cn))
            {
                v.Parameters.AddWithValue("@id", usuarioId);
                v.Parameters.AddWithValue("@a", passActual);
                if (await v.ExecuteScalarAsync() == null) return false;
            }

            bool tieneMust = await ColumnaExisteAsync(cn, "usuarios", "must_change");
            bool tieneUpd = await ColumnaExisteAsync(cn, "usuarios", "pass_updated_at");

            var sb = new StringBuilder("UPDATE usuarios SET contraseña = UPPER(SHA2(@n,256))");
            if (tieneMust) sb.Append(", must_change = 0");
            if (tieneUpd) sb.Append(", pass_updated_at = NOW()");
            sb.Append(" WHERE id=@id;");

            using var cmd = new MySqlCommand(sb.ToString(), cn);
            cmd.Parameters.AddWithValue("@id", usuarioId);
            cmd.Parameters.AddWithValue("@n", passNueva);
            await cmd.ExecuteNonQueryAsync();
            return true;
        }

        // ========================== DOCENTE =======================
        public async Task<List<(int id, string nombre)>> GetParcialesAsync()
        {
            const string sql = "SELECT id, nombre FROM parciales ORDER BY id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            using var rd = await cmd.ExecuteReaderAsync();
            var list = new List<(int, string)>();
            while (await rd.ReadAsync()) list.Add((rd.GetInt32("id"), rd.GetString("nombre")));
            return list;
        }

        public async Task<List<(int id, string nombre)>> GetEstadosAsistenciaAsync()
        {
            const string sql = "SELECT id, nombre FROM estado_asistencia ORDER BY id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            using var rd = await cmd.ExecuteReaderAsync();
            var list = new List<(int, string)>();
            while (await rd.ReadAsync()) list.Add((rd.GetInt32("id"), rd.GetString("nombre")));
            return list;
        }

        public async Task<List<(int id, string nombre)>> GetAsignaturasDocenteAsync(int docenteId)
        {
            const string sql = @"
                SELECT a.id, a.nombre
                  FROM horarios h
                  JOIN asignaturas a ON a.id = h.asignatura_id
                 WHERE h.docente_id = @docenteId
              GROUP BY a.id, a.nombre
              ORDER BY a.nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@docenteId", docenteId);
            using var rd = await cmd.ExecuteReaderAsync();
            var list = new List<(int, string)>();
            while (await rd.ReadAsync()) list.Add((rd.GetInt32("id"), rd.GetString("nombre")));
            return list;
        }

        public async Task<List<(int id, string nombre)>> GetEstudiantesPorAsignaturaAsync(int asignaturaId)
        {
            const string sql = @"
                SELECT u.id, CONCAT(u.nombres,' ',u.apellidos) AS nombre
                  FROM matriculas m
                  JOIN usuarios u ON u.id = m.estudiante_id
                 WHERE m.asignatura_id = @asignaturaId
              ORDER BY nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@asignaturaId", asignaturaId);
            using var rd = await cmd.ExecuteReaderAsync();
            var list = new List<(int, string)>();
            while (await rd.ReadAsync()) list.Add((rd.GetInt32("id"), rd.GetString("nombre")));
            return list;
        }

        public async Task GuardarNotaAsync(int estudianteId, int asignaturaId, int docenteId, int parcialId, decimal nota)
        {
            const string sql = @"
                INSERT INTO calificaciones (estudiante_id, asignatura_id, docente_id, parcial_id, nota)
                VALUES (@estudianteId, @asignaturaId, @docenteId, @parcialId, @nota);";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@estudianteId", estudianteId);
            cmd.Parameters.AddWithValue("@asignaturaId", asignaturaId);
            cmd.Parameters.AddWithValue("@docenteId", docenteId);
            cmd.Parameters.AddWithValue("@parcialId", parcialId);
            cmd.Parameters.AddWithValue("@nota", nota);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task RegistrarAsistenciaAsync(int estudianteId, int asignaturaId, int docenteId, DateTime fecha, int estadoId)
        {
            const string sql = @"
                INSERT INTO asistencias (estudiante_id, asignatura_id, docente_id, fecha, estado_id)
                VALUES (@estudianteId, @asignaturaId, @docenteId, @fecha, @estadoId);";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@estudianteId", estudianteId);
            cmd.Parameters.AddWithValue("@asignaturaId", asignaturaId);
            cmd.Parameters.AddWithValue("@docenteId", docenteId);
            cmd.Parameters.AddWithValue("@fecha", fecha.Date);
            cmd.Parameters.AddWithValue("@estadoId", estadoId);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task AsignarTareaAsync(int asignaturaId, int docenteId, string titulo, string descripcion, DateTime fechaEntrega)
        {
            const string sql = @"
                INSERT INTO tareas (asignatura_id, docente_id, titulo, descripcion, fecha_entrega)
                VALUES (@asignaturaId, @docenteId, @titulo, @descripcion, @fechaEntrega);";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@asignaturaId", asignaturaId);
            cmd.Parameters.AddWithValue("@docenteId", docenteId);
            cmd.Parameters.AddWithValue("@titulo", titulo);
            cmd.Parameters.AddWithValue("@descripcion", descripcion);
            cmd.Parameters.AddWithValue("@fechaEntrega", fechaEntrega.Date);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> CountCertificadosAsync(int estudianteId)
        {
            const string sql = "SELECT COUNT(*) FROM certificados WHERE estudiante_id=@id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", estudianteId);
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        // ========================= ESTUDIANTE =====================
        public async Task<List<(int id, string nombre)>> GetAsignaturasEstudianteAsync(int estudianteId)
        {
            const string sql = @"
                SELECT a.id, a.nombre
                  FROM matriculas m
                  JOIN asignaturas a ON a.id = m.asignatura_id
                 WHERE m.estudiante_id = @estudianteId
              GROUP BY a.id, a.nombre
              ORDER BY a.nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@estudianteId", estudianteId);
            using var rd = await cmd.ExecuteReaderAsync();
            var list = new List<(int, string)>();
            while (await rd.ReadAsync()) list.Add((rd.GetInt32("id"), rd.GetString("nombre")));
            return list;
        }

        public async Task<DataTable> GetNotasAsync(int estudianteId, int asignaturaId, int? parcialId)
        {
            var sql = @"
                SELECT p.nombre AS Parcial, c.nota AS Nota, c.fecha_registro AS Fecha
                  FROM calificaciones c
                  JOIN parciales p ON p.id = c.parcial_id
                 WHERE c.estudiante_id=@est AND c.asignatura_id=@asig";
            if (parcialId.HasValue) sql += " AND c.parcial_id=@parcial";
            sql += " ORDER BY p.id;";

            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@est", estudianteId);
            cmd.Parameters.AddWithValue("@asig", asignaturaId);
            if (parcialId.HasValue) cmd.Parameters.AddWithValue("@parcial", parcialId.Value);
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public async Task<decimal?> GetPromedioAsync(int estudianteId, int asignaturaId)
        {
            const string sql = @"SELECT AVG(nota) FROM calificaciones WHERE estudiante_id=@e AND asignatura_id=@a;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@e", estudianteId);
            cmd.Parameters.AddWithValue("@a", asignaturaId);
            var obj = await cmd.ExecuteScalarAsync();
            return obj == DBNull.Value ? (decimal?)null : Convert.ToDecimal(obj);
        }

        public async Task<DataTable> GetTareasAsync(int asignaturaId)
        {
            const string sql = @"
                SELECT t.titulo AS Titulo, t.descripcion AS Descripcion, t.fecha_asignacion AS Asignada, t.fecha_entrega AS Entrega
                  FROM tareas t
                 WHERE t.asignatura_id=@id
              ORDER BY t.fecha_asignacion DESC;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", asignaturaId);
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public async Task<List<(int id, string nombre)>> GetTiposCertificadosAsync()
        {
            const string sql = "SELECT id, nombre FROM tipos_certificados ORDER BY nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            using var rd = await cmd.ExecuteReaderAsync();
            var list = new List<(int, string)>();
            while (await rd.ReadAsync()) list.Add((rd.GetInt32("id"), rd.GetString("nombre")));
            return list;
        }

        public async Task<(byte[]? archivo, string sugerido)> DescargarCertificadoAsync(int estudianteId, int tipoCertId)
        {
            const string sql = @"
                SELECT archivo, DATE_FORMAT(fecha_generado,'%Y%m%d') AS f
                  FROM certificados
                 WHERE estudiante_id=@e AND tipo_certificado_id=@t
              ORDER BY fecha_generado DESC
                 LIMIT 1;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@e", estudianteId);
            cmd.Parameters.AddWithValue("@t", tipoCertId);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                var bytes = rd["archivo"] == DBNull.Value ? null : (byte[])rd["archivo"];
                var fecha = rd.GetString("f");
                var nombre = $"cert_{estudianteId}_{tipoCertId}_{fecha}.pdf";
                return (bytes, nombre);
            }
            return (null, $"cert_{estudianteId}_{tipoCertId}.pdf");
        }

        // ======================= ASIGNATURAS CRUD =================
        public async Task<DataTable> BuscarAsignaturasAsync(string? filtro)
        {
            const string sql = @"
                SELECT id, codigo, nombre, descripcion, estado
                  FROM asignaturas
                 WHERE (@f IS NULL OR @f = '' 
                        OR codigo LIKE CONCAT('%',@f,'%') 
                        OR nombre LIKE CONCAT('%',@f,'%'))
              ORDER BY nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@f", filtro ?? "");
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public async Task<int> InsertAsignaturaAsync(string codigo, string nombre, string descripcion, bool activa)
        {
            const string sql = @"
                INSERT INTO asignaturas (codigo, nombre, descripcion, estado)
                VALUES (@c,@n,@d,@e);
                SELECT LAST_INSERT_ID();";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@c", codigo);
            cmd.Parameters.AddWithValue("@n", nombre);
            cmd.Parameters.AddWithValue("@d", descripcion);
            cmd.Parameters.AddWithValue("@e", activa ? 1 : 0);
            var o = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(o);
        }

        public async Task UpdateAsignaturaAsync(int id, string codigo, string nombre, string descripcion, bool activa)
        {
            const string sql = @"
                UPDATE asignaturas
                   SET codigo=@c, nombre=@n, descripcion=@d, estado=@e
                 WHERE id=@id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@c", codigo);
            cmd.Parameters.AddWithValue("@n", nombre);
            cmd.Parameters.AddWithValue("@d", descripcion);
            cmd.Parameters.AddWithValue("@e", activa ? 1 : 0);
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsignaturaAsync(int id)
        {
            const string sql = "DELETE FROM asignaturas WHERE id=@id;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            await cmd.ExecuteNonQueryAsync();
        }

        // ================== ASIGNACIÓN / MATRÍCULA =================
        public async Task AsignarDocenteAAsignaturaAsync(int docenteId, int asignaturaId)
        {
            const string sql = @"
                INSERT INTO docente_asignatura (docente_id, asignatura_id)
                VALUES (@doc, @asig)
                ON DUPLICATE KEY UPDATE docente_id = VALUES(docente_id);";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@doc", docenteId);
            cmd.Parameters.AddWithValue("@asig", asignaturaId);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task MatricularEstudianteEnAsignaturaAsync(int estudianteId, int asignaturaId)
        {
            using var cn = await AbrirConexionAsync();

            bool tieneFecha = await ColumnaExisteAsync(cn, "matriculas", "fecha");
            bool tieneEstado = await ColumnaExisteAsync(cn, "matriculas", "estado");

            string sql = tieneFecha && tieneEstado
                ? @"INSERT INTO matriculas (estudiante_id, asignatura_id, fecha, estado)
                    VALUES (@est, @asig, NOW(), 1)
                    ON DUPLICATE KEY UPDATE fecha = NOW(), estado = 1;"
                : @"INSERT INTO matriculas (estudiante_id, asignatura_id)
                    VALUES (@est, @asig)
                    ON DUPLICATE KEY UPDATE asignatura_id = VALUES(asignatura_id);";

            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@est", estudianteId);
            cmd.Parameters.AddWithValue("@asig", asignaturaId);
            await cmd.ExecuteNonQueryAsync();
        }

        // ========================== REPORTES ======================
        public async Task<DataTable> ReportePromediosAsync(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT a.codigo, a.nombre, AVG(c.nota) AS promedio
                  FROM calificaciones c
                  INNER JOIN asignaturas a ON a.id = c.asignatura_id
                 WHERE c.fecha_registro BETWEEN @d1 AND @d2
              GROUP BY a.codigo, a.nombre
              ORDER BY a.nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@d1", desde);
            cmd.Parameters.AddWithValue("@d2", hasta);
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public async Task<DataTable> ReporteAsistenciasAsync(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT a.codigo, a.nombre,
                       SUM(CASE WHEN ea.nombre = 'Presente' THEN 1 ELSE 0 END) AS presentes,
                       COUNT(*) AS total
                  FROM asistencias asis
                  INNER JOIN asignaturas a ON a.id = asis.asignatura_id
                  INNER JOIN estado_asistencia ea ON ea.id = asis.estado_id
                 WHERE asis.fecha BETWEEN @d1 AND @d2
              GROUP BY a.codigo, a.nombre
              ORDER BY a.nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@d1", desde);
            cmd.Parameters.AddWithValue("@d2", hasta);
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        public async Task<DataTable> ReporteCalificacionesGlobalAsync(DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT u.cedula, u.nombres, u.apellidos,
                       a.codigo, a.nombre AS asignatura,
                       AVG(c.nota) AS promedio
                  FROM calificaciones c
                  INNER JOIN usuarios u ON u.id = c.estudiante_id
                  INNER JOIN asignaturas a ON a.id = c.asignatura_id
                 WHERE c.fecha_registro BETWEEN @d1 AND @d2
              GROUP BY u.cedula, u.nombres, u.apellidos, a.codigo, a.nombre
              ORDER BY u.apellidos, u.nombres, a.nombre;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@d1", desde);
            cmd.Parameters.AddWithValue("@d2", hasta);
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }

        // =========================== AUDITORÍA ====================
        public async Task<DataTable> AuditoriaAsync(string? usuarioTexto, string? accion, DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT id, usuario, accion, descripcion, fecha
                  FROM auditoria
                 WHERE (@usr IS NULL OR @usr = '' OR usuario LIKE CONCAT('%', @usr, '%'))
                   AND (@acc IS NULL OR @acc = '' OR accion = @acc)
                   AND fecha >= @d1 AND fecha < DATE_ADD(@d2, INTERVAL 1 DAY)
              ORDER BY fecha DESC;";
            using var cn = await AbrirConexionAsync();
            using var cmd = new MySqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@usr", usuarioTexto ?? "");
            cmd.Parameters.AddWithValue("@acc", accion ?? "");
            cmd.Parameters.AddWithValue("@d1", desde.Date);
            cmd.Parameters.AddWithValue("@d2", hasta.Date);
            using var ad = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
    }
}