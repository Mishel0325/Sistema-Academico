namespace Sistema_Academico
{
    partial class Administrador
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
            tabMain = new TabControl();
            tpUsuarios = new TabPage();
            grpUsuarioForm = new GroupBox();
            lblCedula = new Label();
            txtCedula = new TextBox();
            lblNombres = new Label();
            txtNombres = new TextBox();
            lblApellidos = new Label();
            txtApellidos = new TextBox();
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            lblRol = new Label();
            cbRol = new ComboBox();
            chkEstadoUsuario = new CheckBox();
            btnUsrNuevo = new Button();
            btnUsrGuardar = new Button();
            btnUsrActualizar = new Button();
            btnUsrEliminar = new Button();
            btnUsrResetPass = new Button();
            lblBuscarUsuario = new Label();
            txtBuscarUsuario = new TextBox();
            btnBuscarUsuario = new Button();
            dgvUsuarios = new DataGridView();
            tpAsignaturas = new TabPage();
            grpAsigForm = new GroupBox();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblNombreAsig = new Label();
            txtNombreAsig = new TextBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            chkEstadoAsig = new CheckBox();
            btnAsigNuevo = new Button();
            btnAsigGuardar = new Button();
            btnAsigActualizar = new Button();
            btnAsigEliminar = new Button();
            lblBuscarAsig = new Label();
            txtBuscarAsig = new TextBox();
            btnBuscarAsig = new Button();
            dgvAsignaturas = new DataGridView();
            tpReportes = new TabPage();
            lblTipoReporte = new Label();
            cbTipoReporte = new ComboBox();
            lblDesde = new Label();
            dtDesde = new DateTimePicker();
            lblHasta = new Label();
            dtHasta = new DateTimePicker();
            btnGenerarReporte = new Button();
            btnExportarReporte = new Button();
            dgvReportes = new DataGridView();
            tpAuditoria = new TabPage();
            lblFiltroUsuario = new Label();
            txtFiltroUsuario = new TextBox();
            lblAccion = new Label();
            cbAccion = new ComboBox();
            lblAudDesde = new Label();
            dtAudDesde = new DateTimePicker();
            lblAudHasta = new Label();
            dtAudHasta = new DateTimePicker();
            btnFiltrarAud = new Button();
            dgvAuditoria = new DataGridView();
            btnExportAud = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            tabMain.SuspendLayout();
            tpUsuarios.SuspendLayout();
            grpUsuarioForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            tpAsignaturas.SuspendLayout();
            grpAsigForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAsignaturas).BeginInit();
            tpReportes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).BeginInit();
            tpAuditoria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tpUsuarios);
            tabMain.Controls.Add(tpAsignaturas);
            tabMain.Controls.Add(tpReportes);
            tabMain.Controls.Add(tpAuditoria);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(894, 518);
            tabMain.TabIndex = 0;
            // 
            // tpUsuarios
            // 
            tpUsuarios.Controls.Add(grpUsuarioForm);
            tpUsuarios.Controls.Add(lblBuscarUsuario);
            tpUsuarios.Controls.Add(txtBuscarUsuario);
            tpUsuarios.Controls.Add(btnBuscarUsuario);
            tpUsuarios.Controls.Add(dgvUsuarios);
            tpUsuarios.Location = new Point(4, 24);
            tpUsuarios.Name = "tpUsuarios";
            tpUsuarios.Padding = new Padding(10);
            tpUsuarios.Size = new Size(886, 490);
            tpUsuarios.TabIndex = 0;
            tpUsuarios.Text = "Usuarios";
            // 
            // grpUsuarioForm
            // 
            grpUsuarioForm.Controls.Add(lblCedula);
            grpUsuarioForm.Controls.Add(txtCedula);
            grpUsuarioForm.Controls.Add(lblNombres);
            grpUsuarioForm.Controls.Add(txtNombres);
            grpUsuarioForm.Controls.Add(lblApellidos);
            grpUsuarioForm.Controls.Add(txtApellidos);
            grpUsuarioForm.Controls.Add(lblCorreo);
            grpUsuarioForm.Controls.Add(txtCorreo);
            grpUsuarioForm.Controls.Add(lblRol);
            grpUsuarioForm.Controls.Add(cbRol);
            grpUsuarioForm.Controls.Add(chkEstadoUsuario);
            grpUsuarioForm.Controls.Add(btnUsrNuevo);
            grpUsuarioForm.Controls.Add(btnUsrGuardar);
            grpUsuarioForm.Controls.Add(btnUsrActualizar);
            grpUsuarioForm.Controls.Add(btnUsrEliminar);
            grpUsuarioForm.Controls.Add(btnUsrResetPass);
            grpUsuarioForm.Location = new Point(10, 10);
            grpUsuarioForm.Name = "grpUsuarioForm";
            grpUsuarioForm.Size = new Size(420, 220);
            grpUsuarioForm.TabIndex = 0;
            grpUsuarioForm.TabStop = false;
            grpUsuarioForm.Text = "Formulario de usuario";
            // 
            // lblCedula
            // 
            lblCedula.Location = new Point(15, 30);
            lblCedula.Name = "lblCedula";
            lblCedula.Size = new Size(70, 20);
            lblCedula.TabIndex = 0;
            lblCedula.Text = "Cédula:";
            // 
            // txtCedula
            // 
            txtCedula.Location = new Point(100, 28);
            txtCedula.Name = "txtCedula";
            txtCedula.Size = new Size(140, 23);
            txtCedula.TabIndex = 1;
            // 
            // lblNombres
            // 
            lblNombres.Location = new Point(15, 60);
            lblNombres.Name = "lblNombres";
            lblNombres.Size = new Size(70, 20);
            lblNombres.TabIndex = 2;
            lblNombres.Text = "Nombres:";
            // 
            // txtNombres
            // 
            txtNombres.Location = new Point(100, 58);
            txtNombres.Name = "txtNombres";
            txtNombres.Size = new Size(300, 23);
            txtNombres.TabIndex = 3;
            // 
            // lblApellidos
            // 
            lblApellidos.Location = new Point(15, 90);
            lblApellidos.Name = "lblApellidos";
            lblApellidos.Size = new Size(70, 20);
            lblApellidos.TabIndex = 4;
            lblApellidos.Text = "Apellidos:";
            // 
            // txtApellidos
            // 
            txtApellidos.Location = new Point(100, 88);
            txtApellidos.Name = "txtApellidos";
            txtApellidos.Size = new Size(300, 23);
            txtApellidos.TabIndex = 5;
            // 
            // lblCorreo
            // 
            lblCorreo.Location = new Point(15, 120);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(70, 20);
            lblCorreo.TabIndex = 6;
            lblCorreo.Text = "Correo:";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(100, 118);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(300, 23);
            txtCorreo.TabIndex = 7;
            // 
            // lblRol
            // 
            lblRol.Location = new Point(15, 150);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(70, 20);
            lblRol.TabIndex = 8;
            lblRol.Text = "Rol:";
            // 
            // cbRol
            // 
            cbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRol.Location = new Point(100, 148);
            cbRol.Name = "cbRol";
            cbRol.Size = new Size(180, 23);
            cbRol.TabIndex = 9;
            // 
            // chkEstadoUsuario
            // 
            chkEstadoUsuario.Location = new Point(300, 150);
            chkEstadoUsuario.Name = "chkEstadoUsuario";
            chkEstadoUsuario.Size = new Size(80, 23);
            chkEstadoUsuario.TabIndex = 10;
            chkEstadoUsuario.Text = "Activo";
            // 
            // btnUsrNuevo
            // 
            btnUsrNuevo.Location = new Point(15, 185);
            btnUsrNuevo.Name = "btnUsrNuevo";
            btnUsrNuevo.Size = new Size(70, 26);
            btnUsrNuevo.TabIndex = 11;
            btnUsrNuevo.Text = "Nuevo";
            // 
            // btnUsrGuardar
            // 
            btnUsrGuardar.Location = new Point(95, 185);
            btnUsrGuardar.Name = "btnUsrGuardar";
            btnUsrGuardar.Size = new Size(75, 26);
            btnUsrGuardar.TabIndex = 12;
            btnUsrGuardar.Text = "Guardar";
            // 
            // btnUsrActualizar
            // 
            btnUsrActualizar.Location = new Point(180, 185);
            btnUsrActualizar.Name = "btnUsrActualizar";
            btnUsrActualizar.Size = new Size(85, 26);
            btnUsrActualizar.TabIndex = 13;
            btnUsrActualizar.Text = "Actualizar";
            // 
            // btnUsrEliminar
            // 
            btnUsrEliminar.Location = new Point(275, 185);
            btnUsrEliminar.Name = "btnUsrEliminar";
            btnUsrEliminar.Size = new Size(75, 26);
            btnUsrEliminar.TabIndex = 14;
            btnUsrEliminar.Text = "Eliminar";
            // 
            // btnUsrResetPass
            // 
            btnUsrResetPass.Location = new Point(355, 185);
            btnUsrResetPass.Name = "btnUsrResetPass";
            btnUsrResetPass.Size = new Size(55, 26);
            btnUsrResetPass.TabIndex = 15;
            btnUsrResetPass.Text = "Reset Pass";
            // 
            // lblBuscarUsuario
            // 
            lblBuscarUsuario.Location = new Point(450, 18);
            lblBuscarUsuario.Name = "lblBuscarUsuario";
            lblBuscarUsuario.Size = new Size(55, 20);
            lblBuscarUsuario.TabIndex = 1;
            lblBuscarUsuario.Text = "Buscar:";
            // 
            // txtBuscarUsuario
            // 
            txtBuscarUsuario.Location = new Point(505, 16);
            txtBuscarUsuario.Name = "txtBuscarUsuario";
            txtBuscarUsuario.Size = new Size(250, 23);
            txtBuscarUsuario.TabIndex = 2;
            // 
            // btnBuscarUsuario
            // 
            btnBuscarUsuario.Location = new Point(765, 15);
            btnBuscarUsuario.Name = "btnBuscarUsuario";
            btnBuscarUsuario.Size = new Size(90, 26);
            btnBuscarUsuario.TabIndex = 3;
            btnBuscarUsuario.Text = "Buscar";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.Location = new Point(450, 50);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(405, 400);
            dgvUsuarios.TabIndex = 4;
            // 
            // tpAsignaturas
            // 
            tpAsignaturas.Controls.Add(grpAsigForm);
            tpAsignaturas.Controls.Add(lblBuscarAsig);
            tpAsignaturas.Controls.Add(txtBuscarAsig);
            tpAsignaturas.Controls.Add(btnBuscarAsig);
            tpAsignaturas.Controls.Add(dgvAsignaturas);
            tpAsignaturas.Location = new Point(4, 24);
            tpAsignaturas.Name = "tpAsignaturas";
            tpAsignaturas.Padding = new Padding(10);
            tpAsignaturas.Size = new Size(886, 490);
            tpAsignaturas.TabIndex = 1;
            tpAsignaturas.Text = "Asignaturas";
            // 
            // grpAsigForm
            // 
            grpAsigForm.Controls.Add(lblCodigo);
            grpAsigForm.Controls.Add(txtCodigo);
            grpAsigForm.Controls.Add(lblNombreAsig);
            grpAsigForm.Controls.Add(txtNombreAsig);
            grpAsigForm.Controls.Add(lblDescripcion);
            grpAsigForm.Controls.Add(txtDescripcion);
            grpAsigForm.Controls.Add(chkEstadoAsig);
            grpAsigForm.Controls.Add(btnAsigNuevo);
            grpAsigForm.Controls.Add(btnAsigGuardar);
            grpAsigForm.Controls.Add(btnAsigActualizar);
            grpAsigForm.Controls.Add(btnAsigEliminar);
            grpAsigForm.Location = new Point(10, 10);
            grpAsigForm.Name = "grpAsigForm";
            grpAsigForm.Size = new Size(420, 220);
            grpAsigForm.TabIndex = 0;
            grpAsigForm.TabStop = false;
            grpAsigForm.Text = "Formulario de asignatura";
            // 
            // lblCodigo
            // 
            lblCodigo.Location = new Point(15, 30);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(70, 20);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código:";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(100, 28);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(140, 23);
            txtCodigo.TabIndex = 1;
            // 
            // lblNombreAsig
            // 
            lblNombreAsig.Location = new Point(15, 60);
            lblNombreAsig.Name = "lblNombreAsig";
            lblNombreAsig.Size = new Size(70, 20);
            lblNombreAsig.TabIndex = 2;
            lblNombreAsig.Text = "Nombre:";
            // 
            // txtNombreAsig
            // 
            txtNombreAsig.Location = new Point(100, 58);
            txtNombreAsig.Name = "txtNombreAsig";
            txtNombreAsig.Size = new Size(300, 23);
            txtNombreAsig.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            lblDescripcion.Location = new Point(15, 90);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(80, 20);
            lblDescripcion.TabIndex = 4;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(100, 88);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(300, 60);
            txtDescripcion.TabIndex = 5;
            // 
            // chkEstadoAsig
            // 
            chkEstadoAsig.Location = new Point(100, 155);
            chkEstadoAsig.Name = "chkEstadoAsig";
            chkEstadoAsig.Size = new Size(65, 23);
            chkEstadoAsig.TabIndex = 6;
            chkEstadoAsig.Text = "Activa";
            // 
            // btnAsigNuevo
            // 
            btnAsigNuevo.Location = new Point(15, 185);
            btnAsigNuevo.Name = "btnAsigNuevo";
            btnAsigNuevo.Size = new Size(70, 26);
            btnAsigNuevo.TabIndex = 7;
            btnAsigNuevo.Text = "Nuevo";
            // 
            // btnAsigGuardar
            // 
            btnAsigGuardar.Location = new Point(95, 185);
            btnAsigGuardar.Name = "btnAsigGuardar";
            btnAsigGuardar.Size = new Size(75, 26);
            btnAsigGuardar.TabIndex = 8;
            btnAsigGuardar.Text = "Guardar";
            // 
            // btnAsigActualizar
            // 
            btnAsigActualizar.Location = new Point(180, 185);
            btnAsigActualizar.Name = "btnAsigActualizar";
            btnAsigActualizar.Size = new Size(85, 26);
            btnAsigActualizar.TabIndex = 9;
            btnAsigActualizar.Text = "Actualizar";
            // 
            // btnAsigEliminar
            // 
            btnAsigEliminar.Location = new Point(275, 185);
            btnAsigEliminar.Name = "btnAsigEliminar";
            btnAsigEliminar.Size = new Size(75, 26);
            btnAsigEliminar.TabIndex = 10;
            btnAsigEliminar.Text = "Eliminar";
            // 
            // lblBuscarAsig
            // 
            lblBuscarAsig.Location = new Point(450, 18);
            lblBuscarAsig.Name = "lblBuscarAsig";
            lblBuscarAsig.Size = new Size(55, 20);
            lblBuscarAsig.TabIndex = 1;
            lblBuscarAsig.Text = "Buscar:";
            // 
            // txtBuscarAsig
            // 
            txtBuscarAsig.Location = new Point(505, 16);
            txtBuscarAsig.Name = "txtBuscarAsig";
            txtBuscarAsig.Size = new Size(250, 23);
            txtBuscarAsig.TabIndex = 2;
            // 
            // btnBuscarAsig
            // 
            btnBuscarAsig.Location = new Point(765, 15);
            btnBuscarAsig.Name = "btnBuscarAsig";
            btnBuscarAsig.Size = new Size(90, 26);
            btnBuscarAsig.TabIndex = 3;
            btnBuscarAsig.Text = "Buscar";
            // 
            // dgvAsignaturas
            // 
            dgvAsignaturas.AllowUserToAddRows = false;
            dgvAsignaturas.AllowUserToDeleteRows = false;
            dgvAsignaturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAsignaturas.Location = new Point(450, 50);
            dgvAsignaturas.MultiSelect = false;
            dgvAsignaturas.Name = "dgvAsignaturas";
            dgvAsignaturas.ReadOnly = true;
            dgvAsignaturas.RowHeadersVisible = false;
            dgvAsignaturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAsignaturas.Size = new Size(405, 400);
            dgvAsignaturas.TabIndex = 4;
            // 
            // tpReportes
            // 
            tpReportes.Controls.Add(lblTipoReporte);
            tpReportes.Controls.Add(cbTipoReporte);
            tpReportes.Controls.Add(lblDesde);
            tpReportes.Controls.Add(dtDesde);
            tpReportes.Controls.Add(lblHasta);
            tpReportes.Controls.Add(dtHasta);
            tpReportes.Controls.Add(btnGenerarReporte);
            tpReportes.Controls.Add(btnExportarReporte);
            tpReportes.Controls.Add(dgvReportes);
            tpReportes.Location = new Point(4, 24);
            tpReportes.Name = "tpReportes";
            tpReportes.Padding = new Padding(10);
            tpReportes.Size = new Size(886, 490);
            tpReportes.TabIndex = 2;
            tpReportes.Text = "Reportes";
            // 
            // lblTipoReporte
            // 
            lblTipoReporte.Location = new Point(15, 18);
            lblTipoReporte.Name = "lblTipoReporte";
            lblTipoReporte.Size = new Size(100, 20);
            lblTipoReporte.TabIndex = 0;
            lblTipoReporte.Text = "Tipo de reporte:";
            // 
            // cbTipoReporte
            // 
            cbTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoReporte.Items.AddRange(new object[] { "Promedios por asignatura", "Asistencia por asignatura", "Calificaciones globales" });
            cbTipoReporte.Location = new Point(120, 16);
            cbTipoReporte.Name = "cbTipoReporte";
            cbTipoReporte.Size = new Size(220, 23);
            cbTipoReporte.TabIndex = 1;
            // 
            // lblDesde
            // 
            lblDesde.Location = new Point(360, 18);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(45, 20);
            lblDesde.TabIndex = 2;
            lblDesde.Text = "Desde:";
            // 
            // dtDesde
            // 
            dtDesde.Format = DateTimePickerFormat.Short;
            dtDesde.Location = new Point(410, 16);
            dtDesde.Name = "dtDesde";
            dtDesde.Size = new Size(110, 23);
            dtDesde.TabIndex = 3;
            // 
            // lblHasta
            // 
            lblHasta.Location = new Point(530, 18);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(45, 20);
            lblHasta.TabIndex = 4;
            lblHasta.Text = "Hasta:";
            // 
            // dtHasta
            // 
            dtHasta.Format = DateTimePickerFormat.Short;
            dtHasta.Location = new Point(580, 16);
            dtHasta.Name = "dtHasta";
            dtHasta.Size = new Size(110, 23);
            dtHasta.TabIndex = 5;
            // 
            // btnGenerarReporte
            // 
            btnGenerarReporte.Location = new Point(700, 15);
            btnGenerarReporte.Name = "btnGenerarReporte";
            btnGenerarReporte.Size = new Size(75, 26);
            btnGenerarReporte.TabIndex = 6;
            btnGenerarReporte.Text = "Generar";
            // 
            // btnExportarReporte
            // 
            btnExportarReporte.Location = new Point(780, 15);
            btnExportarReporte.Name = "btnExportarReporte";
            btnExportarReporte.Size = new Size(75, 26);
            btnExportarReporte.TabIndex = 7;
            btnExportarReporte.Text = "Exportar";
            // 
            // dgvReportes
            // 
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.AllowUserToDeleteRows = false;
            dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReportes.Location = new Point(15, 50);
            dgvReportes.MultiSelect = false;
            dgvReportes.Name = "dgvReportes";
            dgvReportes.ReadOnly = true;
            dgvReportes.RowHeadersVisible = false;
            dgvReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.Size = new Size(840, 420);
            dgvReportes.TabIndex = 8;
            // 
            // tpAuditoria
            // 
            tpAuditoria.Controls.Add(lblFiltroUsuario);
            tpAuditoria.Controls.Add(txtFiltroUsuario);
            tpAuditoria.Controls.Add(lblAccion);
            tpAuditoria.Controls.Add(cbAccion);
            tpAuditoria.Controls.Add(lblAudDesde);
            tpAuditoria.Controls.Add(dtAudDesde);
            tpAuditoria.Controls.Add(lblAudHasta);
            tpAuditoria.Controls.Add(dtAudHasta);
            tpAuditoria.Controls.Add(btnFiltrarAud);
            tpAuditoria.Controls.Add(dgvAuditoria);
            tpAuditoria.Controls.Add(btnExportAud);
            tpAuditoria.Location = new Point(4, 24);
            tpAuditoria.Name = "tpAuditoria";
            tpAuditoria.Padding = new Padding(10);
            tpAuditoria.Size = new Size(886, 490);
            tpAuditoria.TabIndex = 3;
            tpAuditoria.Text = "Auditoría";
            // 
            // lblFiltroUsuario
            // 
            lblFiltroUsuario.Location = new Point(15, 18);
            lblFiltroUsuario.Name = "lblFiltroUsuario";
            lblFiltroUsuario.Size = new Size(55, 20);
            lblFiltroUsuario.TabIndex = 0;
            lblFiltroUsuario.Text = "Usuario:";
            // 
            // txtFiltroUsuario
            // 
            txtFiltroUsuario.Location = new Point(72, 16);
            txtFiltroUsuario.Name = "txtFiltroUsuario";
            txtFiltroUsuario.Size = new Size(140, 23);
            txtFiltroUsuario.TabIndex = 1;
            // 
            // lblAccion
            // 
            lblAccion.Location = new Point(225, 18);
            lblAccion.Name = "lblAccion";
            lblAccion.Size = new Size(50, 20);
            lblAccion.TabIndex = 2;
            lblAccion.Text = "Acción:";
            // 
            // cbAccion
            // 
            cbAccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAccion.Items.AddRange(new object[] { "Todas", "registro_nota", "registro_asistencia", "asignar_tarea", "login" });
            cbAccion.Location = new Point(278, 16);
            cbAccion.Name = "cbAccion";
            cbAccion.Size = new Size(140, 23);
            cbAccion.TabIndex = 3;
            // 
            // lblAudDesde
            // 
            lblAudDesde.Location = new Point(430, 18);
            lblAudDesde.Name = "lblAudDesde";
            lblAudDesde.Size = new Size(45, 20);
            lblAudDesde.TabIndex = 4;
            lblAudDesde.Text = "Desde:";
            // 
            // dtAudDesde
            // 
            dtAudDesde.Format = DateTimePickerFormat.Short;
            dtAudDesde.Location = new Point(480, 16);
            dtAudDesde.Name = "dtAudDesde";
            dtAudDesde.Size = new Size(110, 23);
            dtAudDesde.TabIndex = 5;
            // 
            // lblAudHasta
            // 
            lblAudHasta.Location = new Point(600, 18);
            lblAudHasta.Name = "lblAudHasta";
            lblAudHasta.Size = new Size(45, 20);
            lblAudHasta.TabIndex = 6;
            lblAudHasta.Text = "Hasta:";
            // 
            // dtAudHasta
            // 
            dtAudHasta.Format = DateTimePickerFormat.Short;
            dtAudHasta.Location = new Point(650, 16);
            dtAudHasta.Name = "dtAudHasta";
            dtAudHasta.Size = new Size(110, 23);
            dtAudHasta.TabIndex = 7;
            // 
            // btnFiltrarAud
            // 
            btnFiltrarAud.Location = new Point(770, 15);
            btnFiltrarAud.Name = "btnFiltrarAud";
            btnFiltrarAud.Size = new Size(75, 26);
            btnFiltrarAud.TabIndex = 8;
            btnFiltrarAud.Text = "Filtrar";
            // 
            // dgvAuditoria
            // 
            dgvAuditoria.AllowUserToAddRows = false;
            dgvAuditoria.AllowUserToDeleteRows = false;
            dgvAuditoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditoria.Location = new Point(15, 50);
            dgvAuditoria.MultiSelect = false;
            dgvAuditoria.Name = "dgvAuditoria";
            dgvAuditoria.ReadOnly = true;
            dgvAuditoria.RowHeadersVisible = false;
            dgvAuditoria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditoria.Size = new Size(840, 420);
            dgvAuditoria.TabIndex = 9;
            // 
            // btnExportAud
            // 
            btnExportAud.Location = new Point(770, 475);
            btnExportAud.Name = "btnExportAud";
            btnExportAud.Size = new Size(75, 26);
            btnExportAud.TabIndex = 10;
            btnExportAud.Text = "Exportar";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 518);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(894, 22);
            statusStrip.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(32, 17);
            lblStatus.Text = "Listo";
            // 
            // Administrador
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(894, 540);
            Controls.Add(tabMain);
            Controls.Add(statusStrip);
            Name = "Administrador";
            Text = "SISTEMA ACADÉMICO – ADMINISTRADOR";
            Load += Administrador_Load;
            tabMain.ResumeLayout(false);
            tpUsuarios.ResumeLayout(false);
            tpUsuarios.PerformLayout();
            grpUsuarioForm.ResumeLayout(false);
            grpUsuarioForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            tpAsignaturas.ResumeLayout(false);
            tpAsignaturas.PerformLayout();
            grpAsigForm.ResumeLayout(false);
            grpAsigForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAsignaturas).EndInit();
            tpReportes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReportes).EndInit();
            tpAuditoria.ResumeLayout(false);
            tpAuditoria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TabControl tabMain;
        private TabPage tpUsuarios;
        private TabPage tpAsignaturas;
        private TabPage tpReportes;
        private TabPage tpAuditoria;

        // Usuarios
        private GroupBox grpUsuarioForm;
        private Label lblCedula; private TextBox txtCedula;
        private Label lblNombres; private TextBox txtNombres;
        private Label lblApellidos; private TextBox txtApellidos;
        private Label lblCorreo; private TextBox txtCorreo;
        private Label lblRol; private ComboBox cbRol;
        private CheckBox chkEstadoUsuario;
        private Button btnUsrNuevo, btnUsrGuardar, btnUsrActualizar, btnUsrEliminar, btnUsrResetPass;
        private Label lblBuscarUsuario; private TextBox txtBuscarUsuario; private Button btnBuscarUsuario;
        private DataGridView dgvUsuarios;

        // Asignaturas
        private GroupBox grpAsigForm;
        private Label lblCodigo; private TextBox txtCodigo;
        private Label lblNombreAsig; private TextBox txtNombreAsig;
        private Label lblDescripcion; private TextBox txtDescripcion;
        private CheckBox chkEstadoAsig;
        private Button btnAsigNuevo, btnAsigGuardar, btnAsigActualizar, btnAsigEliminar;
        private Label lblBuscarAsig; private TextBox txtBuscarAsig; private Button btnBuscarAsig;
        private DataGridView dgvAsignaturas;

        // Reportes
        private Label lblTipoReporte; private ComboBox cbTipoReporte;
        private Label lblDesde; private DateTimePicker dtDesde;
        private Label lblHasta; private DateTimePicker dtHasta;
        private Button btnGenerarReporte, btnExportarReporte;
        private DataGridView dgvReportes;

        // Auditoría
        private Label lblFiltroUsuario; private TextBox txtFiltroUsuario;
        private Label lblAccion; private ComboBox cbAccion;
        private Label lblAudDesde; private DateTimePicker dtAudDesde;
        private Label lblAudHasta; private DateTimePicker dtAudHasta;
        private Button btnFiltrarAud, btnExportAud;
        private DataGridView dgvAuditoria;

        // Status
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
    }
}
