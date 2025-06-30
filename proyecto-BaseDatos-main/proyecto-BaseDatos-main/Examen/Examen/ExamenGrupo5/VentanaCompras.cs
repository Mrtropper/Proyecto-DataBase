using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using BLL;
using DAL;

namespace ExamenGrupo5
{
    public partial class VentanaCompras : Form
    {
        private OracleConexion conexion;
        private PermisosVentana permisos;

        public VentanaCompras(PermisosVentana permisos)
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

        private void AplicarPermisosDataGrid()
        {
            if (dtgDatos.Columns.Contains("btnEditar"))
                dtgDatos.Columns["btnEditar"].Visible = permisos.PuedeActualizar;

            if (dtgDatos.Columns.Contains("btnEliminar"))
                dtgDatos.Columns["btnEliminar"].Visible = permisos.PuedeEliminar;
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


        private void CargarDatos()
        {
            dtgDatos.DataSource = conexion.BuscarPorEstadoCompra(txt_Estado_compra.Text).Tables[0];
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta la ventana de compras

            var gestion = new VentanaGestionCompras();
            gestion.FormClosed += (s, args) =>
            {
                this.Show();      // Vuelve a mostrar la ventana de compras
                CargarDatos();    // Recarga datos
            };
            gestion.ShowDialog(); // Espera hasta que se cierre
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            if (dtgDatos.SelectedRows.Count > 0)
            {
                int idCompra = Convert.ToInt32(dtgDatos.SelectedRows[0].Cells["IDCompra"].Value);
                Compra compra = conexion.MostrarIDCompra(idCompra);

                if (compra != null)
                {
                    this.Hide();
                    var ventana = new VentanaGestionCompras(compra);
                    ventana.FormClosed += (s, args) =>
                    {
                        this.Show();
                        CargarDatos();
                    };
                    ventana.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Compra no encontrada.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para editar la compra.");
            }
        }

        private void BuscarCompraPorEstado(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void EditarCompraDesdeGrid(int idCompra)
        {
            Compra compra = conexion.MostrarIDCompra(idCompra);
            if (compra != null)
            {
                this.Hide();
                var ventana = new VentanaGestionCompras(compra);
                ventana.FormClosed += (s, args) =>
                {
                    this.Show();
                    CargarDatos();
                };
                ventana.ShowDialog();
            }
            else
            {
                MessageBox.Show("Compra no encontrada.");
            }
        }

        private void EliminarCompraDesdeGrid(int idCompra)
        {
            Compra compra = conexion.MostrarIDCompra(idCompra);
            if (compra != null)
            {
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta compra?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    conexion.EliminarCompra(compra.IDCompra);
                    CargarDatos();
                    MessageBox.Show("Compra eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Compra no encontrada.");
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (dtgDatos.SelectedRows.Count > 0)
            {
                int idCompra = Convert.ToInt32(dtgDatos.SelectedRows[0].Cells["IDCompra"].Value);
                Compra compra = conexion.MostrarIDCompra(idCompra);

                if (compra != null)
                {
                    // Confirmación antes de eliminar
                    DialogResult confirmacion = MessageBox.Show(
                        "¿Está seguro de que desea eliminar esta compra?",
                        "Confirmación de eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmacion == DialogResult.Yes)
                    {
                        conexion.EliminarCompra(compra.IDCompra);
                        dtgDatos.DataSource = conexion.BuscarPorEstadoCompra(txt_Estado_compra.Text).Tables[0];
                        MessageBox.Show("Compra eliminada correctamente.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Compra no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar la compra.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
        }

        private void ShowToolTipOnMouseUp(PictureBox pictureBox, string message)
        {
            ToolTip toolTip = new ToolTip();
            pictureBox.MouseUp += (sender, e) =>
            {
                toolTip.SetToolTip(pictureBox, message);
            };
        }


        private void btn_Imprimir_Click_1(object sender, EventArgs e)
        {
  
        }

        private void dtgDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string col = dtgDatos.Columns[e.ColumnIndex].Name;
            int idCompra = Convert.ToInt32(dtgDatos.Rows[e.RowIndex].Cells["IDCompra"]?.Value);

            if (col == "btnEditar" && permisos.PuedeActualizar)
            {
                EditarCompraDesdeGrid(idCompra);
            }
            else if (col == "btnEliminar" && permisos.PuedeEliminar)
            {
                EliminarCompraDesdeGrid(idCompra);
            }
        }
    }
}
