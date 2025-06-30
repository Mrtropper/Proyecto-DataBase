using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cosmetico
    {
        public int IDCosmetico { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public double PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Categoria { get; set; }
        public string EstadoProducto { get; set; }
        public string Imagen { get; set; }

        public Cosmetico(int IDCosmetico, string Nombre, string Marca, double PrecioUnitario, int StockDisponible,
            DateTime FechaVencimiento, string Categoria, string EstadoProducto, string Imagen)
        {
            this.IDCosmetico = IDCosmetico;
            this.Nombre = Nombre;
            this.Marca = Marca;
            this.PrecioUnitario = PrecioUnitario;
            this.FechaVencimiento = FechaVencimiento;
            this.StockDisponible = StockDisponible;
            this.Categoria = Categoria;
            this.EstadoProducto = EstadoProducto;
            this.Imagen = Imagen;

        }

        public Cosmetico()
        {

        }

    }
}
