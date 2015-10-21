namespace Infraestructure.Common.Logging.DataProviders
{
    using System;
    using System.Web;

    public class HttpContextUserNameProvider 
    {
        public override string ToString()
        {
            var ctx = HttpContext.Current;
            if (ctx == null) return String.Empty;
            if (ctx.User == null || ctx.User.Identity == null || ctx.User.Identity.IsAuthenticated == false)
                return "Anonimo [" + ctx.Request.UserHostAddress + "]";
            else
                return ctx.User.Identity.Name;
        }
    }
}
