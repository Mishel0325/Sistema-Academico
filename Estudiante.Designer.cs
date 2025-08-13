namespace Sistema_Academico
{
    partial class Estudiante
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
            lblTitulo = new Label();
            lblAsignaturas = new Label();
            lstAsignaturas = new ListBox();
            lblParcial = new Label();
            cbParcial = new ComboBox();
            dgvNotas = new DataGridView();
            lblNotas = new Label();
            lblPromedio = new Label();
            grpSep1 = new GroupBox();
            lblTareas = new Label();
            dgvTareas = new DataGridView();
            lblCertificados = new Label();
            cbTipoCertificado = new ComboBox();
            btnDescargarCert = new Button();
            btnActualizar = new Button();
            btnCerrarSesion = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvNotas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTareas).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(230, 19);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Bienvenido, Estudiante (Nombre)";
            // 
            // lblAsignaturas
            // 
            lblAsignaturas.AutoSize = true;
            lblAsignaturas.Location = new Point(20, 48);
            lblAsignaturas.Name = "lblAsignaturas";
            lblAsignaturas.Size = new Size(115, 15);
            lblAsignaturas.TabIndex = 1;
            lblAsignaturas.Text = "Mis asignaturas (BD)";
            // 
            // lstAsignaturas
            // 
            lstAsignaturas.FormattingEnabled = true;
            lstAsignaturas.ItemHeight = 15;
            lstAsignaturas.Location = new Point(20, 67);
            lstAsignaturas.Name = "lstAsignaturas";
            lstAsignaturas.Size = new Size(230, 154);
            lstAsignaturas.TabIndex = 0;
            lstAsignaturas.SelectedIndexChanged += lstAsignaturas_SelectedIndexChanged;
            // 
            // lblParcial
            // 
            lblParcial.AutoSize = true;
            lblParcial.Location = new Point(270, 48);
            lblParcial.Name = "lblParcial";
            lblParcial.Size = new Size(96, 15);
            lblParcial.TabIndex = 2;
            lblParcial.Text = "Filtrar por parcial";
            // 
            // cbParcial
            // 
            cbParcial.DropDownStyle = ComboBoxStyle.DropDownList;
            cbParcial.FormattingEnabled = true;
            cbParcial.Location = new Point(368, 44);
            cbParcial.Name = "cbParcial";
            cbParcial.Size = new Size(160, 23);
            cbParcial.TabIndex = 1;
            cbParcial.SelectedIndexChanged += cbParcial_SelectedIndexChanged;
            // 
            // dgvNotas
            // 
            dgvNotas.AllowUserToAddRows = false;
            dgvNotas.AllowUserToDeleteRows = false;
            dgvNotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNotas.BackgroundColor = SystemColors.Window;
            dgvNotas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNotas.Location = new Point(270, 99);
            dgvNotas.MultiSelect = false;
            dgvNotas.Name = "dgvNotas";
            dgvNotas.ReadOnly = true;
            dgvNotas.RowHeadersVisible = false;
            dgvNotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNotas.Size = new Size(520, 122);
            dgvNotas.TabIndex = 2;
            dgvNotas.CellContentClick += dgvNotas_CellContentClick;
            // 
            // lblNotas
            // 
            lblNotas.AutoSize = true;
            lblNotas.Location = new Point(270, 80);
            lblNotas.Name = "lblNotas";
            lblNotas.Size = new Size(106, 15);
            lblNotas.TabIndex = 3;
            lblNotas.Text = "Calificaciones (BD)";
            // 
            // lblPromedio
            // 
            lblPromedio.AutoSize = true;
            lblPromedio.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPromedio.Location = new Point(680, 48);
            lblPromedio.Name = "lblPromedio";
            lblPromedio.Size = new Size(117, 15);
            lblPromedio.TabIndex = 4;
            lblPromedio.Text = "Promedio: 0.00/100";
            // 
            // grpSep1
            // 
            grpSep1.Location = new Point(20, 235);
            grpSep1.Name = "grpSep1";
            grpSep1.Size = new Size(770, 2);
            grpSep1.TabIndex = 5;
            grpSep1.TabStop = false;
            // 
            // lblTareas
            // 
            lblTareas.AutoSize = true;
            lblTareas.Location = new Point(20, 250);
            lblTareas.Name = "lblTareas";
            lblTareas.Size = new Size(120, 15);
            lblTareas.TabIndex = 6;
            lblTareas.Text = "Tareas asignadas (BD)";
            // 
            // dgvTareas
            // 
            dgvTareas.AllowUserToAddRows = false;
            dgvTareas.AllowUserToDeleteRows = false;
            dgvTareas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTareas.BackgroundColor = SystemColors.Window;
            dgvTareas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTareas.Location = new Point(20, 269);
            dgvTareas.MultiSelect = false;
            dgvTareas.Name = "dgvTareas";
            dgvTareas.ReadOnly = true;
            dgvTareas.RowHeadersVisible = false;
            dgvTareas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTareas.Size = new Size(770, 160);
            dgvTareas.TabIndex = 3;
            dgvTareas.CellContentClick += dgvTareas_CellContentClick;
            // 
            // lblCertificados
            // 
            lblCertificados.AutoSize = true;
            lblCertificados.Location = new Point(20, 440);
            lblCertificados.Name = "lblCertificados";
            lblCertificados.Size = new Size(149, 15);
            lblCertificados.TabIndex = 7;
            lblCertificados.Text = "Descargar certificados (BD)";
            // 
            // cbTipoCertificado
            // 
            cbTipoCertificado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoCertificado.FormattingEnabled = true;
            cbTipoCertificado.Location = new Point(187, 436);
            cbTipoCertificado.Name = "cbTipoCertificado";
            cbTipoCertificado.Size = new Size(240, 23);
            cbTipoCertificado.TabIndex = 4;
            cbTipoCertificado.SelectedIndexChanged += cbTipoCertificado_SelectedIndexChanged;
            // 
            // btnDescargarCert
            // 
            btnDescargarCert.Location = new Point(433, 435);
            btnDescargarCert.Name = "btnDescargarCert";
            btnDescargarCert.Size = new Size(144, 26);
            btnDescargarCert.TabIndex = 5;
            btnDescargarCert.Text = "Descargar certificado";
            btnDescargarCert.UseVisualStyleBackColor = true;
            btnDescargarCert.Click += btnDescargarCert_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(600, 435);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(90, 26);
            btnActualizar.TabIndex = 6;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(696, 435);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(94, 26);
            btnCerrarSesion.TabIndex = 7;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // Estudiante
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 560);
            Controls.Add(lblTitulo);
            Controls.Add(lblAsignaturas);
            Controls.Add(lstAsignaturas);
            Controls.Add(lblParcial);
            Controls.Add(cbParcial);
            Controls.Add(lblNotas);
            Controls.Add(dgvNotas);
            Controls.Add(lblPromedio);
            Controls.Add(grpSep1);
            Controls.Add(lblTareas);
            Controls.Add(dgvTareas);
            Controls.Add(lblCertificados);
            Controls.Add(cbTipoCertificado);
            Controls.Add(btnDescargarCert);
            Controls.Add(btnActualizar);
            Controls.Add(btnCerrarSesion);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Estudiante";
            Text = "SISTEMA ACADÉMICO – PANEL DEL ESTUDIANTE";
            ((System.ComponentModel.ISupportInitialize)dgvNotas).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTareas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblAsignaturas;
        private ListBox lstAsignaturas;

        private Label lblParcial;
        private ComboBox cbParcial;
        private Label lblNotas;
        private DataGridView dgvNotas;
        private Label lblPromedio;

        private GroupBox grpSep1;
        private Label lblTareas;
        private DataGridView dgvTareas;

        private Label lblCertificados;
        private ComboBox cbTipoCertificado;
        private Button btnDescargarCert;

        private Button btnActualizar;
        private Button btnCerrarSesion;
    }
}
