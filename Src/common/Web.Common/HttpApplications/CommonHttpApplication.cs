namespace Web.Common.HttpApplications
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Reflection;

    using log4net;
    using System.Web;

    using Web.Common.HttpApplications.AppConfig;
    using Bootstrapper.Web.Common;
    using System;
    using System.Net;
    using StructureMap;
    using System.Collections.Generic;
    using System.Web.Optimization;

    public class CommonHttpApplication : HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected virtual void Application_Start()
        {
            LoginProviderConfig.RegisterLoggingProviders();
            WebStartUp.Init();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
            Exception exception = this.Server.GetLastError().GetBaseException();
            HttpException httpException = exception as HttpException;

            if (httpException == null)
            {
                Log.Error(exception.Message, exception);
            }
            else
            {
                int statusCode = httpException.GetHttpCode();

                if (statusCode != (int)HttpStatusCode.NotFound && statusCode != (int)HttpStatusCode.ServiceUnavailable)
                {
                    Log.Error(exception.Message, httpException);
                }
            }
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            //Se Habilita para evitar error en las URLs q no contienen el "/" al final
            if (String.Compare(Request.Path, Request.ApplicationPath, StringComparison.InvariantCultureIgnoreCase) == 0
                && !(Request.Path.EndsWith("/")))
                Response.Redirect(Request.Path + "/");
        }

        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
            var context = (sender as HttpApplication).Context;

            if (context.Response.StatusCode == 401)
            {
                var noRedirect = context.Items["NoRedirect"];

                if (noRedirect == null)
                {
                    var route = new RouteValueDictionary(new Dictionary<string, object>
                        {
                            { "Controller", "Account" },
                            { "Action", "SignIn" },
                            { "ReturnUrl", HttpUtility.UrlEncode(context.Request.RawUrl, context.Request.ContentEncoding) }
                        });

                    Response.RedirectToRoute(route);
                }
            }
        }

        protected virtual void Application_End()
        {

            HttpRuntime runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime",
                BindingFlags.NonPublic
                | BindingFlags.Static
                | BindingFlags.GetField,
                null,
                null,
                null);

            if (runtime == null)
                return;

            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
                BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.GetField,
                null,
                runtime,
                null);

            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.GetField,
                null,
                runtime,
                null);

            Log.Info(String.Format("\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}",
                shutDownMessage,
                shutDownStack));
        }

        //protected virtual void Application_PostAuthenticateRequest(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.Session == null) return;
        //    if (!this.Request.IsAuthenticated) return;
        //    var identity = new CustomIdentity(HttpContext.Current.User.Identity);
        //    var principal = new CustomPrincipal(identity);
        //    HttpContext.Current.User = principal;
        //}
     }
}