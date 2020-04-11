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
    public partial class FrmEstudiantes : Form
    {
        public FrmEstudiantes()
        {
            InitializeComponent();
        }

        estudiante student = new estudiante();

        void CargarDatos()
        {
            using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
            {
                var tbEstudiantes = bd.estudiante;

                foreach(var iterarDatos in tbEstudiantes)
                {
                    dtvEstudiantes.Rows.Add(iterarDatos.idEstudiante, iterarDatos.nombresEstudiante, iterarDatos.apellidos, iterarDatos.usuario, iterarDatos.contrasenia);
                }
            }
        }
        void LimpiarDatos()
        {
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtUsuario.Text = "";
            txtPassword.Text = "";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombres.Text == "" || txtApellidos.Text == "" || txtUsuario.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("No se permiten incersiones en blanco, \ncomplete todos los campos.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
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

                    dtvEstudiantes.Rows.Clear();
                    CargarDatos();
                    LimpiarDatos();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al Insertar: \n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombres.Text == "" || txtApellidos.Text == "" || txtUsuario.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Para actualizar, primero seleccione \nun estudiante en específico.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
                    {
                        String id = dtvEstudiantes.CurrentRow.Cells[0].Value.ToString();

                        int idC = int.Parse(id);

                        student = bd.estudiante.Where(VerificarId => VerificarId.idEstudiante == idC).First();
                        student.nombresEstudiante = txtNombres.Text;
                        student.apellidos = txtApellidos.Text;
                        student.usuario = txtUsuario.Text;
                        student.contrasenia = txtPassword.Text;

                        bd.Entry(student).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                    }

                    dtvEstudiantes.Rows.Clear();
                    CargarDatos();
                    LimpiarDatos();
                    //this.btnActualizar.Enabled = false;
                    this.btnRegistrar.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al Actualizar: \n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmEstudiantes_Load(object sender, EventArgs e)
        {
            CargarDatos();
            this.btnActualizar.Enabled = false;
        }

        private void dtvEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String nombres = dtvEstudiantes.CurrentRow.Cells[1].Value.ToString();
            String apellidos = dtvEstudiantes.CurrentRow.Cells[2].Value.ToString();
            String usuario = dtvEstudiantes.CurrentRow.Cells[3].Value.ToString();
            String password = dtvEstudiantes.CurrentRow.Cells[4].Value.ToString();

            txtNombres.Text = nombres;
            txtApellidos.Text = apellidos;
            txtUsuario.Text = usuario;
            txtPassword.Text = password;

            this.btnRegistrar.Enabled = false;
            this.btnActualizar.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
            this.btnActualizar.Enabled = false;
            this.btnRegistrar.Enabled = true;
        }
    }
}
