using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using DAL;
using System.Configuration;
using DAL;
using System.Drawing;

namespace modulo_seguridadBD
{
    public partial class AdminitracionSistema : Form
    {

        private string nombreUsuarioSeleccionado = null;
        private string sistemaSeleccionado = null;

        private string connectionString = null;
        private ConexionOracle _conexion = null;

        private string usuarioActual;
        public AdminitracionSistema(string usuario)
        {
            connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);
            InitializeComponent();
            CargarSistemas();
  
            btnUserManagment.Click += btnUserManagment_Click;
            usuarioActual = usuario;

            dgvSistemas.ReadOnly = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void dtgUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void CargarSistemas(string filtro = "")
        {
            try
            {
                ConexionOracle conexion = new ConexionOracle(connectionString);

                string nombre = string.IsNullOrWhiteSpace(filtro) ? null : filtro;
                DataTable dt = conexion.ObtenerSistemas();
                dgvSistemas.DataSource = dt;

                // Verificar si las columnas ya existen antes de agregarlas  
                if (!dgvSistemas.Columns.Contains("btnEditar"))
                    AgregarBoton("btnEditar", "Editar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }
    
        private void txtNombreUsuario_TextChanged(object sender, EventArgs e)
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
            VentanaRoleManagement ventanaRoles = new VentanaRoleManagement(usuarioActual);
            ventanaRoles.Owner = this;
            ventanaRoles.Show();
        }

     


        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columna = dgvSistemas.Columns[e.ColumnIndex].Name;
            string nombreSistema = dgvSistemas.Rows[e.RowIndex].Cells["NOMBRESISTEMA"].Value.ToString();


            if (columna == "btnEditar")
            {
                EditarSistemas modificarSistemas = new EditarSistemas(nombreSistema);
                modificarSistemas.Show();
                
            }
            
               
        }


        private void AgregarUsuario_Click(object sender, EventArgs e)
        {
            CrearVentana crearVentana = new CrearVentana(usuarioActual);
            crearVentana.Show();
            this.Dispose();

        }

         //--------------------------
        private void AgregarBoton(string nombre, string texto)
        {
            // Verificar si el botón ya existe antes de agregarlo  
            if (!dgvSistemas.Columns.Contains(nombre))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = nombre,
                    HeaderText = texto,
                    Text = texto,
                    UseColumnTextForButtonValue = true,

                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgvSistemas.Columns.Add(btn);
            }
        }

        private void cmbSistemas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUserManagment_Click_1(object sender, EventArgs e)
        {
            Dispose();
            VentanaAdministrador ventanaAdministrador = new VentanaAdministrador(usuarioActual);
            ventanaAdministrador.Owner = this;
      

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnCrearSistema_Click(object sender, EventArgs e)
        {
            this.Hide();
            creacionSistemas ventanaCrearSistema = new creacionSistemas();
            ventanaCrearSistema.FormClosed += (s, args) =>
            {
                this.Show();
                CargarSistemas(); // Recargar los datos actualizados  
            };
            ventanaCrearSistema.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show(); // Mostrar la ventana anterior
            }
            this.Close(); // Cierra esta
        }

        //-----------------------------------------------------------




    }

}
