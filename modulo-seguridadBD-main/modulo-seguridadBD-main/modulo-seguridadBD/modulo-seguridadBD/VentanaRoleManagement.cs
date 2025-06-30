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
    public partial class VentanaRoleManagement : Form
    {
        private ConexionOracle conexion;
        private int rolSeleccionado = -1;
        private string sistemaSeleccionado;
        private string usuarioActual;
        private string nombreUsuarioSeleccionado = null;
        private ConexionOracle _conexion = null;




        public VentanaRoleManagement(String usuarioActual)
        {
            InitializeComponent();
            this.usuarioActual = usuarioActual;
            this.Load += new System.EventHandler(this.VentanaRoleManagement_Load);
            dgvRole.ReadOnly = true;
            dgvVentana.ReadOnly = true;

        }

        private void VentanaRoleManagement_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            conexion = new ConexionOracle(connectionString);
            cmbSistemas.SelectedIndexChanged += cmbSistemas_SelectedIndexChanged;



            CargarSistemas();

            if (cmbSistemas.Items.Count > 0)
            {
                sistemaSeleccionado = cmbSistemas.SelectedItem.ToString();
                CargarRoles();
            }
        }

       
        private void CargarRoles()
        {
            if (string.IsNullOrEmpty(sistemaSeleccionado))
            {
                MessageBox.Show("Seleccione un sistema antes de cargar roles.");
                return;
            }

            string filtro = txtSearch.Text.Trim();
            dgvRole.DataSource = conexion.MostrarRoles(filtro, sistemaSeleccionado);

            if (!dgvRole.Columns.Contains("btnEditar"))
                AgregarBoton("btnEditar", "Editar");

            if (!dgvRole.Columns.Contains("btnEliminar"))
                AgregarBoton("btnEliminar", "Eliminar");
        }
        private void CargarSistemas()
        {
            cmbSistemas.Items.Clear();

            // Agregar manualmente los nombres conocidos
            cmbSistemas.Items.Add("negocio1");
            cmbSistemas.Items.Add("negocio2");

            if (cmbSistemas.Items.Count > 0)
                cmbSistemas.SelectedIndex = 0;  
        }


        private void AgregarBoton(string nombre, string texto)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn
            {
                Name = nombre,
                HeaderText = texto,
                Text = texto,
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dgvRole.Columns.Add(btn);
        }




        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUserManagment_Click(object sender, EventArgs e)
        {

            this.Hide(); // Oculta la ventana actual en lugar de Dispose() para evitar excepciones.  
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
            var ventanaRoleUser = new VentanaRoleUser(usuarioActual, nombreUsuarioSeleccionado, sistemaSeleccionado);
            ventanaRoleUser.Owner = this;
            ventanaRoleUser.Show();
        }


        private void dgvRole_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {    
            if (e.RowIndex < 0) return;

            string columna = dgvRole.Columns[e.ColumnIndex].Name;


            if (columna == "btnEditar")
            {
                int idRol = Convert.ToInt32(dgvRole.Rows[e.RowIndex].Cells["IDROL"].Value);

                this.Hide();

                var ventanaEditarRol = new VentanaEditarRol(idRol, sistemaSeleccionado);
                ventanaEditarRol.FormClosed += (s, args) =>
                {
                    this.Show();     
                    CargarRoles();     
                };

                ventanaEditarRol.ShowDialog();
            }

            else if (columna == "btnEliminar")
            {
                
                DialogResult result = MessageBox.Show($"¿Deseas eliminar el rol '{lbRolSelec.Text}'?", "Confirmar eliminación",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    conexion.EliminarRol(rolSeleccionado, usuarioActual);
                    MessageBox.Show("Rol eliminado correctamente.");
                    CargarRoles();
                }
            }
        
        }


        private void dgvRole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //no puede estar vacio
            if (e.RowIndex < 0) return;

            int idRol = Convert.ToInt32(dgvRole.Rows[e.RowIndex].Cells["IDROL"].Value);
            string nombreRol = dgvRole.Rows[e.RowIndex].Cells["NOMBREROL"].Value.ToString();

            rolSeleccionado = idRol;
            lbRolSelec.Text = nombreRol;
            CargarPermisosVentana(rolSeleccionado);
        }


        private void CargarPermisosVentana(int idRol)
        {   
            var dt = conexion.MostrarVentanasConPermisos(idRol, sistemaSeleccionado); // ahora incluye sistema
            dgvVentana.DataSource = dt;

            LimpiarColumnasExtra();
            AgregarBotonSiNoExiste("btnCreate", "Permite Crear");
            AgregarBotonSiNoExiste("btnRead", "Permite Leer");
            AgregarBotonSiNoExiste("btnUpdate", "Permite Editar");
            AgregarBotonSiNoExiste("btnDelete", "Permite Eliminar");

            foreach (DataGridViewRow row in dgvVentana.Rows)
            {
                PintarBoton(row, "btnCreate", Convert.ToInt32(row.Cells["CREATE"].Value));
                PintarBoton(row, "btnRead", Convert.ToInt32(row.Cells["READ"].Value));
                PintarBoton(row, "btnUpdate", Convert.ToInt32(row.Cells["UPDATE"].Value));
                PintarBoton(row, "btnDelete", Convert.ToInt32(row.Cells["DELETE"].Value));
            }

            dgvVentana.Columns["IDVENTANA"].Visible = false;
            dgvVentana.Columns["CREATE"].Visible = false;
            dgvVentana.Columns["READ"].Visible = false;
            dgvVentana.Columns["UPDATE"].Visible = false;
            dgvVentana.Columns["DELETE"].Visible = false;
        }

        private void AgregarBotonSiNoExiste(string nombre, string texto)
        {
            if (!dgvVentana.Columns.Contains(nombre))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = nombre,
                    HeaderText = texto,
                    Text = texto,
                    UseColumnTextForButtonValue = true
                };
                dgvVentana.Columns.Add(btn);
            }
        }

        private void PintarBoton(DataGridViewRow row, string columna, int activo)
        {
            var cell = row.Cells[columna];
            cell.Style.BackColor = activo == 1 ? Color.LightGreen : Color.IndianRed;
        }
        private void LimpiarColumnasExtra()
        {
            var columnasCRUD = new[] { "btnCreate", "btnRead", "btnUpdate", "btnDelete" };
            foreach (var nombre in columnasCRUD)
            {
                if (dgvVentana.Columns.Contains(nombre))
                    dgvVentana.Columns.Remove(nombre);
            }
        }



        private void dgvVentana_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rolSeleccionado == -1 || e.RowIndex < 0) return;

            string col = dgvVentana.Columns[e.ColumnIndex].Name;
            int idVentana = Convert.ToInt32(dgvVentana.Rows[e.RowIndex].Cells["IDVENTANA"].Value);

            int permisoId = 0;
            switch (col)
            {
                case "btnCreate": permisoId = 1; break;
                case "btnRead": permisoId = 2; break;
                case "btnUpdate": permisoId = 3; break;
                case "btnDelete": permisoId = 4; break;
            }


            if (permisoId == 0) return;

            bool tienePermiso = dgvVentana.Rows[e.RowIndex].Cells[col].Style.BackColor == Color.LightGreen;

            if (tienePermiso)
            {
                conexion.EliminarPermiso(rolSeleccionado, idVentana, permisoId);
                if (permisoId == 2)
                {
                    conexion.EliminarPermiso(rolSeleccionado, idVentana, 1); 
                    conexion.EliminarPermiso(rolSeleccionado, idVentana, 3); 
                    conexion.EliminarPermiso(rolSeleccionado, idVentana, 4); 
                }
            }
            else
            {
                
                conexion.AsignarPermiso(rolSeleccionado, idVentana, permisoId);

                // Activar también el permiso de lectura si no está activo
                if (permisoId != 2) // si no es leer
                {
                    bool tieneLectura = dgvVentana.Rows[e.RowIndex].Cells["btnRead"].Style.BackColor == Color.LightGreen;
                    if (!tieneLectura)
                    {
                        conexion.AsignarPermiso(rolSeleccionado, idVentana, 2);
                    }
                }
            }


            // Recargar para reflejar cambios
            CargarPermisosVentana(rolSeleccionado);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            CargarRoles();
        }

        private void txtRoleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSistemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void cmbSistemas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            sistemaSeleccionado = cmbSistemas.SelectedItem.ToString();
            CargarRoles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ventanaAgregarRol = new VentanaAgregarRol())
            {
                ventanaAgregarRol.ShowDialog();
            }
            CargarRoles(); // Refrescar la lista de roles después de agregar uno nuevo  
        }

        private void btnLogin_Click(object sender, EventArgs e)
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
