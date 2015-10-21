
using System;
namespace Web.Siport.Models.HojaRuta
{
    public class PlanificacionRutaDetModel
    {
        public double? IdPlanHojaDeRutaDet { get; set; }
        public double? IdVehiculo { get; set; }
        public double? IdOrdenServicio { get; set; }
        public double? IdOrdenServicioDestino { get; set; }
        public int OrdenAtencion { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get;  set; }

        public string PlacaVehiculo { get; set; }
        public string CodigoOrdServicio { get; set; }
        public string Direccion { get; set; }
        public string DesHorarioEntrega { get; set; }
    }
}