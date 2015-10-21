

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Siport.HojaRuta.Result
{
    public class ListarOrdenServicioDisponibleResult : QueryResult
    {
        public IEnumerable<ListarOrdenServicioDisponibleDto> Hits { get; set; }
    }

    public class ListarOrdenServicioDisponibleDto
    {
        public double IdOrdenServicioDestino { get; set; }
        public string DesOrdenServicioDestino { get; set; }
        public string Estado { get; set; }
    }
}
