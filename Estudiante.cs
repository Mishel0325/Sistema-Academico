using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Academico
{
    public partial class Estudiante : Form
    {
        private readonly MySqlAcademicService _svc = new MySqlAcademicService();

        private readonly int _userId;        // id del estudiante (usuario)
        private readonly string _nombre;     // nombre para mostrar

        // ===== Constructor recomendado (desde login) =====
        public Estudiante(int userId, string nombre)
        {
            InitializeComponent();

            _userId = userId;
            _nombre = nombre;

            // Título y saludo
            Text = string.IsNullOrWhiteSpace(_nombre) ? "Panel del Estudiante" : $"Panel del Estudiante – {_nombre}";
            var lbl = Controls.Find("lblTitulo", true);
            if (lbl.Length > 0 && lbl[0] is Label l) l.Text = $"Bienvenido, {_nombre}";

            // Eventos (por si el diseñador no los enganchó)
            this.Load += Estudiante_Load;
            lstAsignaturas.SelectedIndexChanged += lstAsignaturas_SelectedIndexChanged;
            cbParcial.SelectedIndexChanged += cbParcial_SelectedIndexChanged;
            btnDescargarCert.Click += btnDescargarCert_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
        }

        // ===== Constructor sin parámetros (compatibilidad/pruebas) =====
        // Cambia 7 y "Estudiante" por un usuario real si quieres probar sin login
        public Estudiante() : this(7, "Estudiante") { }

        // ================== LOAD ==================
        private async void Estudiante_Load(object sender, EventArgs e)
        {
            try
            {
                // Parciales (con opción "Todos")
                var parciales = await _svc.GetParcialesAsync();
                parciales.Insert(0, (0, "Todos"));
                cbParcial.DisplayMember = "Item2";
                cbParcial.ValueMember = "Item1";
                cbParcial.DataSource = parciales;
                cbParcial.SelectedIndex = 0;

                // Tipos de certificados
                var tipos = await _svc.GetTiposCertificadosAsync();
                cbTipoCertificado.DisplayMember = "Item2";
                cbTipoCertificado.ValueMember = "Item1";
                cbTipoCertificado.DataSource = tipos;

                // Asignaturas del estudiante
                var asigns = await _svc.GetAsignaturasEstudianteAsync(_userId);
                lstAsignaturas.DisplayMember = "Item2";
                lstAsignaturas.ValueMember = "Item1";
                lstAsignaturas.DataSource = asigns;

                // Autoselección y carga inicial
                if (asigns.Count > 0)
                {
                    lstAsignaturas.SelectedIndex = 0;
                    await RefrescarNotasYTareas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void lstAsignaturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RefrescarNotasYTareas();
        }

        private async void cbParcial_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RefrescarNotasYTareas(onlyNotas: true);
        }

        private async Task RefrescarNotasYTareas(bool onlyNotas = false)
        {
            if (lstAsignaturas.SelectedItem is not ValueTuple<int, string> asig) return;

            try
            {
                // Notas
                int? parcialId = null;
                if (cbParcial.SelectedItem is ValueTuple<int, string> parc && parc.Item1 != 0)
                    parcialId = parc.Item1;

                DataTable dtNotas = await _svc.GetNotasAsync(_userId, asig.Item1, parcialId);
                dgvNotas.DataSource = dtNotas;

                // Promedio
                var promedio = await _svc.GetPromedioAsync(_userId, asig.Item1);
                lblPromedio.Text = $"Promedio: {(promedio ?? 0m):0.00}/100";

                if (onlyNotas) return;

                // Tareas
                DataTable dtTareas = await _svc.GetTareasAsync(asig.Item1);
                dgvTareas.DataSource = dtTareas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message);
            }
        }

        private async void btnDescargarCert_Click(object sender, EventArgs e)
        {
            if (cbTipoCertificado.SelectedItem is not ValueTuple<int, string> tipo)
            {
                MessageBox.Show("Selecciona un tipo de certificado.");
                return;
            }

            try
            {
                var (archivo, sugerido) = await _svc.DescargarCertificadoAsync(_userId, tipo.Item1);
                if (archivo == null || archivo.Length == 0)
                {
                    MessageBox.Show("No hay certificado disponible.");
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Title = "Guardar certificado",
                    FileName = sugerido,
                    Filter = "PDF (*.pdf)|*.pdf|Todos (*.*)|*.*"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, archivo);
                    MessageBox.Show("Certificado guardado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo descargar el certificado: " + ex.Message);
            }
        }

        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            await RefrescarNotasYTareas();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Handlers vacíos del diseñador (si están enganchados)
        private void dgvNotas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvTareas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void cbTipoCertificado_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}


