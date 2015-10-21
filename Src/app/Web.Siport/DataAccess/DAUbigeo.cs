using QueryContracts.Siport.Ubigeo.Parameter;
using QueryContracts.Siport.Ubigeo.Result;
using ServiceAgents.Common;

namespace Web.Siport.DataAccess
{
    public static class DAUbigeo
    {
        public static ListarUbigeoResult ListarUbigeo(string ptipoubigeo, string pcodigoubigeo)
        {
            var parameter = new ListarUbigeoParameter();
            parameter.TipoUbigeo = ptipoubigeo;
            parameter.CodigoUbigeo = pcodigoubigeo;
            var resultado = (ListarUbigeoResult)parameter.Execute();
            return resultado;

        }

        public static ListarUbigeoResult ListarUbigeo()
        {
            return ListarUbigeo("D", null);
        }

        
    }
}