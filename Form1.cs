using System;
using System.Windows.Forms;

namespace Sistema_Academico
{
    public partial class Form1 : Form
    {
        private readonly MySqlAcademicService _svc;

        public Form1()
        {
            InitializeComponent();
            // Usa el ctor por defecto: root sin contraseña a BD sistema_academico
            _svc = new MySqlAcademicService();
        }

        private void Form1_Load(object sender, EventArgs e) => CenterCard();
        private void Form1_Resize(object sender, EventArgs e) => CenterCard();

        private void CenterCard()
        {
            panelCard.Left = (ClientSize.Width - panelCard.Width) / 2;
            panelCard.Top = (ClientSize.Height - panelCard.Height) / 2;
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = !chkMostrar.Checked;
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            var usuario = txtUsuario.Text.Trim();
            var pass = txtContraseña.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Ingresa usuario y contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                btnIniciar.Enabled = false;

                // Especificar explícitamente cuál de los métodos LoginAsync usar
                var login = await _svc.LoginWithMustChangeAsync(usuario, pass);
                if (login is null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var (userId, nombre, rolId, rolNombre, _) = login.Value;
                var rolText = (rolNombre ?? "").Trim().ToLower();

                Form next;

                if (rolId == 3 || rolText.Contains("admin"))
                {
                    next = new Administrador(userId, nombre);
                }
                else if (rolId == 2 || rolText.Contains("docente") || rolText.Contains("prof"))
                {
                    next = new Docente(userId, nombre);
                }
                else
                {
                    next = new Estudiante(userId, nombre);
                }

                Hide();
                next.FormClosed += (_, __) => Close();
                next.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar con MySQL.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnIniciar.Enabled = true;
            }
        }

    }
}

