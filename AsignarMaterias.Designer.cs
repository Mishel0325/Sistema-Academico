using System.Windows.Forms;

namespace Sistema_Academico
{
    partial class AsignarMaterias
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private CheckedListBox clbMaterias;
        private Button btnAsignar;
        private Button btnCancelar;
        private TextBox txtBuscar;
        private Label lblBuscar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.clbMaterias = new System.Windows.Forms.CheckedListBox();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(560, 18);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Selecciona una o varias asignaturas:";
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(12, 37);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(43, 13);
            this.lblBuscar.TabIndex = 5;
            this.lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(61, 33);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(511, 20);
            this.txtBuscar.TabIndex = 1;
            // 
            // clbMaterias
            // 
            this.clbMaterias.CheckOnClick = true;
            this.clbMaterias.FormattingEnabled = true;
            this.clbMaterias.Location = new System.Drawing.Point(12, 59);
            this.clbMaterias.Name = "clbMaterias";
            this.clbMaterias.Size = new System.Drawing.Size(560, 364);
            this.clbMaterias.TabIndex = 2;
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(386, 434);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(90, 28);
            this.btnAsignar.TabIndex = 3;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(482, 434);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 28);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // AsignarMaterias
            // 
            this.AcceptButton = this.btnAsignar;
            this.CancelButton = this.btnCancelar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 474);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.clbMaterias);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsignarMaterias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asignar Materias";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}