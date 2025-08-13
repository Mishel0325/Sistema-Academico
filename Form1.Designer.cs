using System.Drawing;
using System.Windows.Forms;

namespace Sistema_Academico
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitulo = new Label();
            panelCard = new Panel();
            linkOlvido = new LinkLabel();
            chkMostrar = new CheckBox();
            btnIniciar = new Button();
            txtContraseña = new TextBox();
            txtUsuario = new TextBox();
            lblSubtitulo = new Label();
            lblUsuario = new Label();
            lblContraseña = new Label();
            panelHeader.SuspendLayout();
            panelCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(33, 150, 243);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(880, 72);
            panelHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(24, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(235, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "SISTEMA ACADÉMICO";
            // 
            // panelCard
            // 
            panelCard.Anchor = AnchorStyles.None;
            panelCard.BackColor = Color.White;
            panelCard.Controls.Add(linkOlvido);
            panelCard.Controls.Add(chkMostrar);
            panelCard.Controls.Add(btnIniciar);
            panelCard.Controls.Add(txtContraseña);
            panelCard.Controls.Add(txtUsuario);
            panelCard.Controls.Add(lblSubtitulo);
            panelCard.Controls.Add(lblUsuario);
            panelCard.Controls.Add(lblContraseña);
            panelCard.Location = new Point(227, 137);
            panelCard.Name = "panelCard";
            panelCard.Padding = new Padding(28);
            panelCard.Size = new Size(420, 360);
            panelCard.TabIndex = 2;
            // 
            // linkOlvido
            // 
            linkOlvido.AutoSize = true;
            linkOlvido.LinkColor = Color.FromArgb(33, 150, 243);
            linkOlvido.Location = new Point(28, 295);
            linkOlvido.Name = "linkOlvido";
            linkOlvido.Size = new Size(141, 15);
            linkOlvido.TabIndex = 7;
            linkOlvido.TabStop = true;
            linkOlvido.Text = "¿Olvidaste tu contraseña?";
            // 
            // chkMostrar
            // 
            chkMostrar.AutoSize = true;
            chkMostrar.Location = new Point(28, 198);
            chkMostrar.Name = "chkMostrar";
            chkMostrar.Size = new Size(128, 19);
            chkMostrar.TabIndex = 4;
            chkMostrar.Text = "Mostrar contraseña";
            chkMostrar.UseVisualStyleBackColor = true;
            chkMostrar.CheckedChanged += chkMostrar_CheckedChanged;
            // 
            // btnIniciar
            // 
            btnIniciar.BackColor = Color.FromArgb(33, 150, 243);
            btnIniciar.Cursor = Cursors.Hand;
            btnIniciar.FlatAppearance.BorderSize = 0;
            btnIniciar.FlatStyle = FlatStyle.Flat;
            btnIniciar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnIniciar.ForeColor = Color.White;
            btnIniciar.Location = new Point(28, 240);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(360, 40);
            btnIniciar.TabIndex = 5;
            btnIniciar.Text = "Iniciar sesión";
            btnIniciar.UseVisualStyleBackColor = false;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // txtContraseña
            // 
            txtContraseña.Font = new Font("Segoe UI", 10F);
            txtContraseña.Location = new Point(28, 165);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.Size = new Size(360, 25);
            txtContraseña.TabIndex = 3;
            txtContraseña.UseSystemPasswordChar = true;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 10F);
            txtUsuario.Location = new Point(28, 100);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(360, 25);
            txtUsuario.TabIndex = 1;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSubtitulo.ForeColor = Color.FromArgb(33, 37, 41);
            lblSubtitulo.Location = new Point(28, 28);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(110, 21);
            lblSubtitulo.TabIndex = 0;
            lblSubtitulo.Text = "Iniciar sesión";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(28, 80);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(50, 15);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario ";
            // 
            // lblContraseña
            // 
            lblContraseña.AutoSize = true;
            lblContraseña.Location = new Point(28, 145);
            lblContraseña.Name = "lblContraseña";
            lblContraseña.Size = new Size(67, 15);
            lblContraseña.TabIndex = 0;
            lblContraseña.Text = "Contraseña";
            // 
            // Form1
            // 
            AcceptButton = btnIniciar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(880, 520);
            Controls.Add(panelHeader);
            Controls.Add(panelCard);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(720, 460);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Académico – Iniciar sesión";
            Load += Form1_Load;
            Resize += Form1_Resize;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelCard.ResumeLayout(false);
            panelCard.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.CheckBox chkMostrar;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.LinkLabel linkOlvido;
    }
}
