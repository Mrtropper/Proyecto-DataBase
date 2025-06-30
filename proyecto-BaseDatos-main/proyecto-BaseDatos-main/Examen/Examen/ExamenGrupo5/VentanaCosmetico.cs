using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL;
using DAL;

namespace ExamenGrupo5
{
    public partial class VentanaCosmetico : Form
    {
        private OracleConexion conexion;
        private PermisosVentana permisos;
 

        public VentanaCosmetico(PermisosVentana permisos)
        {
            InitializeComponent();
            this.permisos = permisos;

            string connectionString = ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString;
            conexion = new OracleConexion(connectionString);

            CargarDatos();

            btn_agregar.Visible = permisos.PuedeCrear;
            dtgDatos.Enabled = permisos.PuedeLeer;

            AgregarBotonesSiNoExisten();
            AplicarPermisosDataGrid();
        }

        private void CargarDatos()
        {
            dtgDatos.DataSource = conexion.BuscarPorNombreCosmeticos(txt_Nombre_Producto.Text).Tables[0];
        }

        private void Agregar_click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana actual

            var agregar = new VentanaAgregarCosmetico();
            agregar.FormClosed += (s, args) =>
            {
                this.Show();      // La vuelve a mostrar al cerrar
                CargarDatos();    // Recarga la grilla
            };

            agregar.ShowDialog();
        }

        private void EliminarCosmeticoDesdeGrid(string nombre)
        {
            Cosmetico cosmetico = conexion.BuscarPorNombreCosmetico(nombre);
            if (cosmetico != null)
            {
                if (conexion.CosmeticoVendido(cosmetico.IDCosmetico))
                {
                    MessageBox.Show("El cosmético ha sido vendido. Se marcará como 'Inactivo' en lugar de eliminarlo.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cosmetico.EstadoProducto = "Inactiva";
                    conexion.ModificarCosmetico(cosmetico);
                }
                else
                {
                    conexion.EliminarCosmetico(cosmetico.IDCosmetico);
                }

                CargarDatos();
            }
            else
            {
                MessageBox.Show("Cosmético no encontrado.");
            }
        }

        private void EditarCosmeticoDesdeGrid(string nombre)
        {
            Cosmetico cosmetico = conexion.BuscarPorNombreCosmetico(nombre);
            if (cosmetico != null)
            {
                this.Hide();

                var ventana = new VentanaAgregarCosmetico(cosmetico);
                ventana.FormClosed += (s, args) =>
                {
                    this.Show();
                    CargarDatos();
                };

                ventana.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cosmético no encontrado.");
            }
        }

        private void dtgDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string col = dtgDatos.Columns[e.ColumnIndex].Name;
            string nombre = dtgDatos.Rows[e.RowIndex].Cells["nombreProducto"]?.Value?.ToString()?.Trim();

            if (string.IsNullOrEmpty(nombre)) return;

            if (col == "btnEditar" && permisos.PuedeActualizar)
            {
                EditarCosmeticoDesdeGrid(nombre);
            }
            else if (col == "btnEliminar" && permisos.PuedeEliminar)
            {
                EliminarCosmeticoDesdeGrid(nombre);
            }
        }

        private void BuscarNombreCosmetico(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btn_salir(object sender, EventArgs e)
        {
            Close();
        }

        private void AgregarBotonesSiNoExisten()
        {
            if (!dtgDatos.Columns.Contains("btnEditar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "btnEditar",
                    HeaderText = "Editar",
                    Text = "Editar",
                    UseColumnTextForButtonValue = true
                };
                dtgDatos.Columns.Add(btnEditar);
            }

            if (!dtgDatos.Columns.Contains("btnEliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
                {
                    Name = "btnEliminar",
                    HeaderText = "Eliminar",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true
                };
                dtgDatos.Columns.Add(btnEliminar);
            }
        }

        private void AplicarPermisosDataGrid()
        {
            if (dtgDatos.Columns.Contains("btnEditar"))
                dtgDatos.Columns["btnEditar"].Visible = permisos.PuedeActualizar;

            if (dtgDatos.Columns.Contains("btnEliminar"))
                dtgDatos.Columns["btnEliminar"].Visible = permisos.PuedeEliminar;
        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            // Implementar si se desea imprimir reporte
        }

        public void MostrarInforme()
        {
            try
            {
                // Implementar si se desea mostrar informe
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar informe: " + ex.Message);
            }
        }

        // Métodos que conectan los ToolStripMenuItems
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtgDatos.SelectedRows.Count > 0)
            {
                string nombre = dtgDatos.SelectedRows[0].Cells["nombreProducto"].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(nombre) && permisos.PuedeActualizar)
                {
                    EditarCosmeticoDesdeGrid(nombre);
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dtgDatos.SelectedRows.Count > 0)
            {
                string nombre = dtgDatos.SelectedRows[0].Cells["nombreProducto"].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(nombre) && permisos.PuedeEliminar)
                {
                    EliminarCosmeticoDesdeGrid(nombre);
                }
            }
        }
    }
}
