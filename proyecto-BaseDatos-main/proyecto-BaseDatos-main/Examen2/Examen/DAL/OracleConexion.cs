using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DAL
{
    public class OracleConexion
    {
        private string StringConexion;
        private OracleConnection _connection;
        private OracleCommand _command;
        private OracleDataReader _reader;


        public OracleConexion(string connectionString)
        {
            StringConexion = connectionString;
            _connection = new OracleConnection(StringConexion);
            _command = new OracleCommand();
            _command.Connection = _connection;
        }


        //--------------------------------------------------comienzan los metodos para cosmeticos

        /// <summary> metodo para guardar un cosmetico en la base de datos</summary> 

        public void GuardarCosmetico(Cosmetico cosmetico)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Ins_Cosmeticos", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = cosmetico.Nombre;
                _command.Parameters.Add("pMarca", OracleDbType.Varchar2).Value = cosmetico.Marca;
                _command.Parameters.Add("pPrecioUnitario", OracleDbType.Decimal).Value = cosmetico.PrecioUnitario;
                _command.Parameters.Add("pStockDisponible", OracleDbType.Int32).Value = cosmetico.StockDisponible;
                _command.Parameters.Add("pFechaVencimiento", OracleDbType.Date).Value = cosmetico.FechaVencimiento;
                _command.Parameters.Add("pCategoria", OracleDbType.Varchar2).Value = cosmetico.Categoria;
                _command.Parameters.Add("pEstadoProducto", OracleDbType.Varchar2).Value = cosmetico.EstadoProducto;
                _command.Parameters.Add("pImagen", OracleDbType.Varchar2).Value = cosmetico.Imagen;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el cosmético en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }



        public void ModificarCosmetico(Cosmetico cosmetico)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Upd_Cosmeticos", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdCosmetico", OracleDbType.Int32).Value = cosmetico.IDCosmetico;
                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = cosmetico.Nombre;
                _command.Parameters.Add("pMarca", OracleDbType.Varchar2).Value = cosmetico.Marca;
                _command.Parameters.Add("pPrecioUnitario", OracleDbType.Decimal).Value = cosmetico.PrecioUnitario;
                _command.Parameters.Add("pStockDisponible", OracleDbType.Int32).Value = cosmetico.StockDisponible;
                _command.Parameters.Add("pFechaVencimiento", OracleDbType.Date).Value = cosmetico.FechaVencimiento;
                _command.Parameters.Add("pCategoria", OracleDbType.Varchar2).Value = cosmetico.Categoria;
                _command.Parameters.Add("pEstadoProducto", OracleDbType.Varchar2).Value = cosmetico.EstadoProducto;
                _command.Parameters.Add("pImagen", OracleDbType.Varchar2).Value = cosmetico.Imagen;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el cosmético en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }//fin del metodo para editar

        public void EliminarCosmetico(int IDCosmetico)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Del_Cosmeticos", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdCosmetico", OracleDbType.Int32).Value = IDCosmetico;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cosmético en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }//fin del metodo para eliminar

        public Cosmetico BuscarPorNombreCosmetico(string nombre)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_Cosmetico", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombre;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                _reader = _command.ExecuteReader();

                Cosmetico temp = null;
                if (_reader.Read())
                {
                    temp = new Cosmetico();
                    temp.IDCosmetico = Convert.ToInt32(_reader["idCosmetico"]);
                    temp.Nombre = _reader["nombreProducto"].ToString();
                    temp.Marca = _reader["marca"].ToString();
                    temp.PrecioUnitario = Convert.ToDouble(_reader["precioUnitario"]);
                    temp.StockDisponible = Convert.ToInt32(_reader["stockDisponible"]);
                    temp.FechaVencimiento = Convert.ToDateTime(_reader["fechaVencimiento"]);
                    temp.Categoria = _reader["categoria"].ToString();
                    temp.EstadoProducto = _reader["estadoProducto"].ToString();
                    temp.Imagen = _reader["imagen"].ToString();
                }

                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el cosmético: " + ex.Message, ex);
            }
            finally
            {
                _reader?.Close();
                _reader?.Dispose();
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }//fin del metodo para buscar

        //este se diferencia del anterior debido a que este, esta hecho para llenar la tabla de los datos de la base de datos por esto es un dataset
        public DataSet BuscarPorNombreCosmeticos(string nombre)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_Cosmetico", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombre;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet datos = new DataSet();
                adapter.Fill(datos);

                return datos;
            }
            catch (Exception e)
            {
                throw new Exception("Error al buscar cosméticos: " + e.Message, e);
            }
            finally
            {
                _command?.Dispose();
                _connection?.Close();
                _connection?.Dispose();
            }
        }//fin del metodo para mostrar todos los datos en la tabla

        //metodo para verificar si un cosmetico ya ha sido vendido
        public bool CosmeticoVendido(int idCosmetico)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Verificar_CosmeticoVendido", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada
                _command.Parameters.Add("pIDCosmetico", OracleDbType.Int32).Value = idCosmetico;

                // Parámetro de salida
                OracleParameter outputParam = new OracleParameter("pCantidad", OracleDbType.Int32);
                outputParam.Direction = ParameterDirection.Output;
                _command.Parameters.Add(outputParam);

                _command.ExecuteNonQuery();

                int resultado = ((OracleDecimal)outputParam.Value).ToInt32();


                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si el cosmético fue vendido: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }


        public bool VentasPendientes(int idCosmetico)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Verificar_VentasPendientes", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada
                _command.Parameters.Add("pIDCosmetico", OracleDbType.Int32).Value = idCosmetico;

                // Parámetro de salida
                OracleParameter outputParam = new OracleParameter("pCantidad", OracleDbType.Int32);
                outputParam.Direction = ParameterDirection.Output;
                _command.Parameters.Add(outputParam);

                _command.ExecuteNonQuery();

                // Conversión segura desde OracleDecimal
                int resultado = ((OracleDecimal)outputParam.Value).ToInt32();

                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar ventas pendientes: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }



        //--------------------------------------------------finalizan los metodos para cosmeticos


        //--------------------------------------------------Comienza la parte de las compras



        public void GuardarCompra(Compra compra)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Ins_Compra", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pFechaCompra", OracleDbType.Date).Value = compra.FechaCompra;
                _command.Parameters.Add("pTotalCompra", OracleDbType.Decimal).Value = compra.TotalCompra;
                _command.Parameters.Add("pMetodoPago", OracleDbType.Varchar2).Value = compra.MetodoPago;
                _command.Parameters.Add("pProveedor", OracleDbType.Varchar2).Value = compra.Proveedor;
                _command.Parameters.Add("pCantidadProductos", OracleDbType.Int32).Value = compra.CantidadProductos;
                _command.Parameters.Add("pEstadoCompra", OracleDbType.Varchar2).Value = compra.EstadoCompra;
                _command.Parameters.Add("pIdCosmetico", OracleDbType.Int32).Value = compra.IDCosmeticos;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la compra en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }//fin del metodo para guardar


        public void ModificarCompra(Compra compra)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Upd_Compra", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdCompra", OracleDbType.Int32).Value = compra.IDCompra;
                _command.Parameters.Add("pFechaCompra", OracleDbType.Date).Value = compra.FechaCompra;
                _command.Parameters.Add("pTotalCompra", OracleDbType.Decimal).Value = compra.TotalCompra;
                _command.Parameters.Add("pMetodoPago", OracleDbType.Varchar2).Value = compra.MetodoPago;
                _command.Parameters.Add("pProveedor", OracleDbType.Varchar2).Value = compra.Proveedor;
                _command.Parameters.Add("pCantidadProductos", OracleDbType.Int32).Value = compra.CantidadProductos;
                _command.Parameters.Add("pEstadoCompra", OracleDbType.Varchar2).Value = compra.EstadoCompra;
                _command.Parameters.Add("pIdCosmeticos", OracleDbType.Int32).Value = compra.IDCosmeticos;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la compra en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }//fin el metodo para modificar


        public void EliminarCompra(int IDCompra)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Del_Compra", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdCompra", OracleDbType.Int32).Value = IDCompra;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la compra en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }//fin para el metodo de borrar


        public Compra MostrarIDCompra(int IDCompra)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_IDCompra", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdCompra", OracleDbType.Int32).Value = IDCompra;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                _reader = _command.ExecuteReader();

                Compra temp = null;

                if (_reader.Read())
                {
                    temp = new Compra();
                    temp.IDCompra = Convert.ToInt32(_reader["idCompra"]);
                    temp.FechaCompra = Convert.ToDateTime(_reader["fechaCompra"]);
                    temp.TotalCompra = Convert.ToDouble(_reader["totalCompra"]);
                    temp.MetodoPago = _reader["metodoPago"].ToString();
                    temp.Proveedor = _reader["proveedor"].ToString();
                    temp.CantidadProductos = Convert.ToInt32(_reader["cantidadProducto"]);
                    temp.EstadoCompra = _reader["estado"].ToString(); // campo es "estado" en Oracle
                    temp.IDCosmeticos = Convert.ToInt32(_reader["idCosmetico"]); // debe existir
                }

                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar la compra por ID: " + ex.Message, ex);
            }
            finally
            {
                _reader?.Close();
                _reader?.Dispose();
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }//buscar por id compra


        public DataSet BuscarPorEstadoCompra(string estadoCompra)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_EstadoCompra", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pEstadoCompra", OracleDbType.Varchar2).Value = estadoCompra;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet datos = new DataSet();
                adapter.Fill(datos);

                return datos;
            }
            catch (Exception e)
            {
                throw new Exception("Error al buscar por estado de compra: " + e.Message, e);
            }
            finally
            {
                _command?.Dispose();
                _connection?.Close();
                _connection?.Dispose();
            }
        }



        //--------------------------------------------------Finaliza la parte de las compras
        public void GuardarConsumidor(Consumidor consumidor)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Ins_Consumidor", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombreCompleto", OracleDbType.Varchar2).Value = consumidor.NombreCompleto;
                _command.Parameters.Add("pTelefono", OracleDbType.Int64).Value = Convert.ToInt64(consumidor.telefono);
                _command.Parameters.Add("pCorreoElectronico", OracleDbType.Varchar2).Value = consumidor.CorreoElectronico;
                _command.Parameters.Add("pFechaRegistro", OracleDbType.Date).Value = consumidor.FechaRegistro;
                _command.Parameters.Add("pFrecuenciaCompra", OracleDbType.Varchar2).Value = consumidor.FrecuenciaCompra;
                _command.Parameters.Add("pPuntosFidelidad", OracleDbType.Int32).Value = consumidor.PuntosFidelidad;
                _command.Parameters.Add("pDireccion", OracleDbType.Varchar2).Value = consumidor.Direccion;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el consumidor en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }


        public void ModificarConsumidor(Consumidor consumidor)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Upd_Consumidor", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdConsumidor", OracleDbType.Int32).Value = consumidor.IdConsumidor;
                _command.Parameters.Add("pNombreCompleto", OracleDbType.Varchar2).Value = consumidor.NombreCompleto;
                _command.Parameters.Add("pTelefono", OracleDbType.Int64).Value = Convert.ToInt64(consumidor.telefono);
                _command.Parameters.Add("pCorreoElectronico", OracleDbType.Varchar2).Value = consumidor.CorreoElectronico;
                _command.Parameters.Add("pFechaRegistro", OracleDbType.Date).Value = consumidor.FechaRegistro;
                _command.Parameters.Add("pFrecuenciaCompra", OracleDbType.Varchar2).Value = consumidor.FrecuenciaCompra;
                _command.Parameters.Add("pPuntosFidelidad", OracleDbType.Int32).Value = consumidor.PuntosFidelidad;
                _command.Parameters.Add("pDireccion", OracleDbType.Varchar2).Value = consumidor.Direccion;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el consumidor en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }


        public void EliminarConsumidor(int idConsumidor)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Del_Consumidor", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdConsumidor", OracleDbType.Int32).Value = idConsumidor;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el consumidor en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public Consumidor MostrarIDConsumidor(int IDConsumidor)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_IDConsumidor", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdConsumidor", OracleDbType.Int32).Value = IDConsumidor;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                _reader = _command.ExecuteReader();

                Consumidor temp = null;

                if (_reader.Read())
                {
                    temp = new Consumidor();
                    temp.IdConsumidor = Convert.ToInt32(_reader["idConsumidor"]);
                    temp.NombreCompleto = _reader["nombreConsumidor"].ToString();
                    temp.CorreoElectronico = _reader["correoElectronico"].ToString();
                    temp.FechaRegistro = Convert.ToDateTime(_reader["fechaRegistro"]);
                    temp.FrecuenciaCompra = _reader["frecuenciaCompra"].ToString();
                    temp.PuntosFidelidad = Convert.ToInt32(_reader["puntosFidelidad"]);
                    temp.Direccion = _reader["direccion"].ToString();
                    temp.telefono = ""; // opcional: podrías consultarlo aparte si lo necesitas
                }

                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar consumidor por ID en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _reader?.Close();
                _reader?.Dispose();
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }


        public Consumidor MostrarNombreConsumidor(string NombreCompleto)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_NombreConsumidor", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombreCompleto", OracleDbType.Varchar2).Value = NombreCompleto;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                _reader = _command.ExecuteReader();

                if (_reader.Read())
                {
                    return new Consumidor
                    {
                        IdConsumidor = _reader.IsDBNull(0) ? 0 : Convert.ToInt32(_reader.GetValue(0)),
                        NombreCompleto = _reader.IsDBNull(1) ? string.Empty : _reader.GetString(1),
                        telefono = _reader.IsDBNull(2) ? string.Empty : _reader.GetValue(2).ToString(),
                        CorreoElectronico = _reader.IsDBNull(3) ? string.Empty : _reader.GetString(3),
                        FechaRegistro = _reader.IsDBNull(4) ? DateTime.MinValue : Convert.ToDateTime(_reader.GetValue(4)),
                        FrecuenciaCompra = _reader.IsDBNull(5) ? "0" : _reader.GetString(5),
                        PuntosFidelidad = _reader.IsDBNull(6) ? 0 : Convert.ToInt32(_reader.GetValue(6)),
                        Direccion = _reader.IsDBNull(7) ? string.Empty : _reader.GetString(7)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar consumidor por nombre en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _reader?.Close();
                _reader?.Dispose();
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public DataSet BuscarPorNombreConsumidor(string NombreCompleto)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_ConsumidorNombre", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = NombreCompleto;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet datos = new DataSet();
                adapter.Fill(datos);

                return datos;
            }
            catch (Exception e)
            {
                throw new Exception("Error al buscar consumidores por nombre en Oracle: " + e.Message, e);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public DataSet VentaPorConsumidor(int idConsumidor)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_VentaConsumidor", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIDConsumidor", OracleDbType.Int32).Value = idConsumidor;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet datos = new DataSet();
                adapter.Fill(datos);

                return datos;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener ventas por consumidor en Oracle: " + e.Message, e);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }





        //...................................................inicia la parte de los consumidores

        //---------------------------------------------------inicia la parte de las ventas

        public void GuardarVentas(Venta venta)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Ins_Venta", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pFechaVenta", OracleDbType.Date).Value = venta.FechaVenta;
                _command.Parameters.Add("pTotalVenta", OracleDbType.Double).Value = venta.TotalVenta;
                _command.Parameters.Add("pMetodoPago", OracleDbType.Varchar2).Value = venta.MetodoPago;
                _command.Parameters.Add("pPuntosUsados", OracleDbType.Int32).Value = venta.PuntosUsados;
                _command.Parameters.Add("pEstadoVenta", OracleDbType.Varchar2).Value = venta.EstadoVenta;
                _command.Parameters.Add("pCantidadVendido", OracleDbType.Int32).Value = venta.CantidadVendido;
                _command.Parameters.Add("pIdCosmetico", OracleDbType.Int32).Value = venta.IDCosmetico;
                _command.Parameters.Add("pIdConsumidor", OracleDbType.Int32).Value = venta.IDConsumidor;


                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la venta en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }



        public void ModificarVenta(Venta venta)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Upd_Venta", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdVenta", OracleDbType.Int32).Value = venta.IdVenta;
                _command.Parameters.Add("pFechaVenta", OracleDbType.Date).Value = venta.FechaVenta;
                _command.Parameters.Add("pTotalVenta", OracleDbType.Double).Value = venta.TotalVenta;
                _command.Parameters.Add("pMetodoPago", OracleDbType.Varchar2).Value = venta.MetodoPago;
                _command.Parameters.Add("pPuntosUsados", OracleDbType.Int32).Value = venta.PuntosUsados;
                _command.Parameters.Add("pEstadoVenta", OracleDbType.Varchar2).Value = venta.EstadoVenta;
                _command.Parameters.Add("pCantidadVendido", OracleDbType.Int32).Value = venta.CantidadVendido;
                _command.Parameters.Add("pIdCosmetico", OracleDbType.Int32).Value = venta.IDCosmetico;
                _command.Parameters.Add("pIdConsumidor", OracleDbType.Int32).Value = venta.IDConsumidor;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la venta en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }


        public void EliminarVenta(int IdVenta)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Del_Venta", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdVenta", OracleDbType.Int32).Value = IdVenta;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la venta en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public Venta MostrarIDVenta(int IdVenta)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_IDVenta", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdVenta", OracleDbType.Int32).Value = IdVenta;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                _reader = _command.ExecuteReader();

                Venta temp = null;

                if (_reader.Read())
                {
                    temp = new Venta();
                    temp.IdVenta = Convert.ToInt32(_reader.GetValue(0));
                    temp.FechaVenta = Convert.ToDateTime(_reader.GetValue(1));
                    temp.MetodoPago = _reader.GetString(2);
                    temp.PuntosUsados = Convert.ToInt32(_reader.GetValue(3));
                    temp.IDConsumidor = Convert.ToInt32(_reader.GetValue(4));
                    temp.EstadoVenta = _reader.GetString(5);
                    temp.TotalVenta = Convert.ToDouble(_reader.GetValue(6));
                    temp.IDCosmetico = Convert.ToInt32(_reader.GetValue(7));
                    temp.CantidadVendido = Convert.ToInt32(_reader.GetValue(8));

                }

                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar la venta por ID en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _reader?.Close();
                _reader?.Dispose();
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public DataSet BuscarPorEstadoVenta(string estadoVenta)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_EstadoVenta", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pEstadoVenta", OracleDbType.Varchar2).Value = estadoVenta;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet datos = new DataSet();
                adapter.Fill(datos);

                return datos;
            }
            catch (Exception e)
            {
                throw new Exception("Error al buscar ventas por estado en Oracle: " + e.Message, e);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public DataSet CosmeticosId()
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Obtener_IDCosmetico", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Parámetro de salida tipo REF CURSOR
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet datos = new DataSet();
                adapter.Fill(datos);

                return datos;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los IDs de cosméticos: " + e.Message, e);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }
        public DataSet ObtenerIDConsumidores()
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Obtener_IDConsumidores", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Parámetro de salida tipo REF CURSOR
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet datos = new DataSet();
                adapter.Fill(datos);

                return datos;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los IDs de consumidores: " + e.Message, e);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public Cosmetico BuscarPorIdCosmetico(int IdCosmetico)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_IDCosmetico", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdCosmetico", OracleDbType.Int32).Value = IdCosmetico;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                _reader = _command.ExecuteReader();

                Cosmetico temp = null;

                if (_reader.Read())
                {
                    temp = new Cosmetico();
                    temp.IDCosmetico = Convert.ToInt32(_reader["idCosmetico"]);
                    temp.Nombre = _reader["nombreProducto"].ToString();
                    temp.Marca = _reader["marca"].ToString();
                    temp.PrecioUnitario = Convert.ToDouble(_reader["precioUnitario"]);
                    temp.StockDisponible = Convert.ToInt32(_reader["stockDisponible"]);
                    temp.FechaVencimiento = Convert.ToDateTime(_reader["fechaVencimiento"]);
                    temp.Categoria = _reader["categoria"].ToString();
                    temp.EstadoProducto = _reader["estadoProducto"].ToString();
                    temp.Imagen = _reader["imagen"].ToString();
                }

                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cosmético por ID en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _reader?.Close();
                _reader?.Dispose();
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public Consumidor BuscarPorIdConsumidor(int IdConsumidor)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_IDConsumidor", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdConsumidor", OracleDbType.Int32).Value = IdConsumidor;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                _reader = _command.ExecuteReader();

                Consumidor temp = null;

                if (_reader.Read())
                {
                    temp = new Consumidor();
                    temp.IdConsumidor = Convert.ToInt32(_reader["idConsumidor"]);
                    temp.NombreCompleto = _reader["nombreConsumidor"].ToString();
                    //temp.telefono = _reader["telefono"].ToString();
                    temp.CorreoElectronico = _reader["correoElectronico"].ToString();
                    temp.FechaRegistro = Convert.ToDateTime(_reader["fechaRegistro"]);
                    temp.FrecuenciaCompra = _reader["frecuenciaCompra"].ToString();
                    temp.PuntosFidelidad = Convert.ToInt32(_reader["puntosFidelidad"]);
                    temp.Direccion = _reader["direccion"].ToString();
                }

                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el consumidor por ID en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _reader?.Close();
                _reader?.Dispose();
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public bool PuedeUsarPuntosFidelidad(int idConsumidor)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();
                _command = new OracleCommand("Sp_Validar_Uso_Puntos", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIDConsumidor", OracleDbType.Int32).Value = idConsumidor;
                _command.Parameters.Add("pResultado", OracleDbType.Int32).Direction = ParameterDirection.Output;

                _command.ExecuteNonQuery();

                int resultado = Convert.ToInt32(_command.Parameters["pResultado"].Value);

                return resultado > 0; // Retorna true si puede usar puntos

            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar uso de puntos de fidelidad: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }




        //---------------------------------------------------finaliza la parte de las ventas
    }
}
