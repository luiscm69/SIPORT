namespace Web.Common.HttpApplications.AppConfig
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
 
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Error - 404",
                "NotFound",
                new { controller = "Error", action = "NotFound" }
                );

            routes.MapRoute(
                "Error - 500",
                "ServerError",
                new { controller = "Error", action = "ServerError" }
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Seguridad", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );


        }
    }
}
