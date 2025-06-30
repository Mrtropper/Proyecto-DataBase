using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Consumidor
    {
        public int IdConsumidor { get; set; }

        public string NombreCompleto { get; set; }
        public string telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FrecuenciaCompra { get; set; }

        public int PuntosFidelidad { get; set; }
        public string Direccion { get; set; }

        public Consumidor(int idConsumidor, string nombreCompleto, string telefono,
            string correoElectronico, DateTime fechaRegistro, string frecuenciaCompra, int puntosFidelidad, string direccion)
        {
            this.IdConsumidor = idConsumidor;
            NombreCompleto = nombreCompleto;
            this.telefono = telefono;
            CorreoElectronico = correoElectronico;
            FechaRegistro = fechaRegistro;
            FrecuenciaCompra = frecuenciaCompra;
            PuntosFidelidad = puntosFidelidad;
            Direccion = direccion;
        }
        public Consumidor(string nombreCompleto, string telefono,
          string correoElectronico, DateTime fechaRegistro, string frecuenciaCompra, int puntosFidelidad, string direccion)
        {
            NombreCompleto = nombreCompleto;
            telefono = telefono;
            CorreoElectronico = correoElectronico;
            FechaRegistro = fechaRegistro;
            FrecuenciaCompra = frecuenciaCompra;
            PuntosFidelidad = puntosFidelidad;
            Direccion = direccion;
        }

        public Consumidor()
        {
        }
    }
}
