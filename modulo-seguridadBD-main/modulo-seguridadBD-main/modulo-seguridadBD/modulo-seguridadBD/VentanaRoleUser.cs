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
    public partial class VentanaRoleUser : Form
    {
        private string nombreUsuarioSeleccionado = null;
        private string sistemaSeleccionado = null;
        private ConexionOracle conexion;
        private string usuarioActual ;
        public VentanaRoleUser(String usuarioActual, String NombreUsuarioTabla, String sistemaSeleccionado)
        {
            InitializeComponent();
            this.usuarioActual = usuarioActual;

            this.nombreUsuarioSeleccionado = NombreUsuarioTabla;
            this.sistemaSeleccionado = sistemaSeleccionado;

            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            conexion = new ConexionOracle(connectionString);

            this.Load += VentanaRoleUser_Load;
            dgvAvailableRole.CellContentClick += dgvAvailableRole_CellContentClick;
            dgvCurrentRole.CellContentClick += dgvCurrentRole_CellContentClick;
            txtNombreUsuario.Text = nombreUsuarioSeleccionado;
        }

        private void VentanaRoleUser_Load(object sender, EventArgs e)
        { 
            CargarRoles();
        }

        private void CargarRoles()
        {
            CargarRolesDisponibles();
            CargarRolesAsignados();
        }

        private void CargarRolesDisponibles()
        {
            var dt = conexion.MostrarRolesNoAsignados(nombreUsuarioSeleccionado, sistemaSeleccionado);
            dgvAvailableRole.DataSource = dt;

            if (!dgvAvailableRole.Columns.Contains("btnAsignar"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = "btnAsignar",
                    HeaderText = "Asignar",
                    Text = "Asignar",
                    UseColumnTextForButtonValue = true
                };
                dgvAvailableRole.Columns.Add(btn);
            }
        }

        private void CargarRolesAsignados()
        {
            var dt = conexion.MostrarRolesAsignados(nombreUsuarioSeleccionado, sistemaSeleccionado);
            dgvCurrentRole.DataSource = dt;

            if (!dgvCurrentRole.Columns.Contains("btnEliminar"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = "btnEliminar",
                    HeaderText = "Eliminar",
                    Text = "Eliminar",
                    UseColumnTextForButtonValue = true
                };
                dgvCurrentRole.Columns.Add(btn);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void btnUserManagment_Click(object sender, EventArgs e)
        {
            this.Hide();
            VentanaAdministrador ventanaAdministrador = new VentanaAdministrador(usuarioActual);
            ventanaAdministrador.Owner = this;
            ventanaAdministrador.Show();
        }

        private void btnRoleMagment_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventanaRoles = new VentanaRoleManagement(usuarioActual);
            ventanaRoles.Owner = this;
            ventanaRoles.Show();
        }

        private void btnRoleAssignment_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventanaRoles = new VentanaRoleManagement(usuarioActual);
            ventanaRoles.Owner = this;
            ventanaRoles.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventanaPermisos = new VentanaPermisosUsuario(usuarioActual, nombreUsuarioSeleccionado, sistemaSeleccionado);
            ventanaPermisos.Owner = this;
            ventanaPermisos.Show();
        }

        private void dgvAvailableRole_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvAvailableRole.Columns[e.ColumnIndex].Name != "btnAsignar") return;

            int idRol = Convert.ToInt32(dgvAvailableRole.Rows[e.RowIndex].Cells["IDROL"].Value);
            conexion.AsignarRolUsuario(nombreUsuarioSeleccionado, idRol);
            MessageBox.Show("Rol asignado correctamente.");
            CargarRoles();
        }

        private void dgvCurrentRole_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvCurrentRole.Columns[e.ColumnIndex].Name != "btnEliminar") return;

            int idRol = Convert.ToInt32(dgvCurrentRole.Rows[e.RowIndex].Cells["IDROL"].Value);
            conexion.EliminarRolUsuario(nombreUsuarioSeleccionado, idRol);
            MessageBox.Show("Rol eliminado correctamente.");
            CargarRoles();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
        }

        private void btnViewStory_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show(); // Mostrar la ventana anterior
            }
            this.Close(); // Cierra esta
        }
    }
}