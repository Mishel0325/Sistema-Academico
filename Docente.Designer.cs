namespace Sistema_Academico
{
    partial class Docente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblAsignatura = new Label();
            lstAsignaturas = new ListBox();
            lblEstudiante = new Label();
            cbEstudiante = new ComboBox();
            lblParcial = new Label();
            cbParcial = new ComboBox();
            lblNota = new Label();
            numNota = new NumericUpDown();
            btnGuardarNota = new Button();
            sep1 = new GroupBox();
            lblFechaAsistencia = new Label();
            dtpAsistencia = new DateTimePicker();
            lblEstado = new Label();
            cbEstado = new ComboBox();
            btnRegistrarAsistencia = new Button();
            sep2 = new GroupBox();
            lblTituloTarea = new Label();
            txtTituloTarea = new TextBox();
            lblTarea = new Label();
            txtTarea = new TextBox();
            lblFechaEntrega = new Label();
            dtpEntrega = new DateTimePicker();
            btnAsignarTarea = new Button();
            chkCertificados = new CheckBox();
            btnCerrarSesion = new Button();
            ((System.ComponentModel.ISupportInitialize)numNota).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(213, 19);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Bienvenido, Panel del Docente";
            // 
            // lblAsignatura
            // 
            lblAsignatura.AutoSize = true;
            lblAsignatura.Location = new Point(20, 48);
            lblAsignatura.Name = "lblAsignatura";
            lblAsignatura.Size = new Size(64, 15);
            lblAsignatura.TabIndex = 1;
            lblAsignatura.Text = "Asignatura";
            // 
            // lstAsignaturas
            // 
            lstAsignaturas.FormattingEnabled = true;
            lstAsignaturas.ItemHeight = 15;
            lstAsignaturas.Location = new Point(20, 67);
            lstAsignaturas.Name = "lstAsignaturas";
            lstAsignaturas.Size = new Size(180, 139);
            lstAsignaturas.TabIndex = 0;
            lstAsignaturas.SelectedIndexChanged += lstAsignaturas_SelectedIndexChanged_1;
            // 
            // lblEstudiante
            // 
            lblEstudiante.AutoSize = true;
            lblEstudiante.Location = new Point(220, 48);
            lblEstudiante.Name = "lblEstudiante";
            lblEstudiante.Size = new Size(62, 15);
            lblEstudiante.TabIndex = 2;
            lblEstudiante.Text = "Estudiante";
            // 
            // cbEstudiante
            // 
            cbEstudiante.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEstudiante.FormattingEnabled = true;
            cbEstudiante.Location = new Point(220, 67);
            cbEstudiante.Name = "cbEstudiante";
            cbEstudiante.Size = new Size(310, 23);
            cbEstudiante.TabIndex = 1;
            // 
            // lblParcial
            // 
            lblParcial.AutoSize = true;
            lblParcial.Location = new Point(220, 100);
            lblParcial.Name = "lblParcial";
            lblParcial.Size = new Size(45, 15);
            lblParcial.TabIndex = 3;
            lblParcial.Text = "Parcial:";
            // 
            // cbParcial
            // 
            cbParcial.DropDownStyle = ComboBoxStyle.DropDownList;
            cbParcial.FormattingEnabled = true;
            cbParcial.Location = new Point(275, 97);
            cbParcial.Name = "cbParcial";
            cbParcial.Size = new Size(120, 23);
            cbParcial.TabIndex = 2;
            // 
            // lblNota
            // 
            lblNota.AutoSize = true;
            lblNota.Location = new Point(405, 100);
            lblNota.Name = "lblNota";
            lblNota.Size = new Size(36, 15);
            lblNota.TabIndex = 4;
            lblNota.Text = "Nota:";
            // 
            // numNota
            // 
            numNota.DecimalPlaces = 2;
            numNota.Location = new Point(447, 97);
            numNota.Name = "numNota";
            numNota.Size = new Size(83, 23);
            numNota.TabIndex = 3;
            // 
            // btnGuardarNota
            // 
            btnGuardarNota.Location = new Point(220, 130);
            btnGuardarNota.Name = "btnGuardarNota";
            btnGuardarNota.Size = new Size(310, 28);
            btnGuardarNota.TabIndex = 4;
            btnGuardarNota.Text = "Guardar Nota";
            btnGuardarNota.UseVisualStyleBackColor = true;
            // 
            // sep1
            // 
            sep1.Location = new Point(20, 215);
            sep1.Name = "sep1";
            sep1.Size = new Size(510, 2);
            sep1.TabIndex = 5;
            sep1.TabStop = false;
            // 
            // lblFechaAsistencia
            // 
            lblFechaAsistencia.AutoSize = true;
            lblFechaAsistencia.Location = new Point(20, 230);
            lblFechaAsistencia.Name = "lblFechaAsistencia";
            lblFechaAsistencia.Size = new Size(95, 15);
            lblFechaAsistencia.TabIndex = 6;
            lblFechaAsistencia.Text = "Fecha asistencia:";
            // 
            // dtpAsistencia
            // 
            dtpAsistencia.Format = DateTimePickerFormat.Short;
            dtpAsistencia.Location = new Point(127, 226);
            dtpAsistencia.Name = "dtpAsistencia";
            dtpAsistencia.Size = new Size(120, 23);
            dtpAsistencia.TabIndex = 5;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(265, 230);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(45, 15);
            lblEstado.TabIndex = 7;
            lblEstado.Text = "Estado:";
            // 
            // cbEstado
            // 
            cbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEstado.FormattingEnabled = true;
            cbEstado.Location = new Point(316, 226);
            cbEstado.Name = "cbEstado";
            cbEstado.Size = new Size(120, 23);
            cbEstado.TabIndex = 6;
            // 
            // btnRegistrarAsistencia
            // 
            btnRegistrarAsistencia.Location = new Point(446, 225);
            btnRegistrarAsistencia.Name = "btnRegistrarAsistencia";
            btnRegistrarAsistencia.Size = new Size(84, 25);
            btnRegistrarAsistencia.TabIndex = 7;
            btnRegistrarAsistencia.Text = "Registrar";
            btnRegistrarAsistencia.UseVisualStyleBackColor = true;
            // 
            // sep2
            // 
            sep2.Location = new Point(20, 270);
            sep2.Name = "sep2";
            sep2.Size = new Size(510, 2);
            sep2.TabIndex = 8;
            sep2.TabStop = false;
            // 
            // lblTituloTarea
            // 
            lblTituloTarea.AutoSize = true;
            lblTituloTarea.Location = new Point(20, 285);
            lblTituloTarea.Name = "lblTituloTarea";
            lblTituloTarea.Size = new Size(40, 15);
            lblTituloTarea.TabIndex = 9;
            lblTituloTarea.Text = "Título:";
            // 
            // txtTituloTarea
            // 
            txtTituloTarea.Location = new Point(68, 281);
            txtTituloTarea.Name = "txtTituloTarea";
            txtTituloTarea.Size = new Size(462, 23);
            txtTituloTarea.TabIndex = 8;
            // 
            // lblTarea
            // 
            lblTarea.AutoSize = true;
            lblTarea.Location = new Point(20, 315);
            lblTarea.Name = "lblTarea";
            lblTarea.Size = new Size(37, 15);
            lblTarea.TabIndex = 10;
            lblTarea.Text = "Tarea:";
            // 
            // txtTarea
            // 
            txtTarea.Location = new Point(20, 334);
            txtTarea.Multiline = true;
            txtTarea.Name = "txtTarea";
            txtTarea.ScrollBars = ScrollBars.Vertical;
            txtTarea.Size = new Size(510, 90);
            txtTarea.TabIndex = 9;
            // 
            // lblFechaEntrega
            // 
            lblFechaEntrega.AutoSize = true;
            lblFechaEntrega.Location = new Point(20, 432);
            lblFechaEntrega.Name = "lblFechaEntrega";
            lblFechaEntrega.Size = new Size(84, 15);
            lblFechaEntrega.TabIndex = 11;
            lblFechaEntrega.Text = "Fecha entrega:";
            // 
            // dtpEntrega
            // 
            dtpEntrega.Format = DateTimePickerFormat.Short;
            dtpEntrega.Location = new Point(112, 428);
            dtpEntrega.Name = "dtpEntrega";
            dtpEntrega.Size = new Size(120, 23);
            dtpEntrega.TabIndex = 10;
            // 
            // btnAsignarTarea
            // 
            btnAsignarTarea.Location = new Point(238, 427);
            btnAsignarTarea.Name = "btnAsignarTarea";
            btnAsignarTarea.Size = new Size(102, 25);
            btnAsignarTarea.TabIndex = 11;
            btnAsignarTarea.Text = "Asignar Tarea";
            btnAsignarTarea.UseVisualStyleBackColor = true;
            // 
            // chkCertificados
            // 
            chkCertificados.AutoSize = true;
            chkCertificados.Location = new Point(360, 430);
            chkCertificados.Name = "chkCertificados";
            chkCertificados.Size = new Size(144, 19);
            chkCertificados.TabIndex = 12;
            chkCertificados.Text = "Descargar Certificados";
            chkCertificados.UseVisualStyleBackColor = true;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(402, 465);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(128, 28);
            btnCerrarSesion.TabIndex = 13;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            // 
            // Docente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 520);
            Controls.Add(lblTitulo);
            Controls.Add(lblAsignatura);
            Controls.Add(lstAsignaturas);
            Controls.Add(lblEstudiante);
            Controls.Add(cbEstudiante);
            Controls.Add(lblParcial);
            Controls.Add(cbParcial);
            Controls.Add(lblNota);
            Controls.Add(numNota);
            Controls.Add(btnGuardarNota);
            Controls.Add(sep1);
            Controls.Add(lblFechaAsistencia);
            Controls.Add(dtpAsistencia);
            Controls.Add(lblEstado);
            Controls.Add(cbEstado);
            Controls.Add(btnRegistrarAsistencia);
            Controls.Add(sep2);
            Controls.Add(lblTituloTarea);
            Controls.Add(txtTituloTarea);
            Controls.Add(lblTarea);
            Controls.Add(txtTarea);
            Controls.Add(lblFechaEntrega);
            Controls.Add(dtpEntrega);
            Controls.Add(btnAsignarTarea);
            Controls.Add(chkCertificados);
            Controls.Add(btnCerrarSesion);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Docente";
            Text = "SISTEMA ACADÉMICO – PANEL DEL DOCENTE";
            ((System.ComponentModel.ISupportInitialize)numNota).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblAsignatura;
        private ListBox lstAsignaturas;

        private Label lblEstudiante;
        private ComboBox cbEstudiante;

        private Label lblParcial;
        private ComboBox cbParcial;

        private Label lblNota;
        private NumericUpDown numNota;
        private Button btnGuardarNota;

        private GroupBox sep1; 

        private Label lblFechaAsistencia;
        private DateTimePicker dtpAsistencia;
        private Label lblEstado;
        private ComboBox cbEstado;
        private Button btnRegistrarAsistencia;

        private GroupBox sep2; 

        private Label lblTituloTarea;
        private TextBox txtTituloTarea;
        private Label lblTarea;
        private TextBox txtTarea;
        private Label lblFechaEntrega;
        private DateTimePicker dtpEntrega;
        private Button btnAsignarTarea;

        private CheckBox chkCertificados;
        private Button btnCerrarSesion;
    }
}