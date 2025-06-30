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
    public partial class VentanasSistema : Form
    {

        private string nombreUsuarioSeleccionado = null;
        private string sistemaSeleccionado = null;

        private string connectionString = null;
        private ConexionOracle _conexion = null;

        private string usuarioActual;
        public VentanasSistema(string usuario)
        {
            connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);
            InitializeComponent();
            CargarVentana();
  
            CargarSistemasComboBox();
            btnUserManagment.Click += btnUserManagment_Click;
            usuarioActual = usuario;

            dgvVentanas.ReadOnly = true;
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

      

        private void CargarVentana(string filtro = "")
        {
            try
            {
                ConexionOracle conexion = new ConexionOracle(connectionString);

                string nombre = string.IsNullOrWhiteSpace(filtro) ? null : filtro;
                DataTable dt = conexion.ObtenerVentanas(nombre);
                dgvVentanas.DataSource = dt;

                // Verificar si las columnas ya existen antes de agregarlas  
                if (!dgvVentanas.Columns.Contains("btnEditar"))
                    AgregarBoton("btnEditar", "Editar");

                if (!dgvVentanas.Columns.Contains("btnEliminar"))
                    AgregarBoton("btnEliminar", "Eliminar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }
        private void CargarSistemasComboBox()
        {
            try
            {
                DataTable dt = _conexion.ObtenerSistemasComboBox();
                cmbSistemas.Items.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    string nombreSistema = row["nombreSistema"].ToString();
                    if (!nombreSistema.Equals("seguridad", StringComparison.OrdinalIgnoreCase))
                    {
                        cmbSistemas.Items.Add(nombreSistema);
                    }
                }

                if (cmbSistemas.Items.Count > 0)
                    cmbSistemas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los sistemas: " + ex.Message);
            }
        }
        private void CargarVentanaNegocio(string filtro = "")
        {
            try
            {
                ConexionOracle conexion = new ConexionOracle(connectionString);

                string nombre = string.IsNullOrWhiteSpace(filtro) ? null : filtro;
                DataTable dt = conexion.ObtenerVentanasNegocio(nombre);
                dgvVentanas.DataSource = dt;

                if (!dgvVentanas.Columns.Contains("btnEditar"))
                    AgregarBoton("btnEditar", "Editar");

                if (!dgvVentanas.Columns.Contains("btnEliminar"))
                    AgregarBoton("btnEliminar", "Eliminar");
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
            var ventanaRoles = new VentanaRoleManagement(usuarioActual);
            ventanaRoles.Owner = this; 
            ventanaRoles.Show();

        }

     


        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columna = dgvVentanas.Columns[e.ColumnIndex].Name;
            int idventana = int.Parse(dgvVentanas.Rows[e.RowIndex].Cells["idventana"].Value.ToString());


            if (columna == "btnEditar")
            {
                ModificarVentana modificarVentana = new ModificarVentana(idventana, usuarioActual);
                modificarVentana.Owner = this; 
                modificarVentana.Show();
            }
            else if (columna == "btnEliminar")
            {
                DialogResult result = MessageBox.Show($"¿Deseas eliminar la ventana?", "Confirmar eliminación",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                   _conexion.EliminarVentana(idventana, usuarioActual);
                    MessageBox.Show("Ventana eliminado correctamente.");
                    CargarVentana();
                }
            }


        }


        private void AgregarUsuario_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            var crearVentana = new CrearVentana(usuarioActual);
            crearVentana.Owner = this; 
            crearVentana.Show();
        }

         //--------------------------
        private void AgregarBoton(string nombre, string texto)
        {
            // Verificar si el botón ya existe antes de agregarlo  
            if (!dgvVentanas.Columns.Contains(nombre))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = nombre,
                    HeaderText = texto,
                    Text = texto,
                    UseColumnTextForButtonValue = true,

                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                dgvVentanas.Columns.Add(btn);
            }
        }



        private void cmbSistemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sistemaSeleccionado = cmbSistemas.SelectedItem?.ToString();
            CargarVentanaNegocio(sistemaSeleccionado);
        }

        private void btnUserManagment_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            VentanaAdministrador ventanaAdministrador = new VentanaAdministrador(usuarioActual);
            ventanaAdministrador.Owner = this; 
            ventanaAdministrador.Show();

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
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

        //-----------------------------------------------------------




    }

}
