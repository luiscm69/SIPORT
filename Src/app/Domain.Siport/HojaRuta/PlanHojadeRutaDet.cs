using Domain.Common;
using System;
namespace Domain.Siport.HojaRuta
{
    public class PlanHojadeRutaDet : Entity
    {
        public double? IdVehiculo { get; set; }
        public double? IdOrdenServicio { get; set; }
        public double? IdOrdenServicioDestino { get; set; }
        public int OrdenAtencion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Estado { get; set; }
        public PlanHojadeRutaCab EntidadHojaRuta { get; set; }
    }
}
