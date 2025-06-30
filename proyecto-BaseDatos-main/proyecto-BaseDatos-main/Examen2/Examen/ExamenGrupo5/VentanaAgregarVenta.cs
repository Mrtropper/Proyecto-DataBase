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
    public partial class VentanaAgregarVenta : Form
    {

        private Venta _venta= null;
        private OracleConexion _conexion;
        private bool edita = false;
        
        public VentanaAgregarVenta()
        {
            InitializeComponent();
            edita = false;
            string connectionString = ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString;

            _conexion = new OracleConexion(connectionString);

        }

        public VentanaAgregarVenta(Venta venta)            
        {
            _venta = venta;
            InitializeComponent();
            edita = true;
            string connectionString = ConfigurationManager.ConnectionStrings["OracleSistem"].ConnectionString;
            _conexion = new OracleConexion(connectionString);
            cargarDatos(_venta);
            txtPrecioTotal.Text = (venta.CantidadVendido * _conexion.BuscarPorIdCosmetico(venta.IDCosmetico).PrecioUnitario).ToString();
        }

        private void CargarComboBoxCosmeticos()
        {
            try
            {
                
                DataSet dsCosmeticos = _conexion.CosmeticosId();

                cbIdCosmetico.DataSource = dsCosmeticos.Tables[0];
                cbIdCosmetico.DisplayMember = "IDCosmetico";  // Muestra los ID en el ComboBox
                cbIdCosmetico.ValueMember = "IDCosmetico"; // Guarda el ID como valor

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los ID de los cosméticos: " + ex.Message);
            }
        }
        
        private void CargarComboBoxConsumidores()
        {
            try
            {
                
                DataSet dsConsumidores = _conexion.ObtenerIDConsumidores();

                cbIdConsumidor.DataSource = dsConsumidores.Tables[0];
                cbIdConsumidor.DisplayMember = "IdConsumidor";  // Muestra los ID en el ComboBox
                cbIdConsumidor.ValueMember = "IdConsumidor"; // Guarda el ID como valor
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los ID de los consumidores: " + ex.Message);
            }
        }

        private void GuardarDatos()
        {
            double modificarPuntos = 0;
            try
            {
               

                if (cbIdCosmetico.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un cosmético.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbIdConsumidor.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un consumidor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbMetodoPago.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un método de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (cbEstadoVentas.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un estado para la venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // obtiene al consumidor y al cosmetico que se esta seleccionando
                Cosmetico cosmetico = _conexion.BuscarPorIdCosmetico(int.Parse(cbIdCosmetico.SelectedValue.ToString()));
                Consumidor consumidor = _conexion.BuscarPorIdConsumidor(int.Parse(cbIdConsumidor.SelectedValue.ToString()));


                // ⚠ Validación de existencia
                if (cosmetico == null)
                {
                    MessageBox.Show("El cosmético seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //este es la primera consideracion logica.
                if (cosmetico.FechaVencimiento <= DateTime.Today)
                {
                    MessageBox.Show("El producto que desea comprar está vencido, no se puede realizar la venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int cantidadVendida = (int)pkCantidadVendidos.Value;

                // ✅ 3️⃣ Validar si hay suficiente stock
                if (cosmetico.StockDisponible < cantidadVendida)
                {
                    MessageBox.Show($"No hay suficiente stock. Disponible: {cosmetico.StockDisponible}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               

                // ✅ 4️⃣ Calcular total de la venta
                double totalVenta = cosmetico.PrecioUnitario * cantidadVendida;

                //se le apllica al un descuento de 15% al consumidor que es VIP y un 10% al consumidor frecuente
                //este es la segunda consideracion logica.
                switch (consumidor.FrecuenciaCompra)
                {
                    case "VIP":
                        totalVenta *= 0.75;
                    break;

                    case "Frecuente":
                        totalVenta *= 0.90;
                    break;

                }

                //esta es la cuarta consideracion logica.
                if (cantidadVendida > 100)
                {
                    totalVenta *= 0.95;
                }
             

                //esta es la tercera consideracion logica.
                //  descuento). Este sistema de recompensas incentiva a los consumidores a comprar más
                if (cbMetodoPago.SelectedItem == "Puntos")
                {
                    bool puedeUsarPuntos = _conexion.PuedeUsarPuntosFidelidad(consumidor.IdConsumidor);

                    if (!puedeUsarPuntos)
                    {
                        MessageBox.Show("No puedes pagar el 100% con puntos. Debes realizar al menos 3 compras con otro método de pago primero.",
                                        "Restricción de puntos de fidelidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Verificar si el cliente tiene suficientes puntos
                    if (consumidor.PuntosFidelidad < (int)pkPuntosUsados.Value)
                    {
                        MessageBox.Show("El producto que desea comprar supera a sus puntos, no se puede realizar la venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    

                    // Restar los puntos usados
                    consumidor.PuntosFidelidad -= (int)pkPuntosUsados.Value;



                    // Calcular el descuento (10,000 puntos = 1% de descuento)
                    double descuento = Math.Min((int)pkPuntosUsados.Value / 10000.0, 100); // Máximo 100% de descuento

                    // Aplicar el descuento al total de la venta

                    MessageBox.Show($"Descuento.{descuento}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    totalVenta = totalVenta * (100 - descuento) / 100;
                    MessageBox.Show($"Descuento.{totalVenta}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Si no usa puntos, acumular puntos basados en el total de la venta
                    consumidor.PuntosFidelidad += (int)(totalVenta / 100);
                    
                }

                

                // Sexta consideración lógica: Verificar si se pueden usar puntos correctamente
                if (!ValidarUsoDePuntos(consumidor, totalVenta))
                {
                    return;
                }

                // ✅ 6️⃣ Crear la venta
                var venta = new Venta
                {
                    FechaVenta = dtpFechaVenta.Value,
                    TotalVenta = totalVenta,
                    MetodoPago = cbMetodoPago.SelectedItem.ToString(),
                    PuntosUsados = (int)pkPuntosUsados.Value,
                    CantidadVendido = cantidadVendida,
                    IDCosmetico = int.Parse(cbIdCosmetico.SelectedValue.ToString()),
                    IDConsumidor = int.Parse(cbIdConsumidor.SelectedValue.ToString()),
                    EstadoVenta = cbEstadoVentas.SelectedItem.ToString()
                };


                // ✅ 7️⃣ Guardar la venta en la base de datos


                //lo comente para no guaradar la venta de manera innecesaria
               _conexion.GuardarVentas(venta);

                consumidor.FrecuenciaCompra = obtenerFrecuenciaCompra();

                _conexion.ModificarConsumidor(consumidor);

                MessageBox.Show($"Los cambios se han guardado correctamente {venta.TotalVenta}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);




                // ✅ 8️⃣ Actualizar stock si la venta es "Completada" o "Pendiente"
                 if (venta.EstadoVenta == "Completada" || venta.EstadoVenta == "Pendiente")
                {
                    cosmetico.StockDisponible -= venta.CantidadVendido;
                    _conexion.ModificarCosmetico(cosmetico);
                }

                    // ✅ 9️⃣ Mostrar confirmación y cerrar ventana
                    MessageBox.Show($"Los cambios se han guardado correctamente.{venta.TotalVenta}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string obtenerFrecuenciaCompra()
        {
            try
            {
                Consumidor consumidor = _conexion.MostrarIDConsumidor(int.Parse(cbIdCosmetico.SelectedItem.ToString()));
                if (consumidor == null)
                {
                    DataSet dataSet = _conexion.VentaPorConsumidor(consumidor.IdConsumidor);
                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        DataTable table = dataSet.Tables[0];
                        int rowCount = table.Rows.Count;

                        if (rowCount <= 5)
                        {
                            return "Ocasional";
                        }
                        else if (rowCount > 5 && rowCount <= 10)
                        {
                            return "Frecuente";
                        }
                        else if (rowCount > 10)
                        {
                            return "VIP";
                        }
                    }
                }

                return "Ocasional";

            }
            catch (Exception ex)
            {

                return "Ocasional";
            }


        }
        private void ActualizarDatos()
        {
            try
            {
                // ✅ 1️⃣ Validaciones iniciales
                if (cbIdCosmetico.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un cosmético.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbIdConsumidor.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un consumidor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbMetodoPago.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un método de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbEstadoVentas.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar un estado para la venta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 2️⃣ Obtener el cosmético desde la base de datos
                int idCosmetico = Convert.ToInt32(cbIdCosmetico.SelectedValue);
                Cosmetico cosmetico = _conexion.BuscarPorIdCosmetico(idCosmetico);

                if (cosmetico == null)
                {
                    MessageBox.Show("El cosmético seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ✅ 3️⃣ Validar que el cosmético NO esté vencido
                if (cosmetico.FechaVencimiento <= DateTime.Today)
                {
                    MessageBox.Show("El producto está vencido y no se puede vender.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 4️⃣ Validar el stock disponible
                int cantidadVendida = (int)pkCantidadVendidos.Value;

                if (cosmetico.StockDisponible < cantidadVendida)
                {
                    MessageBox.Show($"No hay suficiente stock disponible. Disponible: {cosmetico.StockDisponible}.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 5️⃣ Calcular el total de la venta con posible descuento
                double totalVenta = cosmetico.PrecioUnitario * cantidadVendida;

                if (cantidadVendida > 100)
                {
                    totalVenta *= 0.95; // Aplicar descuento del 5%
                }

                Venta ventaPrevia = _conexion.MostrarIDVenta(_venta.IdVenta);
                // ✅ 6️⃣ Crear el objeto de venta
                var venta = new Venta
                {
                    IdVenta = _venta.IdVenta,
                    FechaVenta = dtpFechaVenta.Value,
                    TotalVenta = totalVenta,
                    MetodoPago = cbMetodoPago.SelectedItem.ToString(),
                    PuntosUsados = (int)pkPuntosUsados.Value,
                    CantidadVendido = cantidadVendida,
                    IDCosmetico = idCosmetico,
                    IDConsumidor = Convert.ToInt32(cbIdConsumidor.SelectedValue),
                    EstadoVenta = cbEstadoVentas.SelectedItem.ToString()
                };

               


                // ✅ 7️⃣ RESTRICCIÓN: No permitir cancelaciones después de 24 horas
                TimeSpan diferencia = DateTime.Now - venta.FechaVenta;

                if (ventaPrevia.EstadoVenta.Equals("Pendiente") && diferencia.TotalHours > 24 && venta.EstadoVenta.Equals("Cancelada"))
                {
                    MessageBox.Show("No se puede cancelar una venta después de 24 horas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ventaPrevia.EstadoVenta == "Completada")
                {
                    MessageBox.Show("No se puede cancelar una venta ya completada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ 9️⃣ Actualizar la venta en la base de datos
                _conexion.ModificarVenta(venta);

                // ✅ 8️⃣ Si la venta es completada o pendiente, actualizar stock
                if (venta.EstadoVenta == "Completada" || venta.EstadoVenta == "Pendiente")
                {

                    Consumidor consumidor = _conexion.BuscarPorIdConsumidor(venta.IDConsumidor);
                    consumidor.PuntosFidelidad += ventaPrevia.PuntosUsados;
                    consumidor.PuntosFidelidad -= venta.PuntosUsados;
                    _conexion.ModificarConsumidor(consumidor);

                    cosmetico.StockDisponible += ventaPrevia.CantidadVendido;
                    //Este pasa en caso de que se llegara a editar el stock
                    cosmetico.StockDisponible -= cantidadVendida;
                    _conexion.ModificarCosmetico(cosmetico);
                }
                if (venta.EstadoVenta == "Cancelada")
                {
                    cosmetico.StockDisponible += ventaPrevia.CantidadVendido;

                    _conexion.ModificarCosmetico(cosmetico);
                    Consumidor consumidor = _conexion.BuscarPorIdConsumidor(venta.IDConsumidor);
                    consumidor.PuntosFidelidad += ventaPrevia.PuntosUsados;
                    _conexion.ModificarConsumidor(consumidor);
                }

                // ✅ 🔟 Confirmación y cierre de la ventana
                MessageBox.Show("Los cambios se han guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool ValidarUsoDePuntos(Consumidor consumidor, double totalVenta)
        {
            // Sexta consideración lógica: Verificación de uso de puntos en ventas.

            if (cbMetodoPago.SelectedItem.ToString() == "Puntos")
            {
                if (consumidor.PuntosFidelidad < totalVenta)
                {
                    MessageBox.Show("El producto que desea comprar supera la cantidad de puntos disponibles. No se puede realizar la venta.",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Restar los puntos usados
                consumidor.PuntosFidelidad -= (int)totalVenta;
            }
            else
            {
                // Si no se paga con puntos, se acumulan nuevos puntos (Ejemplo: 1 punto por cada 100 gastados)
                int puntosGanados = (int)(totalVenta / 100);
                consumidor.PuntosFidelidad += puntosGanados;
            }

            return true; // Retorna verdadero si la validación se completó sin problemas
        }


        private void VentanaAgregarVenta_Load(object sender, EventArgs e)
        {
            CargarComboBoxCosmeticos();
            CargarComboBoxConsumidores();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pb_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_aceptar(object sender, EventArgs e)
        {

            if (edita == false)
            {
                GuardarDatos();
                this.Close();
            }
            else
            {
                ActualizarDatos();
                this.Close();
            }
            
            


        }

        private void cargarDatos(Venta venta)
        {
            _conexion.MostrarIDVenta(venta.IdVenta);
            dtpFechaVenta.Value = venta.FechaVenta;
            pkCantidadVendidos.Value = venta.CantidadVendido;
            pkPuntosUsados.Value = venta.PuntosUsados;
            cbMetodoPago.SelectedItem = venta.MetodoPago;
            cbEstadoVentas.SelectedItem = venta.EstadoVenta;
            cbIdCosmetico.SelectedValue = venta.IDCosmetico;
            cbIdConsumidor.SelectedValue = venta.IDConsumidor;

        }


        private void pkCantidadVendidos_ValueChanged(object sender, EventArgs e)
        {
            if (cbIdCosmetico.SelectedValue != null) // Verificar que haya un cosmético seleccionado
            {
                // Buscar el cosmético por ID
                Cosmetico cosmetico = _conexion.BuscarPorIdCosmetico(int.Parse(cbIdCosmetico.SelectedValue.ToString()));

                int cantidadVendida = (int)pkCantidadVendidos.Value;
                double totalVenta = cosmetico.PrecioUnitario * cantidadVendida;

                // Aplicar el 5% de descuento si se compran más de 100 unidades
                if (cantidadVendida > 100)
                {
                    totalVenta *= 0.95;
                }

                txtPrecioTotal.Text = totalVenta.ToString("0.00");
            }

        }

       

    }
}
