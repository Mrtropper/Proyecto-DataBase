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
using ExamenGrupo5;

namespace modulo_seguridadBD
{
    public partial class VentanaLogin : Form
    {
        private string usuarioLogueado;

        private OracleConexionSeguridad _conexion = null;

        public VentanaLogin()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            _conexion = new OracleConexionSeguridad(connectionString);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtUsername.Text.Trim();
                string clave = txtPassword.Text.Trim();
                string sistemaActual = "negocio2"; // podrías obtenerlo de un ComboBox si deseas más adelante

                if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(clave))
                {
                    MessageBox.Show("Por favor, ingrese un nombre de usuario y una clave.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener lista de sistemas asociados al usuario
                List<string> sistemas = _conexion.ObtenerSistemasUsuario(nombreUsuario);

                if (!sistemas.Contains(sistemaActual, StringComparer.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Acceso denegado. El usuario no está asociado al sistema actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Si está asignado, intentar login
                string resultado = _conexion.LoginUsuario(nombreUsuario, clave, sistemaActual);

                switch (resultado)
                {
                    case "Acceso permitido":
                        MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        usuarioLogueado = nombreUsuario;
                        Principal ventanaPrincipal = new Principal(usuarioLogueado, sistemaActual);
                        ventanaPrincipal.Show();
                        this.Hide();
                        break;
                    case "El usuario no existe":
                        MessageBox.Show("Acceso denegado. El usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "El status del perfil es inactivo":
                        MessageBox.Show("Acceso denegado. Usuario Inactivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "credencial invalida":
                        MessageBox.Show("Acceso denegado. Contraseña inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Error desconocido al iniciar sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


   



    }
}
