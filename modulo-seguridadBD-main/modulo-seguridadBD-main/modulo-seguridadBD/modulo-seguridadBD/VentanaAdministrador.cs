using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DAL;

namespace modulo_seguridadBD
{
    public partial class VentanaAdministrador : Form
    {
        private string connectionString = null;
        private ConexionOracle _conexion = null;
        private bool cerrarSesion = false;

        private string usuarioActual;
        private string NombreUsuarioTabla;

        public VentanaAdministrador(string usuario)
        {
            usuarioActual = usuario;
            connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);
            InitializeComponent();

            cmbSistemas.SelectedIndexChanged += cmbSistemas_SelectedIndexChanged;
            txtNombreUsuario.TextChanged += txtNombreUsuario_TextChanged;
            btnUserManagment.Click += btnUserManagment_Click;

            CargarSistemasComboBox();
        }

        private void VentanaAdministrador_Load(object sender, EventArgs e)
        {
            btnUserPermissions.Enabled = false;
            btnRoleAssignment.Enabled = false;

            cmbSistemas.Items.Clear();
            cmbSistemas.Items.Add("negocio1");
            cmbSistemas.Items.Add("negocio2");

            cmbSistemas.SelectedIndex = 0;

            CargarUsuarios();
        }

        private void CargarUsuarios(string filtro = "")
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("La cadena de conexión no está definida.");
                    return;
                }

                ConexionOracle conexion = new ConexionOracle(connectionString);
                string sistemaSeleccionado = cmbSistemas.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(sistemaSeleccionado))
                {
                    MessageBox.Show("Seleccione un sistema.");
                    return;
                }

                string nombre = string.IsNullOrWhiteSpace(filtro) ? "" : filtro;
                DataTable dt = conexion.MostrarUsuariosPorSistema(nombre, sistemaSeleccionado);

                dgvUsuarios.DataSource = dt;

                // Eliminar columnas previas si recarga
                if (dgvUsuarios.Columns.Contains("seleccionar")) dgvUsuarios.Columns.Remove("seleccionar");

                // Marcar todas las demás columnas como solo lectura
                

                if (!dgvUsuarios.Columns.Contains("btnEditar"))
                    AgregarBoton("btnEditar", "Editar");

                if (!dgvUsuarios.Columns.Contains("btnEliminar"))
                    AgregarBoton("btnEliminar", "Eliminar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void txtNombreUsuario_TextChanged(object sender, EventArgs e)
        {
            CargarUsuarios(txtNombreUsuario.Text.Trim());
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

                string columna = dgvUsuarios.Columns[e.ColumnIndex].Name;

            if (columna == "seleccionar") return;

                NombreUsuarioTabla = dgvUsuarios.Rows[e.RowIndex].Cells["nombreUsuario"].Value.ToString();
                string sistemaSeleccionado = cmbSistemas.SelectedItem?.ToString();

            if (columna == "btnEditar")
            {
                this.Hide();
                var ventanaEditar = new VentanaEditarUsuario(NombreUsuarioTabla, sistemaSeleccionado);
                ventanaEditar.Owner = this; // ¡Este es el cambio clave!
                ventanaEditar.Show();
            }
            else if (columna == "btnEliminar")
            {
                DialogResult result = MessageBox.Show($"¿Deseas eliminar el usuario?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _conexion.EliminarUsuario(NombreUsuarioTabla);
                    _conexion.VerificarYCrearUsuarioDefault();

                    MessageBox.Show("Usuario eliminado correctamente.");
                    CargarUsuarios();
                }
            }

        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            dgvUsuarios.ClearSelection();
            dgvUsuarios.Rows[e.RowIndex].Selected = true;

            NombreUsuarioTabla = dgvUsuarios.Rows[e.RowIndex].Cells["nombreUsuario"].Value.ToString();
            btnUserPermissions.Enabled = true;
            btnRoleAssignment.Enabled = true;
        }

        private void cmbSistemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarUsuarios();
            btnUserPermissions.Enabled = false;
            btnRoleAssignment.Enabled = false;
            NombreUsuarioTabla = null;
        }

        private void AgregarUsuario_Click(object sender, EventArgs e)
        {
            this.Hide();
            var crearUsuario = new CrearUsuario(usuarioActual);
            crearUsuario.Owner = this;
            crearUsuario.Show();
        }

        private void btnUserPermissions_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NombreUsuarioTabla))
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sistemaSeleccionado = cmbSistemas.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(sistemaSeleccionado))
            {
                MessageBox.Show("Seleccione un sistema válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();
            var ventanaPermisos = new VentanaPermisosUsuario(usuarioActual, NombreUsuarioTabla, sistemaSeleccionado);
            ventanaPermisos.Owner = this;
            ventanaPermisos.Show();
        }

        private void btnRoleAssignment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NombreUsuarioTabla))
            {
                MessageBox.Show("Debe seleccionar un usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sistemaSeleccionado = cmbSistemas.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(sistemaSeleccionado))
            {
                MessageBox.Show("Seleccione un sistema válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();
            var ventanaRolUsuario = new VentanaRoleUser(usuarioActual, NombreUsuarioTabla, sistemaSeleccionado);
            ventanaRolUsuario.Owner = this;
            ventanaRolUsuario.Show();
        }

        private void btnVentanaSistema_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventanaSistemas = new VentanasSistema(usuarioActual);
            ventanaSistemas.Owner = this;
            ventanaSistemas.Show();
        }

        private void btnUserManagment_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventanaAdministrador = new VentanaAdministrador(usuarioActual);
            ventanaAdministrador.Owner = this;
            ventanaAdministrador.Show();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            var login = new VentanaLogin();
            login.Show();

            if (this.Owner is VentanaAdministrador admin)
            {
                admin.cerrarSesion = true;
                admin.Close();
            }

            this.Close();
        }

        private void CargarSistemasComboBox()
        {
            try
            {
                DataTable dt = _conexion.ObtenerSistemasComboBox();
                cmbSistemas.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    cmbSistemas.Items.Add(row["nombreSistema"].ToString());
                }

                if (cmbSistemas.Items.Count > 0)
                    cmbSistemas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los sistemas: " + ex.Message);
            }
        }

        private void AgregarBoton(string nombre, string texto)
        {
            if (!dgvUsuarios.Columns.Contains(nombre))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = nombre,
                    HeaderText = texto,
                    Text = texto,
                    UseColumnTextForButtonValue = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgvUsuarios.Columns.Add(btn);
            }
        }

        private void BtnSistemas_Click(object sender, EventArgs e)
        {
            this.Hide();
            var adminitracionSistema = new AdminitracionSistema(usuarioActual);
            adminitracionSistema.Owner = this;
            adminitracionSistema.Show();
        }

        private void btnRoleMagment_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventanaRoleManagement = new VentanaRoleManagement(usuarioActual);
            ventanaRoleManagement.Owner = this;
            ventanaRoleManagement.Show();
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
