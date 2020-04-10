using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemaNotas.Vistas
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void AbrirFormulario(object formHijo)
        {
            
            if (this.panelContenedor.Controls.Count > 0)
            {
                this.panelContenedor.Controls.RemoveAt(0);
            }

            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            
            fh.Show();
            
        }

        private void datosEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label1.Visible = false;
            AbrirFormulario(new FrmEstudiantes());
        }

        private void mantenimientoDeMateriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label1.Visible = false;
            AbrirFormulario(new FrmMaterias());
        }

        private void ingresarNotasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label1.Visible = false;
            AbrirFormulario(new FrmNotas());
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult entrada = MessageBox.Show("¿Estás seguro de salir del programa?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if(entrada == DialogResult.OK)
            {
                Application.Exit();
            }
            
        }
    }
}
