using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using BCrypt.Net;



namespace DAL
{
    public class ConexionOracle
    {
        private string StringConexion;
        private OracleConnection _connection;
        private OracleCommand _command;
        private OracleDataReader _reader;

        public ConexionOracle(string pStringCnx)
        {
            StringConexion = pStringCnx;
        }

        public DataTable ObtenerUsuarios(string nombreUsuario)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_Usuario", _connection);
                _command.CommandType = CommandType.StoredProcedure;


                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombreUsuario;


                OracleParameter cursor = new OracleParameter("pResultado", OracleDbType.RefCursor);
                cursor.Direction = ParameterDirection.Output;
                _command.Parameters.Add(cursor);

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public string LoginUsuario(string nombreUsuario, string clave, string nombreSistema)
        {
            OracleConnection _connection = null;
            OracleCommand _command = null;

            try
            {
                clave = clave?.Trim();

                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Login", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada
                _command.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = nombreUsuario;
                _command.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = nombreSistema;

                // Parámetros de salida (orden correcto según tu procedimiento)
                var claveHashOut = new OracleParameter("pClaveHash", OracleDbType.Varchar2, 250)
                {
                    Direction = ParameterDirection.Output
                };
                _command.Parameters.Add(claveHashOut);

                var statusOut = new OracleParameter("pStatus", OracleDbType.Varchar2, 50)
                {
                    Direction = ParameterDirection.Output
                };
                _command.Parameters.Add(statusOut);

                var mensajeOut = new OracleParameter("pMensaje", OracleDbType.Varchar2, 250)
                {
                    Direction = ParameterDirection.Output
                };
                _command.Parameters.Add(mensajeOut);

                // Ejecutar procedimiento
                _command.ExecuteNonQuery();

                // Recuperar valores
                string claveHash = claveHashOut.Value?.ToString();
                string status = statusOut.Value?.ToString();
                string mensaje = mensajeOut.Value?.ToString();

            
                if (mensaje != "OK")
                    return mensaje;

                // Validar la contraseña
                bool validacion = BCrypt.Net.BCrypt.Verify(clave, claveHash);
                if (!validacion)
                    return "credencial invalida";

                if (status?.ToLower() != "activo")
                    return "El status del perfil es inactivo";

                // Establecer sesión (si tienes un método para eso)
                EstablecerIdentificadorSesion(nombreUsuario);

                return "Acceso permitido";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar iniciar sesión: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }





        public void EstablecerIdentificadorSesion(string usuario)
        {
            using (var conexion = new OracleConnection(StringConexion))
            {
                conexion.Open();
                using (var comando = new OracleCommand("BEGIN DBMS_SESSION.SET_IDENTIFIER(:usuario); END;", conexion))
                {
                    comando.Parameters.Add(":usuario", OracleDbType.Varchar2).Value = usuario;
                    comando.ExecuteNonQuery();
                }
            }
        }






        //------------------------USUARIO 
        public string InsertarUsuario(string nombreUsuario, string clave, string status, string nombreSistema)
        {
            try
            {
                using (var connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (var command = new OracleCommand("Sp_Ins_Usuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = nombreUsuario;
                        command.Parameters.Add("pClave", OracleDbType.Varchar2).Value = clave;
                        command.Parameters.Add("pStatus", OracleDbType.Varchar2).Value = status;
                        command.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = nombreSistema;

                        var output = command.Parameters.Add("pMensaje", OracleDbType.Varchar2, 250);
                        output.Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                        return output.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el usuario en Oracle: " + ex.Message, ex);
            }
        }


        public void EliminarUsuario(string nombreUsuario)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Del_Usuario", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = nombreUsuario;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }


        public string ActualizarUsuario(string claveNueva, string nuevoStatus, string nombreUsuarioModificado)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Upd_Usuario", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pClave", OracleDbType.Varchar2).Value = claveNueva;
                _command.Parameters.Add("pStatus", OracleDbType.Varchar2).Value = nuevoStatus;
                _command.Parameters.Add("pnombreUsuarioModificado", OracleDbType.Varchar2).Value = nombreUsuarioModificado;

                _command.ExecuteNonQuery();

                return "Usuario actualizado correctamente.";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }






        public DataSet BuscarUsuariosPorNombre(string nombre)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_Usuario", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombre;
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuarios: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public DataSet MostrarUsuarios()
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_Usuario", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Pasamos cadena vacía para traer todos los usuarios
                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = "";
                _command.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar usuarios: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }




        //------------------------USUARIO 

        public void InsertarRol(string nombreRol, string status, string nombreSistema, string usuarioActual)
        {
            try
            {
                using (var connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (var command = new OracleCommand("Sp_Ins_Rol", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("pNombreRol", OracleDbType.Varchar2).Value = nombreRol;
                        command.Parameters.Add("pStatus", OracleDbType.Varchar2).Value = status;
                        command.Parameters.Add("pUsuarioActual", OracleDbType.Varchar2).Value = usuarioActual;
                        command.Parameters.Add("pSistema", OracleDbType.Varchar2).Value = nombreSistema;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar rol: " + ex.Message, ex);
            }
        }


        public void ActualizarRol(int idRol, string nuevoNombreRol, string nuevoStatus, string nombreSistema, string usuarioActual)
        {
            try
            {
                using (var connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (var command = new OracleCommand("Sp_Upd_Rol", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("pIdRol", OracleDbType.Int32).Value = idRol;
                        command.Parameters.Add("pNuevoNombreRol", OracleDbType.Varchar2).Value = nuevoNombreRol;
                        command.Parameters.Add("pNuevoStatus", OracleDbType.Varchar2).Value = nuevoStatus;
                        command.Parameters.Add("pUsuarioActual", OracleDbType.Varchar2).Value = usuarioActual;
                        command.Parameters.Add("pSistema", OracleDbType.Varchar2).Value = nombreSistema;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar rol: " + ex.Message, ex);
            }
        }




        //-------------------------------------ventanas

        public DataTable ObtenerVentanas(string nombreBuscado)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_Ventana", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada
                _command.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombreBuscado;

                OracleParameter resultado = new OracleParameter("pResultado", OracleDbType.RefCursor);
                resultado.Direction = ParameterDirection.Output;
                _command.Parameters.Add(resultado);

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventanas: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public DataTable ObtenerVentanasNegocio(string nombreNegocio)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Most_VentanaNegocio", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                // Parámetro de entrada
                _command.Parameters.Add("pNegocio", OracleDbType.Varchar2).Value = nombreNegocio;

                // Parámetro de salida (REF CURSOR)
                OracleParameter resultado = new OracleParameter("pResultado", OracleDbType.RefCursor);
                resultado.Direction = ParameterDirection.Output;
                _command.Parameters.Add(resultado);

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventanas: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }
        public void EliminarVentana(int idVentana, string usuarioActual)
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Del_Ventana", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pIdVentana", OracleDbType.Int32).Value = idVentana;
                _command.Parameters.Add("pUsuarioActual", OracleDbType.Varchar2).Value = usuarioActual;

                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la ventana (soft delete) en Oracle: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                if (_connection?.State == ConnectionState.Open)
                    _connection.Close();
                _connection?.Dispose();
            }
        }

        public string InsertarVentana(string nombreVentana, string status, string usuarioActual, string nombreSistema)
        {
            try
            {
                using (var connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (var command = new OracleCommand("Sp_Ins_Ventana", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("pNombreVentana", OracleDbType.Varchar2).Value = nombreVentana;
                        command.Parameters.Add("pStatus", OracleDbType.Varchar2).Value = status;
                        command.Parameters.Add("pUsuarioActual", OracleDbType.Varchar2).Value = usuarioActual;
                        command.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = nombreSistema;

                        OracleParameter mensaje = new OracleParameter("pMensaje", OracleDbType.Varchar2, 250);
                        mensaje.Direction = ParameterDirection.Output;
                        command.Parameters.Add(mensaje);

                        command.ExecuteNonQuery();

                        return mensaje.Value?.ToString() ?? "No se recibió mensaje";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la ventana en Oracle: " + ex.Message, ex);
            }
        }


        public string ActualizarVentana(int idVentana, string nuevoNombreVentana, string nuevoStatus, string nuevoNombreSistema, string usuarioActual)
        {
            try
            {
                using (var connection = new OracleConnection(StringConexion))
                {
                    connection.Open();

                    using (var command = new OracleCommand("Sp_Upd_Ventana", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("pIdVentana", OracleDbType.Int32).Value = idVentana;
                        command.Parameters.Add("pNuevoNombreVentana", OracleDbType.Varchar2).Value = nuevoNombreVentana;
                        command.Parameters.Add("pNuevoStatus", OracleDbType.Varchar2).Value = nuevoStatus;
                        command.Parameters.Add("pNuevoNombreSistema", OracleDbType.Varchar2).Value = nuevoNombreSistema;
                        command.Parameters.Add("pUsuarioActual", OracleDbType.Varchar2).Value = usuarioActual;

                        command.ExecuteNonQuery();

                        return "Ventana actualizada correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar la ventana: " + ex.Message;
            }
        }




        //-------------------------------------ventanas


        //---------------------------------------sistemas

        public DataTable MostrarNombreSistemas()
        {
            try
            {
                using (var conn = new OracleConnection(StringConexion))
                {
                    conn.Open();
                    using (var cmd = new OracleCommand("Sp_Most_Sistemas", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetro de salida tipo REF CURSOR
                        cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar los nombres de los sistemas: " + ex.Message, ex);
            }
        }


        public DataTable MostrarSistemasAsignadosUsuario(string nombreUsuario)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(StringConexion))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("Sp_Most_SistemasUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = nombreUsuario;
                        cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar sistemas del usuario: " + ex.Message, ex);
            }
        }

        public void InsertarSistemaUsuario(string sistema, string usuario)
        {
            using (var conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (var cmd = new OracleCommand("Sp_Ins_SistemaUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = sistema;
                    cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = usuario;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarSistemaUsuario(string sistema, string usuario)
        {
            using (var conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (var cmd = new OracleCommand("Sp_Del_SistemaUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = sistema;
                    cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = usuario;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerSistemasComboBox()
        {
            try
            {
                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_ObtenerSistemas", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pCursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter adapter = new OracleDataAdapter(_command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener sistemas desde el procedimiento: " + ex.Message, ex);
            }
            finally
            {
                _command?.Dispose();
                _connection?.Close();
                _connection?.Dispose();
            }
        }


        //-------------------------------------------------------------Sistemas

        public DataTable ObtenerSistemas()
        {
            DataTable tabla = new DataTable();

            try
            {
                using (var conexion = new OracleConnection(StringConexion))
                {
                    conexion.Open();

                    using (var comando = new OracleCommand("Sp_Most_Sistemas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        // Parámetro de salida: cursor
                        OracleParameter cursor = new OracleParameter("pResultado", OracleDbType.RefCursor);
                        cursor.Direction = ParameterDirection.Output;
                        comando.Parameters.Add(cursor);

                        // Ejecutamos y llenamos la tabla con el cursor
                        using (var adaptador = new OracleDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los sistemas desde Oracle: " + ex.Message, ex);
            }

            return tabla;
        }

        public void ActualizarSistemaConMensaje(string nombreSistema, string descripcion)
        {
            using (var conn = new OracleConnection(StringConexion))
            using (var cmd = new OracleCommand("Sp_Ins_Sistema", conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = nombreSistema;
                cmd.Parameters.Add("pDescripcion", OracleDbType.Varchar2).Value = descripcion;

                cmd.ExecuteNonQuery();
            }
        }


        public void ActualizarSistema(int nuevaDescripcion, string nombreSistema)
        {
            using (var conn = new OracleConnection(StringConexion))
            using (var cmd = new OracleCommand("Sp_Upd_Sistema", conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pDescripcion", OracleDbType.Int32).Value = nuevaDescripcion;
                cmd.Parameters.Add("pNuevoNombre", OracleDbType.Varchar2).Value = nombreSistema;

                cmd.ExecuteNonQuery();
            }
        }

        public string ActualizarSistema(string nombreSistema, string descripcion)
        {
            using (var connection = new OracleConnection(StringConexion))
            using (var command = new OracleCommand("Sp_Upd_Sistema", connection))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada
                command.Parameters.Add("pDescripcion", OracleDbType.Varchar2).Value = descripcion;
                command.Parameters.Add("pNuevoNombre", OracleDbType.Varchar2).Value = nombreSistema;

                // Parámetro de salida
                var mensajeParam = new OracleParameter("pMensaje", OracleDbType.Varchar2, 250);
                mensajeParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(mensajeParam);

                command.ExecuteNonQuery();

                return mensajeParam.Value.ToString();
            }
        }

        public List<string> ObtenerSistemasUsuario(string nombreUsuario)
        {
            List<string> sistemas = new List<string>();

            try
            {
                using (var conn = new OracleConnection(StringConexion))
                using (var cmd = new OracleCommand("SELECT nombreSistema FROM SistemaUsuario WHERE nombreUsuario = :usuario", conn))
                {
                    cmd.Parameters.Add("usuario", OracleDbType.Varchar2).Value = nombreUsuario;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sistemas.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener sistemas del usuario: " + ex.Message, ex);
            }

            return sistemas;
        }


       









































































































































































































































































































































































































































































































































































































































































































































        public DataTable MostrarRoles(string nombreRol, string sistema)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(StringConexion))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand("Sp_Most_Rol", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombreRol;
                        cmd.Parameters.Add("pSistema", OracleDbType.Varchar2).Value = sistema;

                        cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar roles: " + ex.Message, ex);
            }
        }


        public void EliminarRol(int idRol, string usuarioActual)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("Sp_Del_Rol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("pIdRol", OracleDbType.Int32).Value = idRol;
                    cmd.Parameters.Add("pUsuarioActual", OracleDbType.Varchar2).Value = usuarioActual;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public DataTable MostrarVentanasConPermisos(int idRol, string sistema)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("Sp_Most_VentanaPermisosRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("pIdRol", OracleDbType.Int32).Value = idRol;
                    cmd.Parameters.Add("pSistema", OracleDbType.Varchar2).Value = sistema;
                    cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public void AsignarPermiso(int idRol, int idVentana, int idPermiso)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("Sp_Ins_VentanaRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.BindByName = true; 

                    cmd.Parameters.Add("pIdVentana", OracleDbType.Int32).Value = idVentana;
                    cmd.Parameters.Add("pIdRol", OracleDbType.Int32).Value = idRol;
                    cmd.Parameters.Add("pIdPermisos", OracleDbType.Int32).Value = idPermiso;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void EliminarPermiso(int idRol, int idVentana, int idPermiso)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("Sp_Del_VentanaRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("pIdRol", OracleDbType.Int32).Value = idRol;
                    cmd.Parameters.Add("pIdVentana", OracleDbType.Int32).Value = idVentana;
                    cmd.Parameters.Add("pIdPermiso", OracleDbType.Int32).Value = idPermiso;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //Parte de ventanaPermisosUsuario
        public DataTable MostrarVentanasUsuario(string nombreUsuario, string sistema)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("Sp_Most_VentanaUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombreUsuario;
                    cmd.Parameters.Add("pSistema", OracleDbType.Varchar2).Value = sistema;
                    cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public void AsignarPermisoUsuario(string usuario, int idVentana, int idPermiso)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("Sp_Ins_VentanaUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = usuario;
                    cmd.Parameters.Add("pIdVentana", OracleDbType.Int32).Value = idVentana;
                    cmd.Parameters.Add("pIdPermiso", OracleDbType.Int32).Value = idPermiso;

                    cmd.ExecuteNonQuery();

                }
            }
        }



        public void EliminarPermisoUsuario(string usuario, int idVentana, int idPermiso)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("Sp_Del_VentanaUsuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = usuario;
                    cmd.Parameters.Add("pIdVentana", OracleDbType.Int32).Value = idVentana;
                    cmd.Parameters.Add("pIdPermiso", OracleDbType.Int32).Value = idPermiso;

                    cmd.ExecuteNonQuery();
                }
            }
        }







        //ROLES USUARIOS
        public DataTable MostrarRolesNoAsignados(string usuario, string sistema)
        {
            using (var conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (var cmd = new OracleCommand("Sp_Most_RolesNoAsignados", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = usuario;
                    cmd.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = sistema;
                    cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable MostrarRolesAsignados(string usuario, string sistema)
        {
            using (var conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (var cmd = new OracleCommand("Sp_Most_UsuarioRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = usuario;
                    cmd.Parameters.Add("pSistema", OracleDbType.Varchar2).Value = sistema;
                    cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public void AsignarRolUsuario(string usuario, int idRol)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("Sp_Ins_UsuarioRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = usuario;
                    cmd.Parameters.Add("pIdRol", OracleDbType.Int32).Value = idRol;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void EliminarRolUsuario(string usuario, int idRol)
        {
            using (var conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (var cmd = new OracleCommand("Sp_Del_UsuarioRol", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = usuario;
                    cmd.Parameters.Add("pIdRol", OracleDbType.Int32).Value = idRol;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public DataTable MostrarUsuariosPorSistema(string nombre, string sistema)
        {
            using (var conn = new OracleConnection(StringConexion))
            {
                conn.Open();
                using (var cmd = new OracleCommand("Sp_Most_UsuarioSistema", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de nombre (para filtrar, si aplica)
                    cmd.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = nombre;

                    // Parámetro del nombre del sistema
                    cmd.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = sistema;

                    // Cursor de salida
                    cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public void VerificarYCrearUsuarioDefault()
        {
            try
            {
                using (var conn = new OracleConnection(StringConexion))
                {
                    conn.Open();

                    using (var cmdCrear = new OracleCommand("Sp_CrearUsuarioDefault", conn))
                    {
                        cmdCrear.CommandType = CommandType.StoredProcedure;
                        cmdCrear.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar o activar usuario admin: " + ex.Message, ex);
            }
        }




    }
}
