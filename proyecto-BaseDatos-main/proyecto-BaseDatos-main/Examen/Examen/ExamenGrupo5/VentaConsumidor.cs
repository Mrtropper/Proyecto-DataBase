using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace ExamenGrupo5
{
    public partial class VentanaConsumidor : Form
    {
        private OracleConexion _conexion;
        private PermisosVentana _permisos;
        public VentanaConsumidor(PermisosVentana permisos)
        {
            InitializeComponent();
            _permisos = permisos;

            string connectionString = ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString;
            _conexion = new OracleConexion(connectionString);

            ShowToolTipOnMouseUp(pictureBox1, "Actualizar");
            ConfigurarInterfaz();
            Buscar("");
        }
        private void ConfigurarInterfaz()
        {
            btn_agregar.Visible = _permisos.PuedeCrear;
            dtgDatos.Enabled = _permisos.PuedeLeer;
        }

        private void ImprimirBTN_Click(object sender, EventArgs e)
        {

        }

        // Método para mostrar el informe de usuarios en un formulario
        public void MostrarInforme()
        {
            try
            {
                // Se crea una instancia del formulario FrmRepConsumidores
                //FrmRepConsumidores formulario = new FrmRepConsumidores();

                //// Se actualiza la lista de usuarios cuando se cierra el formulario
                //formulario.FormClosed += (s, args) => Buscar("");

                //// Se muestra el formulario de informe de usuarios en un diálogo modal
                //formulario.ShowDialog();

                //// Se libera la memoria del formulario después de cerrarlo
                //formulario.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {

        }

        private void btn_Volver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (!_permisos.PuedeActualizar)
            {
                MessageBox.Show("No tiene permisos para editar.");
                return;
            }

            try
            {
                if (dtgDatos.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dtgDatos.SelectedRows[0].Cells["IDCONSUMIDOR"].Value);
                    MostrarVentanaConsumidor("Modificar", id);
                }
                else
                {
                    MessageBox.Show("Seleccione una fila para editar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        // Evento para eliminar un usuario seleccionado en el DataGridView
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_permisos.PuedeEliminar)
            {
                MessageBox.Show("No tiene permisos para eliminar.");
                return;
            }

            try
            {
                if (dtgDatos.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dtgDatos.SelectedRows[0].Cells["IDCONSUMIDOR"].Value);
                    _conexion.EliminarConsumidor(id);
                    Buscar("");
                    MessageBox.Show("Consumidor eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("Seleccione una fila para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }


        private void txt_Nombre_Completo_TextChanged(object sender, EventArgs e)
        {
            Buscar(txt_Nombre_Completo.Text.Trim());

        }

        private void ShowToolTipOnMouseUp(PictureBox pictureBox, string message)
        {
            ToolTip toolTip = new ToolTip();
            pictureBox.MouseUp += (sender, e) =>
            {
                toolTip.SetToolTip(pictureBox, message);
            };
        }

        private void Buscar(string nombre)
        {
            try
            {
                dtgDatos.DataSource = _conexion.BuscarPorNombreConsumidor(nombre).Tables[0];
                dtgDatos.AutoResizeColumns();
                dtgDatos.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar consumidores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarVentanaConsumidor("Crear", 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ventana: " + ex.Message);
            }
        }

    

        private void MostrarVentanaConsumidor(string funcion, int id)
        {
            try
            {
                VentanaGestionConsumidor ventana = new VentanaGestionConsumidor(funcion, id);
                var result = ventana.ShowDialog(); 

                this.Buscar(""); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dtgDatos.SelectedRows.Count > 0)
                {
                    // Se procede a eliminar los datos del usuario
                    _conexion.EliminarConsumidor(int.Parse(this.dtgDatos.SelectedRows[0].Cells["IdConsumidor"].Value.ToString()));

                    // Se actualiza la lista de usuarios
                    this.Buscar("");
                }
                else
                {
                    throw new Exception("Seleccione la fila del usuario que desea eliminar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarInforme();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar informe: " + ex.Message);
            }
        }


        private void btn_Imprimir_Click_1(object sender, EventArgs e)
        {
     
        }

    }
}
