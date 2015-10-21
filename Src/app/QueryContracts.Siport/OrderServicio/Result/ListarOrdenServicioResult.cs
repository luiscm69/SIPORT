

using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Siport.OrderServicio.Result
{
    public class ListarOrdenServicioResult : QueryResult
    {
        public IEnumerable<ListarOrdenServicioDto> Hits { get; set; }

    }

    public class ListarOrdenServicioDto
    {
        public double IdOrdenServicio { get; set; }
        public string CodigoOrdenServicio { get; set; }
        public string Descripcion { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreRazonSocial { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public int CantDestinos { get; set; }
    }
}
