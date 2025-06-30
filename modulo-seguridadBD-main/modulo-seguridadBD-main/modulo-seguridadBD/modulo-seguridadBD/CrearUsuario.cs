using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using DAL;

namespace modulo_seguridadBD
{
    public partial class CrearUsuario : Form
    {
        private ConexionOracle _conexion = null;
        private string usuarioActual;
        private string resultado;
        public CrearUsuario(string usuarioActual)
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);
            this.usuarioActual = usuarioActual;
            CargarSistemas();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtUsername.Text))
            {
                MessageBox.Show("El campo de nombre de usuario no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtPassword.Text))
            {
                MessageBox.Show("El campo de contraseña no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> sistemasSeleccionados = ObtenerSistemasSeleccionados();

            if (sistemasSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un sistema en la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hash = BCrypt.Net.BCrypt.HashPassword(TxtPassword.Text);

                foreach (string sistema in sistemasSeleccionados)
                {
                     resultado = _conexion.InsertarUsuario(TxtUsername.Text.Trim(), hash, "activo", sistema);
                    // Puedes verificar el resultado aquí si lo deseas
                }

                MessageBox.Show($"{resultado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al crear el usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarSistemas(string filtro = "")
        {
            try
            {
                DataTable dt = _conexion.MostrarNombreSistemas();
                dtgSistemas.DataSource = dt;

                if (!dtgSistemas.Columns.Contains("btnEditar"))
                    AgregarCheckbox("btnEditar", "Editar");

                foreach (DataGridViewColumn columna in dtgSistemas.Columns)
                {
                    if (columna.Name != "btnEditar")
                        columna.ReadOnly = true; // Solo permitir editar la columna checkbox
                    else
                        columna.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los sistemas: " + ex.Message);
            }
        }

        private void AgregarCheckbox(string nombre, string texto)
        {
            if (!dtgSistemas.Columns.Contains(nombre))
            {
                DataGridViewCheckBoxColumn checkbox = new DataGridViewCheckBoxColumn
                {
                    Name = nombre,
                    HeaderText = texto,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dtgSistemas.Columns.Add(checkbox);
            }
        }

        private List<string> ObtenerSistemasSeleccionados()
        {
            List<string> sistemasSeleccionados = new List<string>();

            foreach (DataGridViewRow fila in dtgSistemas.Rows)
            {
                if (fila.IsNewRow) continue;

                bool seleccionado = Convert.ToBoolean(fila.Cells["btnEditar"].Value);

                if (seleccionado)
                {
                    string nombreSistema = fila.Cells["NOMBRESISTEMA"].Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(nombreSistema))
                    {
                        sistemasSeleccionados.Add(nombreSistema);
                    }
                }
            }

            return sistemasSeleccionados;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Puedes dejar esto vacío o usarlo para cerrar/ocultar la ventana si es un botón de cerrar
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evento no utilizado por ahora
        }
    }
}
