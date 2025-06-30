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
    public partial class VentanaEditarRol : Form
    {
        private ConexionOracle _conexion = null;
        private int _idRol;
        private string _nombreSistema;

        public VentanaEditarRol(int idRol, string nombreSistema)
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);
            _idRol = idRol;
            _nombreSistema = nombreSistema;

            CargarDatosRol();

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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                int idRol = _idRol;

                string nuevoNombreRol = txtNombreRol.Text.Trim();
                string nuevoEstado = cmbEstado.SelectedItem?.ToString();
                string nombreSistema = cbSistema.SelectedItem?.ToString(); 

                if (string.IsNullOrEmpty(nuevoNombreRol) || string.IsNullOrEmpty(nuevoEstado))
                {
                    MessageBox.Show("Debe completar todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _conexion.ActualizarRol(idRol, nuevoNombreRol, nuevoEstado, nombreSistema, "NULL");

                MessageBox.Show("Rol actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: limpiar campos
                txtNombreRol.Clear();
                txtNombreRol.Clear();
                cmbEstado.SelectedIndex = -1;
                cbSistema.SelectedIndex = -1;
                this.Close(); // Cerrar la ventana después de actualizar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el rol: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosRol()
        {
            try
            {
                DataTable roles = _conexion.MostrarRoles("", _nombreSistema);

                var coincidencias = roles.AsEnumerable()
                    .Where(row => Convert.ToInt32(row["IDROL"]) == _idRol);

                if (coincidencias.Any())
                {
                    DataRow fila = coincidencias.First();
                    txtNombreRol.Text = fila["NOMBREROL"].ToString();
                    cmbEstado.SelectedItem = fila["STATUS"].ToString();
                    cbSistema.SelectedItem = _nombreSistema;
                }
                else
                {
                    MessageBox.Show("No se encontró el rol especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del rol: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

