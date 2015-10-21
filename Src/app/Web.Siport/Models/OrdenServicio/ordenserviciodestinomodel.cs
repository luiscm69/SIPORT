using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Siport.Models.OrdenServicio
{
    public class OrdenServicioDestinoModel
    {
        public double? IdOrdenServicioDestino { get; set; }
        public string CodigoUbigeo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? LogFecha { get; set; }
        public string LogUsuario { get; set; }
        [DisplayName("Fecha de Entrega")]
        public DateTime? FechaEstEntrega { get; set; }
        public DateTime? FechaRealDespacho { get; set; }
        [DisplayName("Horario de Entrega")]
        public double IdHorarioEntrega { get; set; }
        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        [DisplayName("Referencia")]
        public string Referencia { get; set; }
        public string Estado { get; set; }
        public string DesHorarioEntrega { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

    }
}