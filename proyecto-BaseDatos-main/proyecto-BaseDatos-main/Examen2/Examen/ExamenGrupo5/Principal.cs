using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using DAL;
using System.Configuration;

namespace ExamenGrupo5
{
    public partial class Principal : Form
    {
        private string usuarioLogeado;
        private string sistema;
        private DataTable permisosTotales;
        public Principal(string usuarioLogeado, string sistema)
        {
            InitializeComponent();
            this.usuarioLogeado = usuarioLogeado; // Asigna el usuario logueado 
            this.sistema = sistema; // Asigna el sistema
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            string cadena = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            var conexion = new OracleConexionSeguridad(cadena); // ← ahora sí correcto

            permisosTotales = conexion.MostrarPermisosTotalesUsuario(usuarioLogeado, sistema);


            OcultarBotonSiNoTienePermiso(permisosTotales, "Cosmetico", btn_Cosmeticos);
            OcultarBotonSiNoTienePermiso(permisosTotales, "Compra", btn_Compras);
            OcultarBotonSiNoTienePermiso(permisosTotales, "Venta", btn_Ventas);
            OcultarBotonSiNoTienePermiso(permisosTotales, "Consumidor", Consumidores);

        }

        private PermisosVentana ObtenerPermisosPorVentana(string nombreVentana)
        {
            if (permisosTotales == null)
                return new PermisosVentana(); // Todos en false

            var fila = permisosTotales.AsEnumerable()
                            .FirstOrDefault(r => r.Field<string>("NOMBREVENTANA") == nombreVentana);

            if (fila == null)
                return new PermisosVentana(); // Todos en false

            return new PermisosVentana
            {
                PuedeCrear = fila.Field<decimal>("CREATE") == 1,
                PuedeLeer = fila.Field<decimal>("READ") == 1,
                PuedeActualizar = fila.Field<decimal>("UPDATE") == 1,
                PuedeEliminar = fila.Field<decimal>("DELETE") == 1
            };
        }
        private void OcultarBotonSiNoTienePermiso(DataTable permisos, string nombreVentana, Control boton)
        {
            var fila = permisos.AsEnumerable()
                            .FirstOrDefault(r => r.Field<string>("NOMBREVENTANA") == nombreVentana);

            if (fila == null || fila.Field<decimal>("READ") == 0)
                boton.Visible = false;
        }

        private void btn_Venta_Click(object sender, EventArgs e)
        {

            var permisos = ObtenerPermisosPorVentana("Venta");
            AbrirVentanaConOcultamiento(new VentanaVentas(permisos));
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void AbrirVentanaConOcultamiento(Form ventana)
        {
            this.Hide(); // Oculta principal
            ventana.ShowDialog(); // Muestra la ventana hija
            this.Show(); // Vuelve a mostrar principal al cerrar la hija
        }

        private void btnCosmeticosCLick(object sender, EventArgs e)
        {
            var permisos = ObtenerPermisosPorVentana("Cosmetico");
            AbrirVentanaConOcultamiento(new VentanaCosmetico(permisos));

        }

        private void SalirClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Compras_Click(object sender, EventArgs e)
        {
            var permisos = ObtenerPermisosPorVentana("Compra");
            AbrirVentanaConOcultamiento(new VentanaCompras(permisos));
        }

        private void btnConsumidores(object sender, EventArgs e)
        {
            var permisos = ObtenerPermisosPorVentana("Consumidor");
            AbrirVentanaConOcultamiento(new VentanaConsumidor(permisos));
        }
    }
}
