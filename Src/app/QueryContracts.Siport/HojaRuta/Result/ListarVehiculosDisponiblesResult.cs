using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Siport.HojaRuta.Result
{
    public class ListarVehiculosDisponiblesResult : QueryResult
    {
        public IEnumerable<ListarVehiculosDisponiblesDto> Hits { get; set; }
    }

    public class ListarVehiculosDisponiblesDto 
    {
        public double IdVehiculo { get; set; }
        public string EstadoVehiculo { get; set; }
        public string NumeroPlaca { get; set; }
        public DateTime FechaEstEntrega { get; set; }
    }
}
