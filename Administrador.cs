using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

// Asegúrate de tener esta referencia si usas MySQL
// using MySql.Data.MySqlClient;

namespace Sistema_Academico
{
    public partial class Administrador : Form
    {
        private readonly MySqlAcademicService _svc;
        private readonly int _userId;
        private readonly string _nombre;

        public Administrador(int userId, string nombre)
        {
            InitializeComponent();

            // ============ Reenlace de eventos (evitar duplicados) ============
            ReenlazarEventosUsuarios();
            ReenlazarEventosAsignaturas();
            ReenlazarEventosReportes();
            ReenlazarEventosAuditoria();

            _svc = new MySqlAcademicService();
            _userId = userId;
            _nombre = nombre;

            Text = string.IsNullOrWhiteSpace(_nombre) ? "Administrador" : $"Administrador – {_nombre}";
            var lbl = Controls.Find("lblStatus", true);
            if (lbl.Length > 0 && lbl[0] is Label l) l.Text = $"Sesión: {_nombre} (ID: {_userId})";

            this.Load += Administrador_Load;
        }

        public Administrador() : this(0, "Administrador") { }

        // ================== Load ==================
        private async void Administrador_Load(object? sender, EventArgs e)
        {
            try
            {
                await CargarCombos();
                dtDesde.Value = DateTime.Today.AddMonths(-1);
                dtHasta.Value = DateTime.Today;
                dtAudDesde.Value = DateTime.Today.AddMonths(-1);
                dtAudHasta.Value = DateTime.Today;

                await CargarUsuarios();
                await CargarAsignaturas();

                lblStatus.Text = "Listo";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar: " + ex.Message);
            }
        }

        // ================== DTO para combos ==================
        private class Rol
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = "";
            public override string ToString() => Nombre;
        }

        // ================== Métodos de enlace de eventos ==================
        private void ReenlazarEventosUsuarios()
        {
            btnUsrNuevo.Click += (s, e) => LimpiarUsuario();
            btnUsrGuardar.Click += btnUsrGuardar_Click;
            btnUsrActualizar.Click += btnUsrActualizar_Click;
            btnUsrEliminar.Click += btnUsrEliminar_Click;
            btnUsrResetPass.Click += btnUsrResetPass_Click;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            btnBuscarUsuario.Click += btnBuscarUsuario_Click;
        }

        private void ReenlazarEventosAsignaturas()
        {
            btnAsigNuevo.Click += (s, e) => LimpiarAsignatura();
            btnAsigGuardar.Click += btnAsigGuardar_Click;
            btnAsigActualizar.Click += btnAsigActualizar_Click;
            btnAsigEliminar.Click += btnAsigEliminar_Click;
            dgvAsignaturas.CellClick += dgvAsignaturas_CellClick;
            btnBuscarAsig.Click += btnBuscarAsig_Click;
        }

        private void ReenlazarEventosReportes()
        {
            btnGenerarReporte.Click += btnGenerarReporte_Click;
            btnExportarReporte.Click += btnExportarReporte_Click;
        }

        private void ReenlazarEventosAuditoria()
        {
            btnFiltrarAud.Click += btnFiltrarAud_Click;
            btnExportAud.Click += btnExportAud_Click;
        }

        // ================== Métodos de carga inicial ==================
        private async Task CargarCombos()
        {
            try
            {
                var rolesTupla = await _svc.GetRolesAsync();
                var roles = rolesTupla.ConvertAll(r => new Rol { Id = r.Item1, Nombre = r.Item2 });

                cbRol.DataSource = roles;
                cbRol.DisplayMember = "Nombre";
                cbRol.ValueMember = "Id";

                if (cbAccion.Items.Count > 0) cbAccion.SelectedIndex = 0;
                if (cbTipoReporte.Items.Count > 0) cbTipoReporte.SelectedIndex = 0;
            }
            catch
            {
                cbRol.DataSource = new List<Rol> { new Rol { Id = 1, Nombre = "Administrador" } };
            }
        }

