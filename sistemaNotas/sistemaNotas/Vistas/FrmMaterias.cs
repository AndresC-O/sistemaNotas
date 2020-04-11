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
    public partial class FrmMaterias : Form
    {
        public FrmMaterias()
        {
            InitializeComponent();
        }

        materia subject = new materia();

        void CargarDatos()
        {
            using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
            {
                var tbMateria = bd.materia;

                foreach(var iterarDatos in tbMateria)
                {
                    dtvMateria.Rows.Add(iterarDatos.idMateria, iterarDatos.nombreMateria);
                }
            }
        }

        void LimpiarDatos()
        {
            txtId.Text = "";
            txtMateria.Text = "";
        }

        private void FrmMaterias_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMateria.Text == "")
                {
                    MessageBox.Show("No se permiten incersiones en blanco, \ncomplete todos los campos.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
                    {
                        subject.nombreMateria = txtMateria.Text;

                        bd.materia.Add(subject);
                        bd.SaveChanges();
                    }

                    dtvMateria.Rows.Clear();
                    CargarDatos();
                    LimpiarDatos();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al Insertar: \n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void dtvMateria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnRegistrar.Enabled = false;

            String materia = dtvMateria.CurrentRow.Cells[1].Value.ToString();
            String id = dtvMateria.CurrentRow.Cells[0].Value.ToString();

            txtMateria.Text = materia;
            txtId.Text = id;

            this.btnActualizar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMateria.Text == "")
                {
                    MessageBox.Show("Para actualizar, primero seleccione \nuna materia en específica.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
                    {
                        String id = dtvMateria.CurrentRow.Cells[0].Value.ToString();

                        int idC = int.Parse(id);

                        subject = bd.materia.Where(VerificarId => VerificarId.idMateria == idC).First();
                        subject.nombreMateria = txtMateria.Text;

                        bd.Entry(subject).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                    }

                    dtvMateria.Rows.Clear();
                    CargarDatos();
                    LimpiarDatos();
                    this.btnRegistrar.Enabled = true;
                    //this.btnActualizar.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al Actualizar: \n\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
            this.btnActualizar.Enabled = false;
            this.btnRegistrar.Enabled = true;
        }
    }
}
