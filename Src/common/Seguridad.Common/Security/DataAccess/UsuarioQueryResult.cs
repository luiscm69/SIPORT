

using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using ServiceAgents.Common;

namespace Seguridad.Common.Security.DataAccess
{
    public static class UsuarioQueryResult
    {
        public static ValidarCredencialesUsuarioResult ValidarUsuario(string codigousuario, string password)
        {
            var parametro = new ValidarCredencialesUsuarioParameter();
            parametro.CodigoUsuario = codigousuario;
            parametro.PasswordUsuario = password;
            var resultado = (ValidarCredencialesUsuarioResult)parametro.Execute();
            return resultado;
        }

        public static ObtenerDatosUsuarioResult ObtenerDataUsuario(string codigousuario)
        {
            var parametro =new  ObtenerDatosUsuarioParameter();
            parametro.CodigoUsuario = codigousuario;
            var resultado = (ObtenerDatosUsuarioResult)parametro.Execute();
            return resultado;
        }
    }
}
