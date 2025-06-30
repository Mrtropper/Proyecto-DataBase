using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }

        public double TotalVenta { get; set; }

        public string MetodoPago { get; set; }
        public int PuntosUsados { get; set; }
        public int CantidadVendido { get; set; }
        public int IDCosmetico { get; set; }
        public int IDConsumidor { get; set; }
        public string EstadoVenta { get; set; }

        public Venta(int idVenta, DateTime fechaVenta, double totalVenta, string metodoPago, int puntosUsados, int cantidadVendido,
            int iDCosmetico, int iDConsumidor, string estadoVenta)
        {
            IdVenta = idVenta;
            FechaVenta = fechaVenta;
            TotalVenta = totalVenta;
            MetodoPago = metodoPago;
            PuntosUsados = puntosUsados;
            CantidadVendido = cantidadVendido;
            IDCosmetico = iDCosmetico;
            IDConsumidor = iDConsumidor;
            EstadoVenta = estadoVenta;
        }

        public Venta()
        {
        }
    }
}