        // ================== Utilidad para detectar duplicados ==================
        private static bool EsDuplicado(Exception ex)
        {
            if (ex.Message.Contains("Duplicate entry", StringComparison.OrdinalIgnoreCase))
                return true;
            return ex.InnerException != null && EsDuplicado(ex.InnerException);
        }

        // ================== USUARIOS ==================
        private async Task CargarUsuarios(string? filtro = null)
        {
            dgvUsuarios.DataSource = await _svc.BuscarUsuariosAsync(filtro);
        }

        private void LimpiarUsuario()
        {
            txtCedula.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtCorreo.Clear();
            if (cbRol.Items.Count > 0) cbRol.SelectedIndex = 0;
            chkEstadoUsuario.Checked = true;
            dgvUsuarios.ClearSelection();
            lblStatus.Text = "Formulario de usuario listo.";
        }

        private async void btnBuscarUsuario_Click(object sender, EventArgs e) => await CargarUsuarios(txtBuscarUsuario.Text.Trim());

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvUsuarios.Rows[e.RowIndex];
            txtCedula.Text = row.Cells["cedula"].Value?.ToString() ?? "";
            txtNombres.Text = row.Cells["nombres"].Value?.ToString() ?? "";
            txtApellidos.Text = row.Cells["apellidos"].Value?.ToString() ?? "";
            txtCorreo.Text = row.Cells["correo"].Value?.ToString() ?? "";
            chkEstadoUsuario.Checked = Convert.ToInt32(row.Cells["estado"].Value) == 1;

