using QueryContracts.Common;
using System;
namespace QueryContracts.Siport.HojaRuta.Parameter
{
    public class ListarVehiculosDisponiblesParameter : QueryParameter
    {
        //public string Estado { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
