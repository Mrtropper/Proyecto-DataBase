using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Compra
    {
        public int IDCompra;
        public DateTime FechaCompra;
        public double TotalCompra;
        public string MetodoPago;
        public string Proveedor;
        public int CantidadProductos;
        public string EstadoCompra;
        public int IDCosmeticos;


        public Compra(int iDCompra, DateTime fechaCompra, double totalCompra, string metodoPago, int cantidadProductos, string estadoCompra, int iDCosmeticos, string proveedor)
        {
            this.IDCompra = iDCompra;
            this.FechaCompra = fechaCompra;
            this.TotalCompra = totalCompra;
            this.MetodoPago = metodoPago;
            this.CantidadProductos = cantidadProductos;
            this.EstadoCompra = estadoCompra;
            this.IDCosmeticos = iDCosmeticos;
            Proveedor = proveedor;
        }

        public Compra(DateTime fechaCompra, double totalCompra, string metodoPago, int cantidadProductos, string estadoCompra, int iDCosmeticos, string proveedor)
        {

            this.FechaCompra = fechaCompra;
            this.TotalCompra = totalCompra;
            this.MetodoPago = metodoPago;
            this.CantidadProductos = cantidadProductos;
            this.EstadoCompra = estadoCompra;
            this.IDCosmeticos = iDCosmeticos;
            Proveedor = proveedor;
        }

        public Compra()
        {

        }
    }
}
