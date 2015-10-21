using CommandContracts.Common;
using System;
using System.Collections.Generic;
namespace CommandContracts.Siport.HojaRuta
{
    public class InsertarPlanificacionCommand : Command
    {
        public double? IdPlanHojadeRutaCab { get; set; }
        public string CodigoPlanificacion { get; set; }
        public string ComentarioPlanificacion { get; set; }
        public List<InsertarPlanificacionDetCommand> ListaPlanHojaRuta { get; set; }
    }

    public class InsertarPlanificacionDetCommand
    {
        public double? IdPlanHojadeRutaDet { get; set; }
        public double? IdPlanHojadeRutaCab { get; set; }
        public double? IdVehiculo { get; set; }
        public double? IdOrdenServicio { get; set; }
        public double? IdOrdenServicioDestino { get; set; }
        public int OrdenAtencion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Estado { get; set; }
    }
}
