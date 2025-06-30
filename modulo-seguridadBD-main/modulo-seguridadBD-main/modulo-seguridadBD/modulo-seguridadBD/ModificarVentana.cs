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

namespace modulo_seguridadBD
{
    public partial class ModificarVentana : Form
    {
        private ConexionOracle _conexion = null;
        private string usuarioActual;
        private int idVentana;
        public ModificarVentana(int idventanan,string usuarioActual)
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);
            this.usuarioActual = usuarioActual;
            this.idVentana = idventanan;
            CargarSistemasComboBox();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            // Validar que los campos de texto no estén vacíos  
            if (string.IsNullOrWhiteSpace(TxtScreenName.Text))
            {
                MessageBox.Show("El campo de nombre de usuario no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string statusSistema = cbStatus.SelectedItem?.ToString();
            string nombreSistema = cbSistema.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(nombreSistema))
            {
                MessageBox.Show("Debe seleccionar un sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(statusSistema))
            {
                MessageBox.Show("Debe seleccionar un estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string resultado = _conexion.ActualizarVentana(idVentana,TxtScreenName.Text,statusSistema,nombreSistema,usuarioActual);
                MessageBox.Show(resultado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al crear el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            VentanasSistema ventanaSistema = new VentanasSistema(usuarioActual);
            ventanaSistema.Owner = this.Owner; // Mantener la referencia a la ventana padre
            ventanaSistema.Show();
        }

        private void CargarSistemasComboBox()
        {
            try
            {
                DataTable dt = _conexion.ObtenerSistemasComboBox();
                cbSistema.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    string nombreSistema = row["nombreSistema"].ToString();
                    if (!nombreSistema.Equals("seguridad", StringComparison.OrdinalIgnoreCase))
                    {
                        cbSistema.Items.Add(nombreSistema);
                    }
                }

                if (cbSistema.Items.Count > 0)
                    cbSistema.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los sistemas: " + ex.Message);
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
