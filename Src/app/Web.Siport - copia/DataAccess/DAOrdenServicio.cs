
using QueryContracts.Siport.OrderServicio.Parameter;
using QueryContracts.Siport.OrderServicio.Result;
using ServiceAgents.Common;

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
    }
}