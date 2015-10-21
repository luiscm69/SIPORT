namespace Web.Common.HttpApplications.AppConfig
{
    using System.Web.Http;
    using System.Web.UI.WebControls;

    using RouteParameter = System.Web.Http.RouteParameter;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
