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
    public partial class VentanaPermisosUsuario : Form
    {
        private ConexionOracle conexion;
        private string nombreUsuarioSeleccionado = null;
        private string sistemaSeleccionado = null;
        private string usuarioActual;

        public VentanaPermisosUsuario(string usuarioActual, string nombreUsuario, string sistema)
        {
            InitializeComponent();
            this.usuarioActual = usuarioActual;
            nombreUsuarioSeleccionado = nombreUsuario;
            sistemaSeleccionado = sistema;
            this.Load += new EventHandler(VentanaPermisosUsuario_Load);
            dgvPermiso.ReadOnly = true;
        }

        private void VentanaPermisosUsuario_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            conexion = new ConexionOracle(connectionString);

            txtUser.Text = "User: " + nombreUsuarioSeleccionado;

            CargarPermisosVentanaUsuario(nombreUsuarioSeleccionado, sistemaSeleccionado);

        }
       
        private void CargarPermisosVentanaUsuario(string nombreUsuario, string sistema)
        {
            var dt = conexion.MostrarVentanasUsuario(nombreUsuario, sistema);
            dgvPermiso.DataSource = dt;

            LimpiarColumnasCRUD();
            AgregarBotonSiNoExiste("btnCreate", "Permite Crear");
            AgregarBotonSiNoExiste("btnRead", "Permite Leer");
            AgregarBotonSiNoExiste("btnUpdate", "Permite Editar");
            AgregarBotonSiNoExiste("btnDelete", "Permite Eliminar");

            foreach (DataGridViewRow row in dgvPermiso.Rows)
            {
                PintarBoton(row, "btnCreate", Convert.ToInt32(row.Cells["CREATE"].Value));
                PintarBoton(row, "btnRead", Convert.ToInt32(row.Cells["READ"].Value));
                PintarBoton(row, "btnUpdate", Convert.ToInt32(row.Cells["UPDATE"].Value));
                PintarBoton(row, "btnDelete", Convert.ToInt32(row.Cells["DELETE"].Value));
            }

            dgvPermiso.Columns["IDVENTANA"].Visible = false;
            dgvPermiso.Columns["CREATE"].Visible = false;
            dgvPermiso.Columns["READ"].Visible = false;
            dgvPermiso.Columns["UPDATE"].Visible = false;
            dgvPermiso.Columns["DELETE"].Visible = false;
        }



        private void AgregarBotonSiNoExiste(string nombre, string texto)
        {
            if (!dgvPermiso.Columns.Contains(nombre))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    Name = nombre,
                    HeaderText = texto,
                    Text = texto,
                    UseColumnTextForButtonValue = true
                };
                dgvPermiso.Columns.Add(btn);
            }
        }

        private void PintarBoton(DataGridViewRow row, string columna, int activo)
        {
            var cell = row.Cells[columna];
            cell.Style.BackColor = activo == 1 ? Color.LightGreen : Color.IndianRed;
        }

        private void LimpiarColumnasCRUD()
        {
            var columnasCRUD = new[] { "btnCreate", "btnRead", "btnUpdate", "btnDelete" };
            foreach (var nombre in columnasCRUD)
            {
                if (dgvPermiso.Columns.Contains(nombre))
                    dgvPermiso.Columns.Remove(nombre);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
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
            var ventanaRoleUser = new VentanaRoleUser(usuarioActual, nombreUsuarioSeleccionado, sistemaSeleccionado);
            ventanaRoleUser.Owner = this;
            ventanaRoleUser.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            var ventanaPermisos = new VentanaPermisosUsuario(usuarioActual, nombreUsuarioSeleccionado, sistemaSeleccionado);
            ventanaPermisos.Owner = this;
            ventanaPermisos.Show();
        }

        private void dgvPermiso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (string.IsNullOrEmpty(nombreUsuarioSeleccionado) || e.RowIndex < 0) return;

            string col = dgvPermiso.Columns[e.ColumnIndex].Name;
            int idVentana = Convert.ToInt32(dgvPermiso.Rows[e.RowIndex].Cells["IDVENTANA"].Value);

            int permisoId = 0;

            switch (col)
            {
                case "btnCreate":
                    permisoId = 1;
                    break;
                case "btnRead":
                    permisoId = 2;
                    break;
                case "btnUpdate":
                    permisoId = 3;
                    break;
                case "btnDelete":
                    permisoId = 4;
                    break;
                default:
                    permisoId = 0;
                    break;
            }


            if (permisoId == 0) return;

            bool tienePermiso = dgvPermiso.Rows[e.RowIndex].Cells[col].Style.BackColor == Color.LightGreen;

            if (tienePermiso)
            {
                conexion.EliminarPermisoUsuario(nombreUsuarioSeleccionado, idVentana, permisoId);


                if (permisoId == 2)
                {
                    conexion.EliminarPermisoUsuario(nombreUsuarioSeleccionado, idVentana, 1);

                    conexion.EliminarPermisoUsuario(nombreUsuarioSeleccionado, idVentana, 3);

                    conexion.EliminarPermisoUsuario(nombreUsuarioSeleccionado, idVentana, 4);

                }
            }
            else
            {
                // Si no tenía el permiso, lo asignamos
                conexion.AsignarPermisoUsuario(nombreUsuarioSeleccionado, idVentana, permisoId);
                
                // Activar también el permiso de lectura si no está activo
                if (permisoId != 2) // si no es el permiso de lectura
                {
                    bool tieneLectura = dgvPermiso.Rows[e.RowIndex].Cells["btnRead"].Style.BackColor == Color.LightGreen;
                    if (!tieneLectura)
                    {
                        conexion.AsignarPermisoUsuario(nombreUsuarioSeleccionado, idVentana, 2);
                    }
                }
            }


            CargarPermisosVentanaUsuario(nombreUsuarioSeleccionado, sistemaSeleccionado);

        }

        private void txtUser_Click(object sender, EventArgs e)
        {

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
