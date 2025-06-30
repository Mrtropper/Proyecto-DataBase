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
    public partial class VentanaAgregarRol : Form
    {
        private ConexionOracle _conexion = null;

        public VentanaAgregarRol()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);

            CargarSistemasComboBox();

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
                string nombreRol = txtRoleName.Text.Trim();
                string estado = cbStatus.SelectedItem?.ToString();
                string sistema = cbSistema.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(nombreRol) || string.IsNullOrEmpty(estado) || string.IsNullOrEmpty(sistema))
                {
                    MessageBox.Show("Debe ingresar el nombre del rol y seleccionar un estado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _conexion.InsertarRol(nombreRol, estado, sistema, "NULL");

                MessageBox.Show("Rol insertado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtRoleName.Clear();
                cbStatus.SelectedIndex = -1;
                cbStatus.SelectedIndex = -1;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el rol: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
