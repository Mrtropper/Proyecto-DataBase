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
using Oracle.ManagedDataAccess.Client;

namespace modulo_seguridadBD
{
    public partial class creacionSistemas : Form
    {
        private ConexionOracle _conexion = null;

        public creacionSistemas()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);


        }




        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreSistema = txtNombreSistema.Text.Trim();
                string contraseña = TxtDescripcion.Text.Trim();
                InsertarSistema(nombreSistema, contraseña);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el sistema: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InsertarSistema(string nombreSistema, string contraseña)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreSistema))
                {
                    MessageBox.Show("El nombre del sistema no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(contraseña))
                {
                    MessageBox.Show("El nombre del sistema no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _conexion.ActualizarSistemaConMensaje(nombreSistema, contraseña);
                MessageBox.Show("Sistema ingresado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ingresar el sistema: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show(); // Mostrar la ventana anterior
            }
            this.Close(); // Cierra esta
        }
    }
}
