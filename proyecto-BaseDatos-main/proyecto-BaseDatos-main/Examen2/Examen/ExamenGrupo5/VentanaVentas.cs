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
using BLL;
using DAL;

namespace ExamenGrupo5
{
    public partial class VentanaVentas : Form
    {
        private OracleConexion conexion;
        private PermisosVentana permisos;

        public VentanaVentas(PermisosVentana permisos)
        {
            InitializeComponent();
            this.permisos = permisos;

            string connectionString = ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString;
            conexion = new OracleConexion(connectionString);

            cbEstadoVenta.SelectedIndex = 0;
            CargarDatos();

            button3.Visible = permisos.PuedeCrear;
            dtgTablaDatos.Enabled = permisos.PuedeLeer;

            AgregarBotonesSiNoExisten();
            AplicarPermisosDataGrid();
        }
        private void AgregarBotonesSiNoExisten()
        {
            if (!dtgTablaDatos.Columns.Contains("btnEditar"))
            {
                DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn
                {
                    Name = "btnEditar",
                    HeaderText = "Editar",
                    Text = "Editar",
                    UseColumnTextForButtonValue = true
                };
                dtgTablaDatos.Columns.Add(btnEditar);
            }

            if (!dtgTablaDatos.Columns.Contains("btnEliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn
                {
                    Name = "btnEliminar",
                    HeaderText = "Eliminar",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true
                };
                dtgTablaDatos.Columns.Add(btnEliminar);
            }
        }
        private void AplicarPermisosDataGrid()
        {
            if (dtgTablaDatos.Columns.Contains("btnEditar"))
                dtgTablaDatos.Columns["btnEditar"].Visible = permisos.PuedeActualizar;

            if (dtgTablaDatos.Columns.Contains("btnEliminar"))
                dtgTablaDatos.Columns["btnEliminar"].Visible = permisos.PuedeEliminar;
        }

        private void CargarDatos()
        {
            string estado = cbEstadoVenta.SelectedItem?.ToString() ?? "";
            dtgTablaDatos.DataSource = conexion.BuscarPorEstadoVenta(estado).Tables[0];
        }

        private void Salir(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowToolTipOnMouseUp(PictureBox pictureBox, string message)
        {
            ToolTip toolTip = new ToolTip();
            pictureBox.MouseUp += (sender, e) =>
            {
                toolTip.SetToolTip(pictureBox, message);
            };
        }

        private void EstadoVentaChanged(object sender, EventArgs e)
        {

            dtgTablaDatos.DataSource = conexion.BuscarPorEstadoVenta(cbEstadoVenta.SelectedItem.ToString()).Tables[0];
        }

        private void Agregar_click(object sender, EventArgs e)
         {
            this.Hide();

            var agregar = new VentanaAgregarVenta();
            agregar.FormClosed += (s, args) =>
            {
                this.Show();
                CargarDatos();
            };

            agregar.ShowDialog();

        }

        private void BuscarNombreCompra(object sender, EventArgs e)
        {

        }

        private void TbEditar(object sender, EventArgs e)
        {
            if (dtgTablaDatos.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dtgTablaDatos.SelectedRows[0];

                if (filaSeleccionada.Cells["IdVenta"] != null && filaSeleccionada.Cells["IdVenta"].Value != DBNull.Value)
                {
                    int ID;
                    bool conversionExitosa = int.TryParse(filaSeleccionada.Cells["IdVenta"].Value.ToString(), out ID);

                    if (conversionExitosa)
                    {
                        Venta venta = conexion.MostrarIDVenta(ID);
                        venta.IdVenta = ID;

                        if (venta != null)
                        {
                            // ✅ CORREGIDO: Ocultar y volver a mostrar
                            this.Hide();
                            var ventana = new VentanaAgregarVenta(venta);
                            ventana.FormClosed += (s, args) =>
                            {
                                this.Show();
                                CargarDatos();
                            };
                            ventana.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Venta no encontrada.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El ID de la venta no es válido.");
                    }
                }
                else
                {
                    MessageBox.Show("La celda 'IdVenta' está vacía o no existe en la tabla.");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para editar.");
            }
        }


        private void Eliminar(object sender, EventArgs e)
        {
            if (dtgTablaDatos.SelectedRows.Count > 0)
            {
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta compra?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    int ID = Convert.ToInt32(dtgTablaDatos.SelectedRows[0].Cells["IdVenta"].Value);
                    conexion.EliminarVenta(ID);
                    dtgTablaDatos.DataSource = conexion.BuscarPorEstadoVenta(cbEstadoVenta.SelectedItem.ToString()).Tables[0];
                }
            }
            else
            {
                MessageBox.Show("Seleccione la fila entera para poder eliminar una compra.");
            }
        }

        private void btn_Imprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnRefrescar(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void dtgTablaDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string col = dtgTablaDatos.Columns[e.ColumnIndex].Name;
            int idVenta = Convert.ToInt32(dtgTablaDatos.Rows[e.RowIndex].Cells["IdVenta"].Value);

            if (col == "btnEditar" && permisos.PuedeActualizar)
            {
                Venta venta = conexion.MostrarIDVenta(idVenta);
                if (venta != null)
                {
                    this.Hide();

                    var ventana = new VentanaAgregarVenta(venta);
                    ventana.FormClosed += (s, args) =>
                    {
                        this.Show();
                        CargarDatos();
                    };

                    ventana.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Venta no encontrada.");
                }
            }
            else if (col == "btnEliminar" && permisos.PuedeEliminar)
            {
                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta venta?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion == DialogResult.Yes)
                {
                    conexion.EliminarVenta(idVenta);
                    CargarDatos();
                }
            }
        }
    }
}