            int rolId = Convert.ToInt32(row.Cells["rol_id"].Value);
            for (int i = 0; i < cbRol.Items.Count; i++)
                if ((cbRol.Items[i] as Rol)?.Id == rolId)
                    cbRol.SelectedIndex = i;
        }

        private async void btnUsrGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCedula.Text) ||
                    string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                    string.IsNullOrWhiteSpace(txtCorreo.Text))
                {
                    MessageBox.Show("Completa todos los campos obligatorios."); return;
                }

                if (cbRol.SelectedItem is not Rol rolSel)
                {
                    MessageBox.Show("Selecciona un rol válido."); return;
                }

                int id = await _svc.InsertUsuarioAsync(
                    txtCedula.Text.Trim(),
                    txtNombres.Text.Trim(),
                    txtApellidos.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    "123456", // Contraseña predeterminada
                    rolSel.Id,
                    chkEstadoUsuario.Checked,
                    false // Valor predeterminado para mustChange
                );
                await CargarUsuarios(txtBuscarUsuario.Text.Trim());
                lblStatus.Text = $"Usuario creado (ID {id}).";

                // 🎯 NUEVO: ofrecer asignar materias si es Docente o Estudiante
                bool esDocente = EsRolDocente(rolSel);
                bool esEstudiante = EsRolEstudiante(rolSel);

                if (esDocente || esEstudiante)
                {
                    var resp = MessageBox.Show(
                        $"¿Quieres asignar materias al {(esDocente ? "docente" : "estudiante")} recién creado?",
                        "Asignar materias",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                    if (resp == DialogResult.Yes)
                    {
                        using var dlg = new AsignarMaterias(_svc, id, esDocente);
                        dlg.ShowDialog(this);
                        // El diálogo ya muestra resultados; aquí solo dejamos constancia
                        lblStatus.Text = "Asignación de materias completada.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(EsDuplicado(ex) ? "El usuario ya existe." : "No se pudo guardar: " + ex.Message);
            }
        }

        private async void btnUsrActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.CurrentRow == null)
                { MessageBox.Show("Selecciona un usuario."); return; }

                if (cbRol.SelectedItem is not Rol rolSel)
                {
                    MessageBox.Show("Selecciona un rol válido."); return;
                }

                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id"].Value);
                await _svc.UpdateUsuarioAsync(
                    id,
                    txtCedula.Text.Trim(),
                    txtNombres.Text.Trim(),
                    txtApellidos.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    rolSel.Id,
                    chkEstadoUsuario.Checked
                );

                await CargarUsuarios(txtBuscarUsuario.Text.Trim());
                lblStatus.Text = $"Usuario actualizado (ID {id}).";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo actualizar: " + ex.Message);
            }
        }

        private async void btnUsrEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) { MessageBox.Show("Selecciona un usuario."); return; }
            int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id"].Value);
            if (MessageBox.Show($"¿Eliminar usuario {id}?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                await _svc.DeleteUsuarioAsync(id);
                await CargarUsuarios(txtBuscarUsuario.Text.Trim());
                LimpiarUsuario();
                lblStatus.Text = $"Usuario eliminado (ID {id}).";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar: " + ex.Message);
            }
        }

        private async void btnUsrResetPass_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) { MessageBox.Show("Selecciona un usuario."); return; }
            int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id"].Value);
            try
            {
                await _svc.ResetPasswordAsync(id);
                lblStatus.Text = $"Contraseña reiniciada (ID {id}).";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo reiniciar: " + ex.Message);
            }
        }

        // ================== ASIGNATURAS ==================
        private async Task CargarAsignaturas(string? filtro = null)
        {
            dgvAsignaturas.DataSource = await _svc.BuscarAsignaturasAsync(filtro);
        }

        private void LimpiarAsignatura()
        {
            txtCodigo.Clear();
            txtNombreAsig.Clear();
            txtDescripcion.Clear();
            chkEstadoAsig.Checked = true;
            dgvAsignaturas.ClearSelection();
            lblStatus.Text = "Formulario de asignatura listo.";
        }

        private async void btnBuscarAsig_Click(object sender, EventArgs e) => await CargarAsignaturas(txtBuscarAsig.Text.Trim());

        private void dgvAsignaturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvAsignaturas.Rows[e.RowIndex];
            txtCodigo.Text = row.Cells["codigo"].Value?.ToString() ?? "";
            txtNombreAsig.Text = row.Cells["nombre"].Value?.ToString() ?? "";
            txtDescripcion.Text = row.Cells["descripcion"].Value?.ToString() ?? "";
            chkEstadoAsig.Checked = Convert.ToInt32(row.Cells["estado"].Value) == 1;
        }

        private async void btnAsigGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombreAsig.Text))
                { MessageBox.Show("Código y nombre son obligatorios."); return; }

                int id = await _svc.InsertAsignaturaAsync(
                    txtCodigo.Text.Trim(),
                    txtNombreAsig.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    chkEstadoAsig.Checked
                );
                await CargarAsignaturas(txtBuscarAsig.Text.Trim());
                lblStatus.Text = $"Asignatura creada (ID {id}).";
            }
            catch (Exception ex)
            {
                lblStatus.Text = EsDuplicado(ex) ? $"El código '{txtCodigo.Text.Trim()}' ya existe." : "Error al guardar.";
            }
        }

        private async void btnAsigActualizar_Click(object sender, EventArgs e)
        {
            if (dgvAsignaturas.CurrentRow == null) { MessageBox.Show("Selecciona una asignatura."); return; }
            int id = Convert.ToInt32(dgvAsignaturas.CurrentRow.Cells["id"].Value);
            try
            {
                await _svc.UpdateAsignaturaAsync(
                    id,
                    txtCodigo.Text.Trim(),
                    txtNombreAsig.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    chkEstadoAsig.Checked
                );
                await CargarAsignaturas(txtBuscarAsig.Text.Trim());
                lblStatus.Text = $"Asignatura actualizada (ID {id}).";
            }
            catch (Exception ex)
            {
                lblStatus.Text = EsDuplicado(ex) ? $"El código '{txtCodigo.Text.Trim()}' ya existe." : "Error al actualizar.";
            }
        }

        private static bool EsRolDocente(object rolObj)
        {
            // Soporta Rol (DTO) o string
            var nombre = rolObj switch
            {
                null => "",
                string s => s,
                _ => rolObj.GetType().GetProperty("Nombre")?.GetValue(rolObj)?.ToString() ?? ""
            };
            if (string.IsNullOrWhiteSpace(nombre)) return false;
            nombre = nombre.Trim().ToLowerInvariant();
            return nombre.Contains("docente") || nombre.Contains("prof") || nombre.Contains("maestr");
        }
        private static bool EsRolEstudiante(object rolObj)
        {
            var nombre = rolObj switch
            {
                null => "",
                string s => s,
                _ => rolObj.GetType().GetProperty("Nombre")?.GetValue(rolObj)?.ToString() ?? ""
            };
            if (string.IsNullOrWhiteSpace(nombre)) return false;
            nombre = nombre.Trim().ToLowerInvariant();
            return nombre.Contains("estudiante") || nombre.Contains("alumn");
        }
        private async void btnAsigEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAsignaturas.CurrentRow == null) { MessageBox.Show("Selecciona una asignatura."); return; }
            int id = Convert.ToInt32(dgvAsignaturas.CurrentRow.Cells["id"].Value);
            if (MessageBox.Show($"¿Eliminar asignatura {id}?", "Confirmar", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                await _svc.DeleteAsignaturaAsync(id);
                await CargarAsignaturas(txtBuscarAsig.Text.Trim());
                LimpiarAsignatura();
                lblStatus.Text = $"Asignatura eliminada (ID {id}).";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar: " + ex.Message);
            }
        }

        // ================== REPORTES ==================
        private async void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                var tipo = cbTipoReporte.SelectedItem?.ToString() ?? "";
                DataTable dt;
                if (tipo.StartsWith("Promedios"))
                    dt = await _svc.ReportePromediosAsync(dtDesde.Value.Date, dtHasta.Value.Date);
                else if (tipo.StartsWith("Asistencia"))
                    dt = await _svc.ReporteAsistenciasAsync(dtDesde.Value.Date, dtHasta.Value.Date);
                else
                    dt = await _svc.ReporteCalificacionesGlobalAsync(dtDesde.Value.Date, dtHasta.Value.Date);

                dgvReportes.DataSource = dt;
                lblStatus.Text = $"Reporte generado: {dt.Rows.Count} filas.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message);
            }
        }

        private void btnExportarReporte_Click(object sender, EventArgs e) => ExportarDataGrid(dgvReportes, "reporte.csv");

        // ================== AUDITORÍA ==================
        private async void btnFiltrarAud_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = await _svc.AuditoriaAsync(
                    txtFiltroUsuario.Text.Trim(),
                    cbAccion.SelectedItem?.ToString(),
                    dtAudDesde.Value.Date,
                    dtAudHasta.Value.Date
                );
                dgvAuditoria.DataSource = dt;
                lblStatus.Text = $"Auditoría: {dt.Rows.Count} eventos.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar auditoría: " + ex.Message);
            }
        }

        private void btnExportAud_Click(object sender, EventArgs e) => ExportarDataGrid(dgvAuditoria, "auditoria.csv");

        // ================== Exportación CSV ==================
        private void ExportarDataGrid(DataGridView grid, string sugerido)
        {
            if (grid.DataSource is not DataTable dt || dt.Rows.Count == 0)
            { MessageBox.Show("Nada para exportar."); return; }

            using var sfd = new SaveFileDialog
            {
                Title = "Exportar CSV",
                FileName = sugerido,
                Filter = "CSV (*.csv)|*.csv|Todos (*.*)|*.*"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var sb = new StringBuilder();
            sb.AppendLine(string.Join(";", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

            foreach (DataRow r in dt.Rows)
            {
                sb.AppendLine(string.Join(";", r.ItemArray.Select(v => v?.ToString()?.Replace(";", ",") ?? "")));
            }

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Exportado.");
        }
    }
}

