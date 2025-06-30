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
    public partial class VentanaEditarUsuario : Form
    {
        private ConexionOracle _conexion = null;
        private string nombreUsuarioModificado;
        private string nombreSistema;

        public VentanaEditarUsuario(string nombreUsuario, string nombresistema)
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            _conexion = new ConexionOracle(connectionString);
            nombreUsuarioModificado = nombreUsuario;
            nombreSistema = nombresistema;
            CargarSistemasAsignados(nombreUsuarioModificado);
           
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
                
                // Antes de enviar la contraseña al procedimiento, encríptala:
                string nuevaPassword = txtPassword.Text.Trim();
                string nuevoEstado = cmbEstado.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(nuevaPassword) || string.IsNullOrEmpty(nuevoEstado))
                {
                    MessageBox.Show("Debe completar todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hashear la nueva clave ANTES de enviarla a Oracle
                string hashPassword = BCrypt.Net.BCrypt.HashPassword(nuevaPassword);

                // Llama al procedimiento con la clave encriptada
                string resultado = _conexion.ActualizarUsuario(hashPassword, nuevoEstado, nombreUsuarioModificado);

                // Comparar checkboxes
                foreach (DataGridViewRow fila in dtgSistema.Rows)
                {
                    if (fila.IsNewRow) continue;

                    string sistema = fila.Cells["NOMBRESISTEMA"].Value?.ToString();

                    bool estabaAsignado = fila.Cells["ASIGNADAS"].Value != DBNull.Value && Convert.ToBoolean(fila.Cells["ASIGNADAS"].Value);
                    bool deseaAsignar = fila.Cells["NUEVOS"].Value != DBNull.Value && Convert.ToBoolean(fila.Cells["NUEVOS"].Value);

                    if (deseaAsignar && !estabaAsignado)
                    {
                        _conexion.InsertarSistemaUsuario(sistema, nombreUsuarioModificado);
                    }
                    else if (!deseaAsignar && estabaAsignado)
                    {
                        _conexion.EliminarSistemaUsuario(sistema, nombreUsuarioModificado);
                    }
                }

                MessageBox.Show(resultado + "\nSistemas actualizados correctamente.", "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Clear();
                cmbEstado.SelectedIndex = -1;
                CargarSistemasAsignados(nombreUsuarioModificado); // refrescar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarSistemasAsignados(string nombreUsuario)
        {
            try
            {
                DataTable dt = _conexion.MostrarSistemasAsignadosUsuario(nombreUsuario);

                // Asegurarse de tener una columna booleana real para "ASIGNADAS"
                if (dt.Columns.Contains("ASIGNADO") && dt.Columns["ASIGNADO"].DataType != typeof(bool))
                {
                    dt.Columns.Add("ASIGNADAS", typeof(bool));
                    foreach (DataRow row in dt.Rows)
                    {
                        int valor = Convert.ToInt32(row["ASIGNADO"]);
                        row["ASIGNADAS"] = (valor == 1);
                    }
                    dt.Columns.Remove("ASIGNADO");
                }
                else if (!dt.Columns.Contains("ASIGNADAS"))
                {
                    dt.Columns.Add("ASIGNADAS", typeof(bool));
                }

                // Agregar columna para nuevos checkboxes
                if (!dt.Columns.Contains("NUEVOS"))
                {
                    dt.Columns.Add("NUEVOS", typeof(bool));
                    foreach (DataRow row in dt.Rows)
                    {
                        row["NUEVOS"] = row["ASIGNADAS"]; // inicializa igual que el estado actual
                    }
                }

                dtgSistema.DataSource = null;
                dtgSistema.Columns.Clear();
                dtgSistema.DataSource = dt;

                // Mostrar columnas
                dtgSistema.Columns["ASIGNADAS"].HeaderText = "Asignadas";
                dtgSistema.Columns["ASIGNADAS"].ReadOnly = true;

                dtgSistema.Columns["NUEVOS"].HeaderText = "Nuevos";
                dtgSistema.Columns["NUEVOS"].ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar sistemas del usuario: " + ex.Message);
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
