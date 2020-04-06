using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemaNotas.Model;
using sistemaNotas.Vistas;

namespace sistemaNotas
{
    public partial class FrmLoguin : Form
    {
        public FrmLoguin()
        {
            InitializeComponent();
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == true)
            {
                if(txtPassword.PasswordChar == '•')
                {
                    txtPassword.PasswordChar = '\0';
                }
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Todos los campos son requeridos.", "Completar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
                {
                    var lista = from estudiante in bd.estudiante
                                where estudiante.usuario == txtUsuario.Text
                                && estudiante.contrasenia == txtPassword.Text
                                select estudiante;

                    if (lista.Count() > 0)
                    {
                        FrmMenuPrincipal menu = new FrmMenuPrincipal();
                        menu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("¡El usuario no existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void linklblNuevoRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmNuevoRegistro newR = new FrmNuevoRegistro();
            newR.Show();
            this.Hide();
        }
    }
}
