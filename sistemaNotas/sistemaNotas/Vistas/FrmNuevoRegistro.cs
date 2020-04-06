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

namespace sistemaNotas.Vistas
{
    public partial class FrmNuevoRegistro : Form
    {
        public FrmNuevoRegistro()
        {
            InitializeComponent();
        }

        estudiante student = new estudiante();

        void Limpiar()
        {
            txtNombres.Text = null;
            txtApellidos.Text = null;
            txtUsuario.Text = null;
            txtPassword.Text = null;
            txtVerifyPass.Text = null;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(txtNombres.Text=="" || txtApellidos.Text=="" || txtUsuario.Text=="" || txtPassword.Text=="" || txtVerifyPass.Text == "")
            {
                MessageBox.Show("Todos los campos son requeridos.", "Completar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtPassword.Text == txtVerifyPass.Text)
                {
                    using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
                    {
                        student.nombresEstudiante = txtNombres.Text;
                        student.apellidos = txtApellidos.Text;
                        student.usuario = txtUsuario.Text;
                        student.contrasenia = txtPassword.Text;

                        bd.estudiante.Add(student);
                        bd.SaveChanges();
                    }

                    Limpiar();
                    FrmMenuPrincipal log = new FrmMenuPrincipal();
                    MessageBox.Show("¡Te Damos la Bienvenida!");
                    log.Show();
                    this.Hide();
                    //Preguntar como validar 
                }
                else
                {
                    MessageBox.Show("¡Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmLoguin log = new FrmLoguin();
            log.Show();
            this.Close();
        }
    }
}
