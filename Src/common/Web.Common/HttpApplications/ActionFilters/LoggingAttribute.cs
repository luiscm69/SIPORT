namespace Web.Common.HttpApplications.ActionFilters
{
    using log4net;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class LoggingAttribute : ActionFilterAttribute
    {
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            log.Debug(Message("Inicio", filterContext.RouteData));
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            log.Debug(Message("Fin", filterContext.RouteData));
        }

        private static string Message(string method, RouteData routeData)
        {
            return string.Format("{0} controller:{1} action:{2}", method, routeData.Values["controller"], routeData.Values["action"]);
        }
    }
}