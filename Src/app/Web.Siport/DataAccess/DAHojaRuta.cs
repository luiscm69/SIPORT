using QueryContracts.Siport.HojaRuta.Parameter;
using QueryContracts.Siport.HojaRuta.Result;
using System;
using ServiceAgents.Common;
using System.Collections.Generic;
using System.Linq;

namespace Web.Siport.DataAccess
{
    public static class DAHojaRuta
    {
        public static IList<ListarOrdenServicioDisponibleDto> GetListarOrdenServicioDisponible(DateTime? pFechaCreacion)
        {
            try
            {
                var parameter = new ListarOrdenServicioDisponibleParameter();
                parameter.FechaCreacion = pFechaCreacion;
                var resultado = (ListarOrdenServicioDisponibleResult)parameter.Execute();
                if(resultado == null) return new List<ListarOrdenServicioDisponibleDto>();
                return resultado.Hits.ToList();
            }
            catch (Exception ex)
            {
                //DataAccessBase.SetLogError(ex);
                return null;
            }
        }

        public static IList<ListarVehiculosDisponiblesDto> GetListarVehiculosDisponibles(DateTime pFechaEntrega)
        {
            try
            {
                var parameter = new ListarVehiculosDisponiblesParameter();
                parameter.FechaEntrega = pFechaEntrega;
                //parameter.Estado = pEstado;
                var resultado = (ListarVehiculosDisponiblesResult)parameter.Execute();
                if(resultado == null) return new List<ListarVehiculosDisponiblesDto>();
                return resultado.Hits.ToList();  
            }
            catch (Exception ex)
            {
                //DataAccessBase.SetLogError(ex);
                return null;
            }
        }



        
    }
}