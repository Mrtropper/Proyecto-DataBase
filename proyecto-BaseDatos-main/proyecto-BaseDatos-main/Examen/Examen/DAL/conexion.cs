using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Conexion
    {

        private string StringConexion;
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;

        public Conexion(string pStringCnx)
        {
            StringConexion = pStringCnx;
        }


//se aplico la parte de la conexion para Cosmeticos----------------------------------------------------------------------------------------------------------------------------------------------
        public void GuardarCosmetico(Cosmetico cosmetico)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;

                _command.CommandText = "[Sp_Ins_Cosmeticos]";

                _command.Parameters.AddWithValue("@Nombre", cosmetico.Nombre);
                _command.Parameters.AddWithValue("@Marca", cosmetico.Marca);
                _command.Parameters.AddWithValue("@PrecioUnitario", cosmetico.PrecioUnitario);
                _command.Parameters.AddWithValue("@StockDisponible", cosmetico.StockDisponible);
                _command.Parameters.AddWithValue("@FechaVencimiento", cosmetico.FechaVencimiento);
                _command.Parameters.AddWithValue("@EstadoProducto", cosmetico.EstadoProducto);
                _command.Parameters.AddWithValue("@Categoria", cosmetico.Categoria);
                _command.Parameters.AddWithValue("@Imagen", cosmetico.Imagen);

                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ModificarCosmetico(Cosmetico cosmetico)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();

                _command.Connection = _connection;
                _command.CommandType = CommandType.StoredProcedure;

                _command.CommandText = "[Sp_Upd_Cosmeticos]";

                _command.Parameters.AddWithValue("@IDCosmetico", cosmetico.IDCosmetico);
                _command.Parameters.AddWithValue("@Nombre", cosmetico.Nombre);
                _command.Parameters.AddWithValue("@Marca", cosmetico.Marca);
                _command.Parameters.AddWithValue("@PrecioUnitario", cosmetico.PrecioUnitario);
                _command.Parameters.AddWithValue("@StockDisponible", cosmetico.StockDisponible);

                _command.Parameters.AddWithValue("@EstadoProducto", cosmetico.EstadoProducto);
                _command.Parameters.AddWithValue("@FechaVencimiento", cosmetico.FechaVencimiento);
                _command.Parameters.AddWithValue("@Categoria", cosmetico.Categoria);
                _command.Parameters.AddWithValue("@Imagen", cosmetico.Imagen);
                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void EliminarCosmetico(int IDCosmetico)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Del_Cosmetico]";
                _command.Parameters.AddWithValue("@IDCosmetico", IDCosmetico);
                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cosmetico BuscarPorNombreCosmetico(string nombre)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_Cosmetico]";
                _command.Parameters.AddWithValue("@Nombre", nombre);
                _reader = _command.ExecuteReader();
                Cosmetico temp = null;

                if (_reader.Read())
                {
                    temp = new Cosmetico();
                    temp.IDCosmetico = int.Parse(_reader.GetValue(0).ToString());
                    temp.Nombre = _reader.GetValue(1).ToString();
                    temp.Marca = _reader.GetValue(2).ToString();
                    temp.PrecioUnitario = double.Parse(_reader.GetValue(3).ToString());
                    temp.StockDisponible = int.Parse(_reader.GetValue(4).ToString());
                    temp.FechaVencimiento = DateTime.Parse(_reader.GetValue(5).ToString());
                    temp.Categoria = _reader.GetValue(6).ToString();
                    temp.EstadoProducto = _reader.GetValue(7).ToString();
                    temp.Imagen = _reader.GetValue(8).ToString();

                }
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet BuscarPorNombreCosmeticos(string nombre)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_Cosmetico]";
                _command.Parameters.AddWithValue("@Nombre", nombre);
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet datos = new DataSet();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //    public DataSet BuscarPorNombreUsuario(String nombre)
        //    {
        //        try
        //        {
        //            _connection = new SqlConnection(StringConexion);
        //            _connection.Open();
        //            _command = new SqlCommand();
        //            _command.Connection = _connection;

        //            _command.CommandType = CommandType.StoredProcedure;
        //            _command.CommandText = "[Sp_Buscar_Nombres_Usuarios]";
        //            _command.Parameters.AddWithValue("@Nombre", nombre);
        //            SqlDataAdapter adapter = new SqlDataAdapter();
        //            DataSet datos = new DataSet();
        //            adapter.SelectCommand = _command;
        //            adapter.Fill(datos);
        //            _connection.close();
        //            _connection.Dispose();
        //            _command.Dispose();
        //            adapter.dispose();
        //            return datos;
        //        }
        //        catch (Exception ex)
        //        {
        //            Throw ex;
        //        }
        //    }



        //    public void BuscarPorEmaiUsuarios(String Email)
        //    {
        //        try
        //        {
        //            _connection = new SqlConnection(StringConexion);
        //            _connection.Open();
        //            _command = new SqlCommand();
        //            _command.Connection = _connection;

        //            _command.CommandType = CommandType.StoredProcedure;
        //            _command.CommandText = "[Sp_Buscar_Email_Usuario]";
        //            _command.Parameters.AddWithValue("@Email", Email);
        //            _reader = _command.ExecuteReader();
        //            Usuario temp = null;

        //            if (_reader.Read())
        //            {
        //                temp = new Usuario();
        //                temp.Email = _reader.GetValue(0).ToString();
        //                temp.NombreCompleto = _reader.GetValue(1).ToString();
        //                temp.Password = _reader.GetValue(2).ToString();
        //                temp.FechaRegistro = DateTime.Parse(_reader.GetValue(3).ToString());
        //            }
        //            _connection.Close();
        //            _connection.Dispose();
        //            _command.Dispose();
        //            return temp;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex
        //        }
        //    }






        //    public void ValidarUsuario(string pEmail, string pPassword)
        //    {
        //        try
        //        {
        //            _connection = new SqlConnection(StringConexion);
        //            _connection.Open();
        //            _command = new SqlCommand();
        //            _command.Connection = _connection;

        //            _command.CommandType = CommandType.StoredProcedure;
        //            _command.CommandText = "[Sp_Cns_Usuario]";
        //            _command.Parameters.AddWithValue("pEmail", pEmail);
        //            _command.Parameters.AddWithValue("@pPw", pPassword);
        //            _reader = _command.ExecuteReader();
        //            Usuario temp = null;

        //            if (_reader.Read())
        //            {
        //                temp = new Usuario();
        //                temp.Email = _reader.GetValue(0).ToString();
        //                temp.NombreCompleto = _reader.GetValue(1).ToString();
        //                temp.Password = _reader.GetValue(2).ToString();
        //                temp.FechaRegistro = DateTime.Parse(_reader.GetValue(3).ToString());
        //            }
        //            _connection.Close();
        //            _connection.Dispose();
        //            _command.Dispose();
        //            return temp;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex
        //        }
        //    }





        //    public void ModificarClaveUsuario(string pEmail, string pPassword)
        //    {
        //        try
        //        {
        //            _connection = newSqlConnection(StringConexion);
        //            _connection.Open();
        //            _command = SqlCommand();
        //            _command.Connection = _connection;
        //            _command.CommandType = CommandType.StoredProcedure;
        //            _command.CommandText = "[Sp_Upd_Pass_Usuarios]";
        //            _command.Parameters.AddWithValue("@Email", pEmail);
        //            _command.Parameters.AddWithValue("@pPwNew", pPassword);

        //            _command.ExecuteNonQuery();
        //            _connection.Dispose();
        //            _command.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //}

