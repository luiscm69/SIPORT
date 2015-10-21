using QueryContracts.Siport.Cliente.Parameter;
using QueryContracts.Siport.Cliente.Result;
using ServiceAgents.Common;
using System;

namespace Web.Siport.DataAccess
{
    public static class DACliente 
    {
        public static ObtenerClienteResult ObtenerCliente(string pCodigoCliente)
        {
            try
            {
                var parameter = new ObtenerClienteParameter();
                parameter.CodigoCliente = pCodigoCliente;
                var resultado = (ObtenerClienteResult)parameter.Execute();
                return resultado;
            }
            catch (Exception ex)
            {
                //DataAccessBase.SetLogError(ex);
                return null;
            }
            
        }
    }
}