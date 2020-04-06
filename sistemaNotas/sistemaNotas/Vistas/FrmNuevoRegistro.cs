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

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text == txtVerifyPass.Text)
            {
                //MessageBox.Show("Verify Sucesfully!!");
                using(notasEstudiantesEntities bd = new notasEstudiantesEntities())
                {
                    student.nombresEstudiante = txtNombres.Text;
                    student.apellidos = txtApellidos.Text;
                    student.usuario = txtUsuario.Text;
                    student.contrasenia = txtPassword.Text;

                    bd.estudiante.Add(student);
                    bd.SaveChanges();
                }

                //Preguntar como validar si o no al Ingeniero.
            }
            //Agregar Else-If Para verificar Campos Vacios.
            else
            {
                MessageBox.Show("¡Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
