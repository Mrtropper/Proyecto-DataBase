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

namespace ExamenGrupo5
{
    public partial class EditarCompras : Form
    {
        private OracleConexion _conexion = null;
        public EditarCompras()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            _conexion = new OracleConexion(connectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e) // acción del botón de inicio de sesión
        {
            try
            {
                string nombreUsuario = txt_username.Text;
                string clave = txt_Clave.Text;
                if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(clave))
                {
                    MessageBox.Show("Por favor, ingrese un nombre de usuario y una clave.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
             //   string resultado = _conexion.LoginUsuario(nombreUsuario, clave, "negocio1");

                //switch (resultado)
                //{
                //    case "Acceso permitido":
                //        MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        Principal principal = new Principal();
                //        principal.Show();
                //        this.Hide();
                //        break;
                //    case "El usuario no existe":
                //        MessageBox.Show("Acceso denegado. El usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        break;
                //    case "El status del perfil es inactivo":
                //        MessageBox.Show("Acceso denegado. Usuario Inactivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        break;
                //    case "Contraseña inválida":
                //        MessageBox.Show("Acceso denegado. Contraseña inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        break;
                //    default:
                //        MessageBox.Show("Error desconocido al iniciar sesión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        break;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSalir_Click(object sender, EventArgs e)
        {

        }
    }
}
