using System.Web.Mvc;
using System.Web.Routing;
namespace Web.Common.HttpApplications.ActionFilters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Items["RequestWasNotAuthorized"] = true;
            }
        }
    }
}
