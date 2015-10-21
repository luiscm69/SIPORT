
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Siport.OrderServicio.Result
{
    public class ObtenerOrdenServicioResult : QueryResult
    {
        public double? IdOrdenServicio { get; set; }
        public string CodigoOrdServicio { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime LogFecha { get; set; }
        public string LogUsuario { get; set; }
        public double IdClientes { get; set; }
        public string NombreRazonSocial { get; set; }
        public IEnumerable<OrdenServicioDestinoDto> ListadoOrdenServicioDestino { get; set; }

    }

    public class OrdenServicioDestinoDto
    {
        public double? IdOrdenServicio { get; set; }
        public double? IdOrdenServicioDestino { get; set; }
        public string CodigoUbigeo { get; set; }

        public string DescripcionUbigeo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? LogFecha { get; set; }
        public string LogUsuario { get; set; }
        public DateTime? FechaEstEntrega { get; set; }
        public DateTime? FechaRealDespacho { get; set; }
        public double IdHorarioEntrega { get; set; }

        public string Direccion { get; set; }

        public string Referencia { get; set; }
        public string Estado { get; set; }
        public string DesHorarioEntrega { get; set; }
        
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }


    }
}
