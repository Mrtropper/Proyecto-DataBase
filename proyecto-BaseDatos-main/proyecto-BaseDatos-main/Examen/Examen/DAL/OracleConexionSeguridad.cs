using System;
using System.Configuration; // Asegúrate de que esta línea esté presente
using System.Data;
using Oracle.ManagedDataAccess.Client;
using BCrypt.Net;
using System.Collections.Generic;

namespace DAL
{
    public class OracleConexionSeguridad
    {
        private string StringConexion;
        private OracleConnection _connection;
        private OracleCommand _command;
        private OracleDataReader _reader;

        public OracleConexionSeguridad(string connectionString)
        {
            StringConexion = connectionString;
            _connection = new OracleConnection(StringConexion);
            _command = new OracleCommand();
            _command.Connection = _connection;
        }

        public string LoginUsuario(string nombreUsuario, string clave, string nombreSistema)
        {
            try
            {
                clave = clave?.Trim();

                _connection = new OracleConnection(StringConexion);
                _connection.Open();

                _command = new OracleCommand("Sp_Login", _connection);
                _command.CommandType = CommandType.StoredProcedure;

                _command.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = nombreUsuario;
                _command.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = nombreSistema;

                var claveHashOut = new OracleParameter("pClaveHash", OracleDbType.Varchar2, 100);
                claveHashOut.Direction = ParameterDirection.Output;
                _command.Parameters.Add(claveHashOut);

                var statusOut = new OracleParameter("pStatus", OracleDbType.Varchar2, 50);
                statusOut.Direction = ParameterDirection.Output;
                _command.Parameters.Add(statusOut);

                var mensajeOut = new OracleParameter("pMensaje", OracleDbType.Varchar2, 250);
                mensajeOut.Direction = ParameterDirection.Output;
                _command.Parameters.Add(mensajeOut);

                _command.ExecuteNonQuery();

                string mensaje = mensajeOut.Value?.ToString();
                string claveHash = claveHashOut.Value?.ToString();
                string estado = statusOut.Value?.ToString();

                if (mensaje != "OK")
                    return mensaje;

                bool validacion = BCrypt.Net.BCrypt.Verify(clave, claveHash);
                if (!validacion)
                    return "credencial invalida";

                if (estado?.ToLower() != "activo")
                    return "El status del perfil es inactivo";

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

        public DataTable MostrarPermisosTotalesUsuario(string nombreUsuario, string sistema)
        {
            using (OracleConnection conn = new OracleConnection(StringConexion))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("Sp_Most_Permisos_Completos_Usuario", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    cmd.Parameters.Add("pNombreUsuario", OracleDbType.Varchar2).Value = nombreUsuario;
                    cmd.Parameters.Add("pNombreSistema", OracleDbType.Varchar2).Value = sistema;
                    cmd.Parameters.Add("pResultado", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
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



    }
}