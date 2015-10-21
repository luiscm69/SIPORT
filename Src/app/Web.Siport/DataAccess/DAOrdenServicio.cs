
using QueryContracts.Siport.OrderServicio.Parameter;
using QueryContracts.Siport.OrderServicio.Result;
using ServiceAgents.Common;
using System;

namespace Web.Siport.DataAccess
{
    public static class DAOrdenServicio
    {
        public static ObtenerOrdenServicioResult GetOrdenServicio(string codigoordsrv)
        {
            var parameter = new ObtenerOrdenServicioParameter();
            parameter.CodigoOrdenServicio = codigoordsrv;
            var resultado = (ObtenerOrdenServicioResult)parameter.Execute();
            return resultado;
        }

        public static ListarHorarioEntregaResult GetListarHorarioEntrega()
        {
            var parameter = new ListarHorarioEntregaParameter();
            var resultado = (ListarHorarioEntregaResult)parameter.Execute();
            return resultado;
        }

        public static ListarOrdenServicioResult GetListarOrdenServicio()
        {
            var parameter = new ListarOrdenServicioParameter();
            var resultado = (ListarOrdenServicioResult)parameter.Execute();
            return resultado;
        }

        public static ObtenerOrdenServicioDestinoResult GetOrdenServicioDestino(double pIdOrdenServicioDestino)
        {
            try
            {
                var parameter = new ObtenerOrdenServicioDestinoParameter();
                parameter.IdOrdenServicioDestino = pIdOrdenServicioDestino;
                var resultado = (ObtenerOrdenServicioDestinoResult)parameter.Execute();
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}