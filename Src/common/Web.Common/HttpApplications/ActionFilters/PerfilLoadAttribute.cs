namespace Web.Common.HttpApplications.ActionFilters
{
    using global::Seguridad.Common;
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Web.Common.AuthenticationServices;

    public class PerfilLoadAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionWrapper.Perfil == null)
            {
                SessionWrapper.PrimeraConexion = true;
                var codigousuario = filterContext.HttpContext.User.Identity.Name;

                var perfilService = new UpdatePerfilService();
                if (String.IsNullOrEmpty(codigousuario)){
                    perfilService.Update(null, null);
                }

            }
        }
    }
}