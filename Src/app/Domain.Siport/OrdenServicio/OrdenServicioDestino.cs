using Domain.Common;
using System;
namespace Domain.Siport.OrdenServicio
{
    public class OrdenServicioDestino : Entity
    {
        public string CodigoUbigeo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? LogFecha { get; set; }
        public string LogUsuario { get; set; }
        public DateTime? FechaEstEntrega { get; set; }
        public DateTime? FechaRealDespacho { get; set; }
        public double IdHorarioEntrega { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string Estado { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual OrdenServicio EntidadOrdenServicio { get; set; }

    }
}
