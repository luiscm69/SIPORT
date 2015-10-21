namespace Web.Common.HttpApplications.ActionFilters
{
    using System.Web;

    using log4net;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Security;
    using System;

    public class SessionExpiredAttribute : ActionFilterAttribute
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ctx = HttpContext.Current;

            if (ctx.Session != null)
            {

                if (ctx.Session.IsNewSession || ctx.Session["dbUser"] == null)
                {
                    var sessionCookie = ctx.Request.Headers["Cookie"];
                    if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0) && ctx.Session["CreateSession"] == null)
                    {
                        string redirectOnSuccess = filterContext.HttpContext.Request.Url.PathAndQuery;
                        string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
                        string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
                        if (ctx.Request.IsAuthenticated)
                        {
                            FormsAuthentication.SignOut();
                        }
                        ctx.Session["CreateSession"] = DateTime.Now;
                        RedirectResult rr = new RedirectResult(loginUrl);
                        filterContext.Result = rr;

                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}