//Parte de la conexion que es para la compra ----------------------------------------------------------------------------------------------------------------------------------------------------

        public void GuardarCompra(Compra compra)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;

                _command.CommandText = "[Sp_Ins_Compra]";

                _command.Parameters.AddWithValue("@FechaCompra", compra.FechaCompra);
                _command.Parameters.AddWithValue("@TotalCompra", compra.TotalCompra);
                _command.Parameters.AddWithValue("@MetodoPago", compra.MetodoPago);
                _command.Parameters.AddWithValue("@Proveedor", compra.Proveedor);
                _command.Parameters.AddWithValue("@CantidadProductos", compra.CantidadProductos);
                _command.Parameters.AddWithValue("@EstadoCompra", compra.EstadoCompra);
                _command.Parameters.AddWithValue("@IDCosmeticos", compra.IDCosmeticos);
                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ModificarCompra(Compra compra)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();

                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Upd_Compra]";

                _command.Parameters.AddWithValue("@IDCompra", compra.IDCompra);
                _command.Parameters.AddWithValue("@FechaCompra", compra.FechaCompra);
                _command.Parameters.AddWithValue("@TotalCompra", compra.TotalCompra);
                _command.Parameters.AddWithValue("@MetodoPago", compra.MetodoPago);
                _command.Parameters.AddWithValue("@Proveedor", compra.Proveedor);
                _command.Parameters.AddWithValue("@CantidadProductos", compra.CantidadProductos);
                _command.Parameters.AddWithValue("@EstadoCompra", compra.EstadoCompra);
                _command.Parameters.AddWithValue("@IDCosmeticos", compra.IDCosmeticos);
                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void EliminarCompra(int IDCompra)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Del_Compra]";
                _command.Parameters.AddWithValue("@IDCompra", IDCompra);
                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Compra MostrarIDCompra(int IDCompra)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_IDCompra]";
                _command.Parameters.AddWithValue("@IDCompra", IDCompra);
                _reader = _command.ExecuteReader();
                Compra temp = null;

                if (_reader.Read())
                {
                    temp = new Compra();
                    temp.IDCompra = int.Parse(_reader.GetValue(0).ToString());
                    temp.FechaCompra = DateTime.Parse(_reader.GetValue(1).ToString());
                    temp.TotalCompra = double.Parse(_reader.GetValue(2).ToString());
                    temp.MetodoPago = _reader.GetValue(3).ToString();
                    temp.Proveedor = _reader.GetValue(4).ToString();

                    temp.CantidadProductos = int.Parse(_reader.GetValue(5).ToString());
                    temp.EstadoCompra = _reader.GetValue(6).ToString();
                    temp.IDCosmeticos = int.Parse(_reader.GetValue(7).ToString());
                }
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet BuscarPorEstadoCompra(string estadoCompra)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_EstadoCompra]";
                _command.Parameters.AddWithValue("@EstadoCompra", estadoCompra);
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet datos = new DataSet();

                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Parte de la conexion que es consumidorm-------------------------------------------------------------------------------------------------------------------------------------------

        public void GuardarConsumidor(Consumidor consumidor)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;
                _command.CommandType = CommandType.StoredProcedure;

                _command.CommandText = "[Sp_Ins_Consumidor]";

                _command.Parameters.AddWithValue("@NombreCompleto", consumidor.NombreCompleto);
                _command.Parameters.AddWithValue("@telefono", consumidor.telefono);
                _command.Parameters.AddWithValue("@CorreoElectronico", consumidor.CorreoElectronico);
                _command.Parameters.AddWithValue("@FechaRegistro", consumidor.FechaRegistro);
                _command.Parameters.AddWithValue("@FrecuenciaCompra", consumidor.FrecuenciaCompra);
                _command.Parameters.AddWithValue("@PuntosFidelidad", consumidor.PuntosFidelidad);
                _command.Parameters.AddWithValue("@Direccion", consumidor.Direccion);

                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ModificarConsumidor(Consumidor consumidor)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();

                _command = new SqlCommand();

                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;

                _command.CommandText = "[Sp_Upd_Consumidor]";

                _command.Parameters.AddWithValue("@IDConsumidor", consumidor.IdConsumidor);
                _command.Parameters.AddWithValue("@NombreCompleto", consumidor.NombreCompleto);
                _command.Parameters.AddWithValue("@telefono", consumidor.telefono);
                _command.Parameters.AddWithValue("@CorreoElectronico", consumidor.CorreoElectronico);
                _command.Parameters.AddWithValue("@FechaRegistro", consumidor.FechaRegistro);
                _command.Parameters.AddWithValue("@FrecuenciaCompra", consumidor.FrecuenciaCompra);
                _command.Parameters.AddWithValue("@PuntosFidelidad", consumidor.PuntosFidelidad);
                _command.Parameters.AddWithValue("@Direccion", consumidor.Direccion);
                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EliminarConsumidor(int idConsumidor)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();

                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;

                _command.CommandText = "[Sp_Del_Consumidor]";

                _command.Parameters.AddWithValue("@IDConsumidor", idConsumidor);

                _command.ExecuteNonQuery();
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Consumidor MostrarIDConsumidor(int IDConsumidor)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_IDConsumidor]";
                _command.Parameters.AddWithValue("@IDConsumidor", IDConsumidor);
                _reader = _command.ExecuteReader();
                Consumidor temp = null;

                if (_reader.Read())
                {
                    temp = new Consumidor();
                    temp.IdConsumidor = int.Parse(_reader.GetValue(0).ToString());
                    temp.NombreCompleto = _reader.GetValue(1).ToString();
                    temp.telefono = _reader.GetValue(2).ToString();
                    temp.CorreoElectronico = _reader.GetValue(3).ToString();
                    temp.FechaRegistro = DateTime.Parse(_reader.GetValue(4).ToString());
                    temp.FrecuenciaCompra = _reader.GetValue(5).ToString();
                    temp.PuntosFidelidad = int.Parse(_reader.GetValue(6).ToString());
                    temp.Direccion = _reader.GetValue(7).ToString();

                }
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Consumidor MostrarNombreConsumidor(string NombreCompleto)
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(StringConexion))
                {
                    _connection.Open();
                    using (SqlCommand _command = new SqlCommand("[Sp_Most_NombreConsumidor]", _connection))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);

                        using (SqlDataReader _reader = _command.ExecuteReader())
                        {
                            if (_reader.Read())
                            {
                                return new Consumidor
                                {
                                    IdConsumidor = _reader.IsDBNull(0) ? 0 : _reader.GetInt32(0),
                                    NombreCompleto = _reader.IsDBNull(1) ? string.Empty : _reader.GetString(1),
                                    telefono = _reader.IsDBNull(2) ? string.Empty : _reader.GetString(2),
                                    CorreoElectronico = _reader.IsDBNull(3) ? string.Empty : _reader.GetString(3),
                                    FechaRegistro = _reader.IsDBNull(4) ? DateTime.MinValue : _reader.GetDateTime(4),
                                    FrecuenciaCompra = _reader.IsDBNull(5) ? "0" : _reader.GetString(5),
                                    PuntosFidelidad = _reader.IsDBNull(6) ? 0 : _reader.GetInt32(6),
                                    Direccion = _reader.IsDBNull(7) ? string.Empty : _reader.GetString(7)
                                };
                            }
                        }
                    }
                }
                return null; // Retorna null si no encuentra resultados
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar consumidor por nombre: " + ex.Message, ex);
            }
        }


        public DataSet BuscarPorNombreConsumidor(String NombreCompleto)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_ConsumidorNombre]";
                _command.Parameters.AddWithValue("@NombreCompleto", NombreCompleto);
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet datos = new DataSet();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Conexion que se refiere a la parte de venta----------------------------------------------------------------------------------------------------------------------------------------------



        public void GuardarVentas(Venta venta)
        {
                try
                {
                    _connection = new SqlConnection(StringConexion);
                    _connection.Open();
                    _command = new SqlCommand();
                    _command.Connection = _connection;

                    _command.CommandType = CommandType.StoredProcedure;

                    _command.CommandText = "[Sp_Ins_Ventas]";

                    _command.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                    _command.Parameters.AddWithValue("@TotalVenta", venta.TotalVenta);
                    _command.Parameters.AddWithValue("@MetodoPago", venta.MetodoPago);
                    _command.Parameters.AddWithValue("@PuntosUsados", venta.PuntosUsados);
                    _command.Parameters.AddWithValue("@CantidadVendido", venta.CantidadVendido);
                    _command.Parameters.AddWithValue("@IDCosmeticos", venta.IDCosmetico);
                    _command.Parameters.AddWithValue("@IDConsumidor", venta.IDConsumidor);
                    _command.Parameters.AddWithValue("@EstadoVenta", venta.EstadoVenta);
                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _connection.Dispose();
                    _command.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

        }


            public void ModificarVenta(Venta venta)
            {
                try
                {
                    _connection = new SqlConnection(StringConexion);
                    _connection.Open();
                    _command = new SqlCommand();

                    _command.Connection = _connection;

                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "[Sp_Upd_Venta]";

                    _command.Parameters.AddWithValue("@IDVenta", venta.IdVenta);
                    _command.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                    _command.Parameters.AddWithValue("@TotalVenta", venta.TotalVenta);
                    _command.Parameters.AddWithValue("@MetodoPago", venta.MetodoPago);
                    _command.Parameters.AddWithValue("@PuntosUsados", venta.PuntosUsados);
                    _command.Parameters.AddWithValue("@CantidadVendido", venta.CantidadVendido);
                    _command.Parameters.AddWithValue("@IDCosmeticos", venta.IDCosmetico);
                    _command.Parameters.AddWithValue("@IDConsumidor", venta.IDConsumidor);
                    _command.Parameters.AddWithValue("@EstadoVenta", venta.EstadoVenta);
                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _connection.Dispose();
                    _command.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }




            public void EliminarVenta(int IdVenta)
            {
                try
                {
                    _connection = new SqlConnection(StringConexion);
                    _connection.Open();
                    _command = new SqlCommand();
                    _command.Connection = _connection;

                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "[Sp_Del_Venta]";
                    _command.Parameters.AddWithValue("@IdVenta", IdVenta);
                    _command.ExecuteNonQuery();
                    _connection.Close();
                    _connection.Dispose();
                    _command.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public Venta MostrarIDVenta(int IdVenta)
            {
                try
                {
                    _connection = new SqlConnection(StringConexion);
                    _connection.Open();
                    _command = new SqlCommand();
                    _command.Connection = _connection;

                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "[Sp_Most_IDVenta]";
                    _command.Parameters.AddWithValue("@IdVenta", IdVenta);
                    _reader = _command.ExecuteReader();
                    Venta temp = null;

                    if (_reader.Read())
                    {
                        temp = new Venta();
                        temp.IdVenta = int.Parse(_reader.GetValue(0).ToString());
                        temp.FechaVenta = DateTime.Parse(_reader.GetValue(1).ToString());
                        temp.TotalVenta = double.Parse(_reader.GetValue(2).ToString());
                        temp.MetodoPago = _reader.GetValue(3).ToString();
                        temp.PuntosUsados = int.Parse(_reader.GetValue(4).ToString());
                        temp.CantidadVendido = int.Parse(_reader.GetValue(5).ToString());
                        temp.IDCosmetico = int.Parse(_reader.GetValue(6).ToString());
                        temp.IDConsumidor = int.Parse(_reader.GetValue(7).ToString());
                        temp.EstadoVenta = _reader.GetValue(8).ToString();
                    }
                    _connection.Close();
                    _connection.Dispose();
                    _command.Dispose();
                    return temp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public DataSet BuscarPorEstadoVenta(string estadoVenta)
            {
                try
                {
                    _connection = new SqlConnection(StringConexion);
                    _connection.Open();
                    _command = new SqlCommand();
                    _command.Connection = _connection;

                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "[Sp_Most_EstadoVenta]";
                    _command.Parameters.AddWithValue("@EstadoVenta", estadoVenta);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    DataSet datos = new DataSet();
                    adapter.SelectCommand = _command;
                    adapter.Fill(datos);
                    _connection.Close();
                    _connection.Dispose();
                    _command.Dispose();
                    adapter.Dispose();
                    return datos;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
//metodos agregados por sebas en la noche

        public DataSet CosmeticosId()
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Obtener_IDCosmetico]"; 

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet datos = new DataSet();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                adapter.Dispose();

                return datos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet ObtenerIDConsumidores()
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "Sp_Obtener_IDConsumidores"; // Asegúrate de que el SP existe

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet datos = new DataSet();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                adapter.Dispose();

                return datos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Cosmetico BuscarPorIdCosmetico(int IdCosmetico)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_IDCosmetico]";
                _command.Parameters.AddWithValue("@IDCosmetico", IdCosmetico);
                _reader = _command.ExecuteReader();

                Cosmetico temp = null;

                if (_reader.Read())
                {
                    temp = new Cosmetico();
                    temp.IDCosmetico = int.Parse(_reader.GetValue(0).ToString());
                    temp.Nombre = _reader.GetValue(1).ToString();
                    temp.Marca = _reader.GetValue(2).ToString();
                    temp.PrecioUnitario = double.Parse(_reader.GetValue(3).ToString());
                    temp.StockDisponible = int.Parse(_reader.GetValue(4).ToString());
                    temp.FechaVencimiento = DateTime.Parse(_reader.GetValue(5).ToString());
                    temp.Categoria = _reader.GetValue(6).ToString();
                    temp.EstadoProducto = _reader.GetValue(7).ToString();
                    temp.Imagen = _reader.GetValue(8).ToString();
                }

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();

                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet VentaPorConsumidor(int consumidor)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_VentaConsumidor]";
                _command.Parameters.AddWithValue("@IDConsumidor", consumidor);
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet datos = new DataSet();
                adapter.SelectCommand = _command;
                adapter.Fill(datos);
                _connection.Close();
                _connection.Dispose();
                _command.Dispose();
                adapter.Dispose();
                return datos;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Consumidor BuscarPorIdConsumidor(int IdConsumidor)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Most_IDConsumidor]";
                _command.Parameters.AddWithValue("@IDConsumidor", IdConsumidor);
                _reader = _command.ExecuteReader();

                Consumidor temp = null;

                if (_reader.Read())
                {
                    temp = new Consumidor();
                    temp.IdConsumidor = int.Parse(_reader.GetValue(0).ToString());
                    temp.NombreCompleto = _reader.GetValue(1).ToString();
                    temp.telefono = _reader.GetValue(2).ToString();
                    temp.CorreoElectronico = _reader.GetValue(3).ToString();
                    temp.FechaRegistro = DateTime.Parse(_reader.GetValue(4).ToString());
                    temp.FrecuenciaCompra = _reader.GetValue(5).ToString();
                    temp.PuntosFidelidad = int.Parse(_reader.GetValue(6).ToString());
                    temp.Direccion = _reader.GetValue(7).ToString();
                }

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();

                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VentasPendientes(int idCosmetico)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Verificar_VentasPendientes]";
                _command.Parameters.AddWithValue("@IDCosmetico", idCosmetico);

                int resultado = (int)_command.ExecuteScalar(); // Ejecuta y obtiene un número

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();

                return resultado > 0; // Si hay ventas pendientes, retorna true
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar ventas pendientes: " + ex.Message, ex);
            }
        }

        public bool CosmeticoVendido(int idCosmetico)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Verificar_CosmeticoVendido]";
                _command.Parameters.AddWithValue("@IDCosmetico", idCosmetico);

                int resultado = (int)_command.ExecuteScalar(); // Ejecuta y obtiene un número

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();

                return resultado > 0; // Si el cosmético ha sido vendido, retorna true
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si el cosmético fue vendido: " + ex.Message, ex);
            }
        }

        public bool PuedeUsarPuntosFidelidad(int idConsumidor)
        {
            try
            {
                _connection = new SqlConnection(StringConexion);
                _connection.Open();
                _command = new SqlCommand();
                _command.Connection = _connection;

                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[Sp_Validar_Uso_Puntos]";
                _command.Parameters.AddWithValue("@IDConsumidor", idConsumidor);

                int resultado = (int)_command.ExecuteScalar(); // Obtiene un número (0 o 1)

                _connection.Close();
                _connection.Dispose();
                _command.Dispose();

                return resultado > 0; // Si ha hecho 3 compras sin puntos, retorna true
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar uso de puntos de fidelidad: " + ex.Message, ex);
            }
        }


    }//fin para la clase
}//fin para el namespace
