using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Siport
{
    using System;
    using System.Web;

    using Web.Common.HttpApplications;
    using Web.Siport.Security;

    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication :  CommonHttpApplication  
    {
        protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session == null) return;
            if (!this.Request.IsAuthenticated) return;
            var identity = new CustomIdentity(HttpContext.Current.User.Identity);
            var principal = new CustomPrincipal(identity);
            HttpContext.Current.User = principal;
        }
    }
}