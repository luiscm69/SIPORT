namespace Web.Common.AuthenticationServices
{
    using System.Configuration;
    using System.Web;
    using ServiceAgents.Common;
    using global::Seguridad.Common;
    using QueryContracts.Common;
    using System;
    using System.Linq;

    using System.Collections.Generic;
    using QueryContracts.Common.Seguridad.Results;
    using QueryContracts.Common.Seguridad.Parameters;

    public class UpdatePerfilService
    {
        public void Update(string codigousuario, double? idperfil)
        {
            var datosCnxParameters = new ObtenerDatosConexionParameter
            {
                IdPerfil = idperfil,
                CodigoUsuario = codigousuario
            };
            var resultadoDatosCnx = ((ObtenerDatosConexionResult)datosCnxParameters.Execute());

            if (resultadoDatosCnx != null){
                this.UpdateConnecctionData(resultadoDatosCnx.Usuario, resultadoDatosCnx.Clave);
                this.UpdatePerfil(codigousuario, resultadoDatosCnx.IdPerfil, resultadoDatosCnx.NombrePerfil);
            }
        }

        private void UpdatePerfil(string CodigoUsuario, Int64 IdPerfil, String NombrePerfil)
        {
            SessionWrapper.Perfil = new Perfil
            {
                IdPerfil = IdPerfil,
                NombrePerfil = NombrePerfil,
            };

        }


        private void UpdateConnecctionData(string user, string dbPassword)
        {
            var session = HttpContext.Current.Session;
            session["dbUser"] = user;
            session["dbPassword"] = dbPassword;
        }
    }
}