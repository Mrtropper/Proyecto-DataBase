using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using BLL;
using DAL;

namespace ExamenGrupo5
{
    public partial class VentanaGestionCompras : Form
    {
        private OracleConexion conexion = null;
        private Compra compraActual;
        private Conexion _conexionServer;

        public VentanaGestionCompras()
        {
            InitializeComponent();
            conexion = new OracleConexion(ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString);
            _conexionServer = new Conexion(ConfigurationManager.ConnectionStrings["serverSystem"].ConnectionString);
        }

        // Constructor para edición de compra
        public VentanaGestionCompras(Compra compra) : this()
        {
            conexion = new OracleConexion(ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString);
            _conexionServer = new Conexion(ConfigurationManager.ConnectionStrings["serverSystem"].ConnectionString);
            compraActual = compra;
            CargarDatosCompra();
        }

        private void CargarDatosCompra()
        {
            if (compraActual != null)
            {
                dtpFechaCompra.Value = compraActual.FechaCompra;
                spTotalCompra.Text = compraActual.TotalCompra.ToString();
                cbMetodoPago.Text = compraActual.MetodoPago;
                cbProveedor.Text = compraActual.Proveedor;
                numericUpDownCantidad.Value = compraActual.CantidadProductos;
                cbEstado.Text = compraActual.EstadoCompra;
                txtIDCosmetico.Text = compraActual.IDCosmeticos.ToString();
            }
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return; // Si la validación falla, detener el guardado

            try
            {
                Compra compra = new Compra
                {
                    FechaCompra = dtpFechaCompra.Value,
                    TotalCompra = Convert.ToDouble(spTotalCompra.Text),
                    MetodoPago = cbMetodoPago.Text,
                    Proveedor = cbProveedor.Text,
                    CantidadProductos = (int)numericUpDownCantidad.Value,
                    EstadoCompra = cbEstado.Text,
                    IDCosmeticos = Convert.ToInt32(txtIDCosmetico.Text)
                };

                if (compraActual == null)
                {
                    // Nueva compra
                    conexion.GuardarCompra(compra);
                    _conexionServer.GuardarCompra(compra);
                    MessageBox.Show("Compra agregada correctamente.");
                }
                else
                {
                    // Actualizar compra existente
                    compra.IDCompra = compraActual.IDCompra;
                    conexion.ModificarCompra(compra);
                    MessageBox.Show("Compra actualizada correctamente.");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidarCampos()
        {
            List<string> mensajesError = new List<string>();

            // Verificar que los campos de texto no estén vacíos
            if (string.IsNullOrWhiteSpace(spTotalCompra.Text))
                mensajesError.Add("El campo de total de compra no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(txtIDCosmetico.Text))
                mensajesError.Add("Debe ingresar un ID de cosmético.");

            // Verificar que los ComboBox tengan una opción seleccionada
            if (cbMetodoPago.SelectedIndex == -1)
                mensajesError.Add("Por favor, seleccione un método de pago.");

            if (cbProveedor.SelectedIndex == -1)
                mensajesError.Add("Por favor, seleccione un proveedor.");

            if (cbEstado.SelectedIndex == -1)
                mensajesError.Add("Por favor, seleccione un estado de compra.");

            // Verificar que la cantidad de productos sea mayor a 0
            if (numericUpDownCantidad.Value <= 0)
                mensajesError.Add("La cantidad de productos debe ser mayor a 0.");

            // Si hay errores, mostrar un solo MessageBox con todos los errores
            if (mensajesError.Count > 0)
            {
                MessageBox.Show("Errores de validación:\n\n" + string.Join("\n", mensajesError),
                                "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Indicar que la validación falló
            }

            return true; // Todo está correcto
        }


        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Agregar_click(object sender, EventArgs e)
        {
            new VentanaGestionCompras().ShowDialog();
        }

        private void btn_salir(object sender, EventArgs e)
        {
            Close();
        }
    }
}
