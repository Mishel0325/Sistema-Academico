using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_Academico
{
    public partial class AsignarMaterias : Form
    {
        private readonly MySqlAcademicService _svc;
        private readonly int _personaId;
        private readonly bool _esDocente;

        private DataTable _dtMaterias = new DataTable();

        private class Item
        {
            public int Id { get; set; }
            public string Texto { get; set; } = "";
            public override string ToString() => Texto;
        }

        public AsignarMaterias(MySqlAcademicService svc, int personaId, bool esDocente)
        {
            InitializeComponent();

            _svc = svc;
            _personaId = personaId;
            _esDocente = esDocente;

            Text = esDocente ? "Asignar materias al docente" : "Asignar materias al estudiante";
            lblTitulo.Text = "Selecciona una o varias asignaturas:";

            this.Load += AsignarMaterias_Load;
            btnAsignar.Click += async (s, e) => await AsignarAsync();
            btnCancelar.Click += (s, e) => Close();
            txtBuscar.TextChanged += (s, e) => FiltrarLista();
        }

        private async void AsignarMaterias_Load(object? sender, EventArgs e)
        {
            try
            {
                _dtMaterias = await _svc.BuscarAsignaturasAsync(null); // columnas: id, codigo, nombre...
                LlenarCheckedList(_dtMaterias);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar las asignaturas: " + ex.Message);
                Close();
            }
        }

        private void LlenarCheckedList(DataTable dt)
        {
            clbMaterias.Items.Clear();
            foreach (DataRow r in dt.Rows)
            {
                int id = Convert.ToInt32(r["id"]);
                string cod = r["codigo"]?.ToString() ?? "";
                string nom = r["nombre"]?.ToString() ?? "";
                clbMaterias.Items.Add(new Item { Id = id, Texto = $"{cod} – {nom}" });
            }
        }

        private void FiltrarLista()
        {
            if (_dtMaterias.Rows.Count == 0) return;

            string q = txtBuscar.Text.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(q)) { LlenarCheckedList(_dtMaterias); return; }

            var filtered = _dtMaterias.AsEnumerable()
                .Where(r =>
                    (r["codigo"]?.ToString() ?? "").ToLowerInvariant().Contains(q) ||
                    (r["nombre"]?.ToString() ?? "").ToLowerInvariant().Contains(q));

            var dt = filtered.Any() ? filtered.CopyToDataTable() : _dtMaterias.Clone();
            LlenarCheckedList(dt);
        }

        private async Task AsignarAsync()
        {
            if (clbMaterias.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una asignatura.");
                return;
            }

            try
            {
                foreach (var obj in clbMaterias.CheckedItems)
                {
                    var it = (Item)obj;

                    if (_esDocente)
                        await _svc.AsignarDocenteAAsignaturaAsync(_personaId, it.Id);
                    else
                        await _svc.MatricularEstudianteEnAsignaturaAsync(_personaId, it.Id);
                }

                MessageBox.Show("✅ Asignación realizada.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar: " + ex.Message);
            }
        }
    }
}