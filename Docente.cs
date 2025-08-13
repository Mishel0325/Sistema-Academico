using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sistema_Academico
{
    public partial class Docente : Form
    {
        private readonly MySqlAcademicService _svc = new MySqlAcademicService();

        private readonly int _userId;        // id del docente (usuario)
        private readonly string _nombre;     // nombre para mostrar

        // ===== DTO opcional para normalizar (si tu servicio no usa tuplas) =====
        private class Opcion
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = "";
            public override string ToString() => Nombre;
        }

        // ===== Constructor recomendado (desde login) =====
        public Docente(int userId, string nombre)
        {
            InitializeComponent();

            _userId = userId;
            _nombre = nombre;

            // Título y saludo
            Text = string.IsNullOrWhiteSpace(_nombre) ? "Panel del Docente" : $"Panel del Docente – {_nombre}";
            var lbl = Controls.Find("lblTitulo", true);
            if (lbl.Length > 0 && lbl[0] is Label l) l.Text = $"Bienvenido, Prof. {_nombre}";

            // Eventos (por si el diseñador no los enganchó)
            this.Load += Docente_Load;
            lstAsignaturas.SelectedIndexChanged += lstAsignaturas_SelectedIndexChanged;
            btnGuardarNota.Click += btnGuardarNota_Click;
            btnRegistrarAsistencia.Click += btnRegistrarAsistencia_Click;
            btnAsignarTarea.Click += btnAsignarTarea_Click;
            btnCerrarSesion.Click += (s, e) => this.Close();
            chkCertificados.CheckedChanged += chkCertificados_CheckedChanged;
        }

        // ===== Constructor sin parámetros (compatibilidad/pruebas) =====
        public Docente() : this(5, "Docente") { }

        // ================== LOAD ==================
        private async void Docente_Load(object sender, EventArgs e)
        {
            try
            {
                // ------- Parciales -------
                var parciales = await _svc.GetParcialesAsync(); // puede ser List<(int,string)> o DataTable o List<...>
                BindCombo(cbParcial, parciales);

                // ------- Estados de asistencia -------
                var estados = await _svc.GetEstadosAsistenciaAsync();
                BindCombo(cbEstado, estados);

                // ------- Asignaturas del docente -------
                var asigns = await _svc.GetAsignaturasDocenteAsync(_userId);
                BindList(lstAsignaturas, asigns);

                // ✅ Autoseleccionar la primera asignatura y cargar estudiantes
                if (lstAsignaturas.Items.Count > 0)
                {
                    lstAsignaturas.SelectedIndex = 0;
                    var asigId = GetSelectedIdFromListBox(lstAsignaturas);
                    if (asigId != null) await CargarEstudiantesDeAsignatura(asigId.Value);
                }
            }
            catch (ArgumentException ex)
            {
                // Captura típica de DisplayMember/ValueMember incorrectos
                MessageBox.Show("Error de enlace de datos. Revisa los nombres de columnas/propiedades.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        // ================== HELPERS DE ENLACE ==================

        /// <summary>
        /// Enlaza un ComboBox detectando si la fuente es List&lt;(int,string)&gt;, DataTable, o lista de objetos con Id/Nombre.
        /// </summary>
        private static void BindCombo(ComboBox combo, object data)
        {
            combo.DataSource = null;

            if (data is DataTable dt)
            {
                // Espera columnas: id, nombre
                combo.DisplayMember = dt.Columns.Contains("nombre") ? "nombre" : dt.Columns[1].ColumnName;
                combo.ValueMember = dt.Columns.Contains("id") ? "id" : dt.Columns[0].ColumnName;
                combo.DataSource = dt;
                return;
            }

            if (data is IEnumerable<(int, string)> tuples)
            {
                // Tuplas: Item1 (id), Item2 (nombre)
                combo.DisplayMember = "Item2";
                combo.ValueMember = "Item1";
                combo.DataSource = tuples.ToList();
                return;
            }

            if (data is IEnumerable enumerable)
            {
                // Intento genérico: buscar propiedades Id/Nombre (o id/nombre)
                var first = enumerable.Cast<object>().FirstOrDefault();
                if (first != null)
                {
                    var props = first.GetType().GetProperties();
                    var pNombre = props.FirstOrDefault(p => string.Equals(p.Name, "Nombre", StringComparison.OrdinalIgnoreCase))
                               ?? props.FirstOrDefault(p => p.PropertyType == typeof(string));
                    var pId = props.FirstOrDefault(p => string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))
                               ?? props.FirstOrDefault(p => p.PropertyType == typeof(int));

                    if (pNombre != null && pId != null)
                    {
                        combo.DisplayMember = pNombre.Name;
                        combo.ValueMember = pId.Name;
                        combo.DataSource = enumerable;
                        return;
                    }
                }
            }

            throw new ArgumentException("Fuente de datos no soportada para ComboBox.");
        }

        /// <summary>
        /// Enlaza un ListBox con detección de tipo similar a BindCombo.
        /// </summary>
        private static void BindList(ListBox list, object data)
        {
            list.DataSource = null;

            if (data is DataTable dt)
            {
                list.DisplayMember = dt.Columns.Contains("nombre") ? "nombre" : dt.Columns[1].ColumnName;
                list.ValueMember = dt.Columns.Contains("id") ? "id" : dt.Columns[0].ColumnName;
                list.DataSource = dt;
                return;
            }

            if (data is IEnumerable<(int, string)> tuples)
            {
                list.DisplayMember = "Item2";
                list.ValueMember = "Item1";
                list.DataSource = tuples.ToList();
                return;
            }

            if (data is IEnumerable enumerable)
            {
                var first = enumerable.Cast<object>().FirstOrDefault();
                if (first != null)
                {
                    var props = first.GetType().GetProperties();
                    var pNombre = props.FirstOrDefault(p => string.Equals(p.Name, "Nombre", StringComparison.OrdinalIgnoreCase))
                               ?? props.FirstOrDefault(p => p.PropertyType == typeof(string));
                    var pId = props.FirstOrDefault(p => string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase))
                               ?? props.FirstOrDefault(p => p.PropertyType == typeof(int));

                    if (pNombre != null && pId != null)
                    {
                        list.DisplayMember = pNombre.Name;
                        list.ValueMember = pId.Name;
                        list.DataSource = enumerable;
                        return;
                    }
                }
            }

            throw new ArgumentException("Fuente de datos no soportada para ListBox.");
        }

        /// <summary>
        /// Extrae el Id seleccionado de un ComboBox sin importar si está enlazado a tuplas, DataTable o DTO.
        /// </summary>
        private static int? GetSelectedIdFromCombo(ComboBox combo)
        {
            if (combo.SelectedValue is int vi) return vi;
            if (combo.SelectedItem is ValueTuple<int, string> t) return t.Item1;

            var item = combo.SelectedItem;
            if (item == null) return null;

            var prop = item.GetType().GetProperty("Id") ?? item.GetType().GetProperty("id");
            if (prop != null && prop.PropertyType == typeof(int))
                return (int)prop.GetValue(item);

            return null;
        }

        /// <summary>
        /// Extrae el Id seleccionado de un ListBox (mismo criterio que Combo).
        /// </summary>
        private static int? GetSelectedIdFromListBox(ListBox list)
        {
            if (list.SelectedValue is int vi) return vi;
            if (list.SelectedItem is ValueTuple<int, string> t) return t.Item1;

            var item = list.SelectedItem;
            if (item == null) return null;

            var prop = item.GetType().GetProperty("Id") ?? item.GetType().GetProperty("id");
            if (prop != null && prop.PropertyType == typeof(int))
                return (int)prop.GetValue(item);

            return null;
        }

        // ================== UTIL: Cargar estudiantes por asignatura ==================
        private async System.Threading.Tasks.Task CargarEstudiantesDeAsignatura(int asignaturaId)
        {
            try
            {
                var ests = await _svc.GetEstudiantesPorAsignaturaAsync(asignaturaId);
                BindCombo(cbEstudiante, ests);
                if (cbEstudiante.Items.Count > 0) cbEstudiante.SelectedIndex = 0;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error de enlace de estudiantes: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estudiantes: " + ex.Message);
            }
        }

        // ================== CAMBIO DE ASIGNATURA (desde la lista) ==================
        private async void lstAsignaturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var asigId = GetSelectedIdFromListBox(lstAsignaturas);
            if (asigId != null)
                await CargarEstudiantesDeAsignatura(asigId.Value);
        }

        // ================== GUARDAR NOTA ==================
        private async void btnGuardarNota_Click(object sender, EventArgs e)
        {
            var asigId = GetSelectedIdFromListBox(lstAsignaturas);
            var estId = GetSelectedIdFromCombo(cbEstudiante);
            var parcId = GetSelectedIdFromCombo(cbParcial);

            if (asigId == null || estId == null || parcId == null)
            {
                MessageBox.Show("Selecciona asignatura, estudiante y parcial.");
                return;
            }

            try
            {
                await _svc.GuardarNotaAsync(estId.Value, asigId.Value, _userId, parcId.Value, (decimal)numNota.Value);
                MessageBox.Show("✅ Nota guardada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar nota: " + ex.Message);
            }
        }

        // ================== REGISTRAR ASISTENCIA ==================
        private async void btnRegistrarAsistencia_Click(object sender, EventArgs e)
        {
            var asigId = GetSelectedIdFromListBox(lstAsignaturas);
            var estId = GetSelectedIdFromCombo(cbEstudiante);
            var estAsi = GetSelectedIdFromCombo(cbEstado);

            if (asigId == null || estId == null || estAsi == null)
            {
                MessageBox.Show("Selecciona asignatura, estudiante y estado.");
                return;
            }

            try
            {
                await _svc.RegistrarAsistenciaAsync(estId.Value, asigId.Value, _userId, dtpAsistencia.Value, estAsi.Value);
                MessageBox.Show("✅ Asistencia registrada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar asistencia: " + ex.Message);
            }
        }

        // ================== ASIGNAR TAREA ==================
        private async void btnAsignarTarea_Click(object sender, EventArgs e)
        {
            var asigId = GetSelectedIdFromListBox(lstAsignaturas);
            if (asigId == null) { MessageBox.Show("Selecciona una asignatura."); return; }
            if (string.IsNullOrWhiteSpace(txtTituloTarea.Text)) { MessageBox.Show("Escribe el título de la tarea."); return; }
            if (string.IsNullOrWhiteSpace(txtTarea.Text)) { MessageBox.Show("Escribe la descripción de la tarea."); return; }

            try
            {
                await _svc.AsignarTareaAsync(asigId.Value, _userId, txtTituloTarea.Text.Trim(), txtTarea.Text.Trim(), dtpEntrega.Value);
                MessageBox.Show("✅ Tarea asignada.");
                txtTituloTarea.Clear();
                txtTarea.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar tarea: " + ex.Message);
            }
        }

        // ================== CERTIFICADOS (consulta rápida) ==================
        private async void chkCertificados_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkCertificados.Checked) return;
            try
            {
                var estId = GetSelectedIdFromCombo(cbEstudiante);
                if (estId != null)
                {
                    var count = await _svc.CountCertificadosAsync(estId.Value);
                    MessageBox.Show(count > 0 ? $"Tiene {count} certificado(s)." : "Sin certificados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar certificados: " + ex.Message);
            }
            finally
            {
                chkCertificados.Checked = false; // volver al estado original
            }
        }

        // (El diseñador te habrá generado este handler vacío: lo dejamos sin uso)
        private void lstAsignaturas_SelectedIndexChanged_1(object sender, EventArgs e) { }
    }
}

