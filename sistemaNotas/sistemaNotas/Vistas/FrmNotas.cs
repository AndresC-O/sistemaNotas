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
    public partial class FrmNotas : Form
    {
        public FrmNotas()
        {
            InitializeComponent();
        }

        notas grade = new notas();

        void CargarDatos()
        {
            using (notasEstudiantesEntities bd = new notasEstudiantesEntities())
            {
                var joinTablas = from tbNotas in bd.notas
                                 from tbEstudiante in bd.estudiante
                                 from tbMateria in bd.materia
                                 where tbNotas.idEstudiante == tbEstudiante.idEstudiante
                                 && tbNotas.idMateria == tbMateria.idMateria

                                 select new
                                 {
                                     Id = tbNotas.idNotas,
                                     Nombre = tbEstudiante.nombresEstudiante,
                                     Materia = tbMateria.nombreMateria,
                                     Nota = tbNotas.nota
                                 };

                foreach(var iterarDatos in joinTablas)
                {
                    dtvNotas.Rows.Add(iterarDatos.Id, iterarDatos.Nombre, iterarDatos.Materia, iterarDatos.Nota);
                }
            }
        }

        void LimpiarDatos()
        {
            txtIdEstudiante.Text = "";
            txtIdMateria.Text = "";
            txtNota.Text = "";
        }

        private void FrmNotas_Load(object sender, EventArgs e)
        {
            this.txtIdEstudiante.Enabled = false;
            this.txtIdMateria.Enabled = false;
            this.txtNota.Enabled = false;

            this.dtvNotas.Enabled = false;

            CargarDatos();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {


            if (txtIdEstudiante.Text == "" || txtIdMateria.Text == "" || txtNota.Text == "")
            {
                MessageBox.Show("No se permiten incersiones en blanco, \ncomplete todos los campos.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using(notasEstudiantesEntities bd = new notasEstudiantesEntities())
                {
                    grade.idEstudiante = int.Parse(txtIdEstudiante.Text);
                    grade.idMateria = int.Parse(txtIdMateria.Text);
                    grade.nota = double.Parse(txtNota.Text);

                    bd.notas.Add(grade);
                    bd.SaveChanges();
                }

                dtvNotas.Rows.Clear();
                CargarDatos();
                LimpiarDatos();

                this.btnNuevo.Enabled = true;
                this.btnRegistrar.Enabled = false;
                this.btnActualizar.Enabled = false;
                this.btnEliminar.Enabled = false;
            }
        }

        private void dtvNotas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtIdEstudiante.Enabled = false;
            this.txtIdMateria.Enabled = false;
            this.btnRegistrar.Enabled = false;
            this.btnNuevo.Enabled = true;
            this.btnEliminar.Enabled = true;
            this.btnActualizar.Enabled = true;

            String idEstudiante = dtvNotas.CurrentRow.Cells[1].Value.ToString();
            String idMateria = dtvNotas.CurrentRow.Cells[2].Value.ToString();
            String nota = dtvNotas.CurrentRow.Cells[3].Value.ToString();

            txtIdEstudiante.Text = idEstudiante;
            txtIdMateria.Text = idMateria;
            txtNota.Text = nota;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtIdEstudiante.Text == "" || txtIdMateria.Text == "" || txtNota.Text == "")
            {
                MessageBox.Show("Para actualizar, primero seleccione \nun id en específico.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using(notasEstudiantesEntities bd = new notasEstudiantesEntities())
                {
                    String id = dtvNotas.CurrentRow.Cells[0].Value.ToString();

                    int idC = int.Parse(id);

                    grade = bd.notas.Where(VerificarId => VerificarId.idNotas == idC).First();
                    //grade.idEstudiante = int.Parse(txtIdEstudiante.Text);
                    //grade.idMateria = int.Parse(txtIdMateria.Text);
                    grade.nota = double.Parse(txtNota.Text);

                    bd.Entry(grade).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                }

                dtvNotas.Rows.Clear();
                CargarDatos();
                LimpiarDatos();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarDatos();

            this.txtIdEstudiante.Enabled = true;
            this.txtIdMateria.Enabled = true;
            this.txtNota.Enabled = true;

            this.btnNuevo.Enabled = false;
            this.btnRegistrar.Enabled = true;
            this.btnActualizar.Enabled = false;
            this.btnEliminar.Enabled = false;

            this.dtvNotas.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtIdEstudiante.Text == "" || txtIdMateria.Text == "" || txtNota.Text == "")
            {
                MessageBox.Show("Para eliminar, primero seleccione \nun id en específico.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                using(notasEstudiantesEntities bd = new notasEstudiantesEntities())
                {
                    String id = dtvNotas.CurrentRow.Cells[0].Value.ToString();

                    grade = bd.notas.Find(int.Parse(id));
                    bd.notas.Remove(grade);
                    bd.SaveChanges();
                }

                dtvNotas.Rows.Clear();
                CargarDatos();
                LimpiarDatos();
            }
        }
    }
}
