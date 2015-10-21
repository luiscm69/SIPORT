namespace Web.Common.HttpApplications.ActionFilters
{
    using System.Web.Mvc;
    using Web.Common.Controllers;

    public class GlobalViewDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var controller = filterContext.Controller as BaseController;
                if (controller != null)
                {
                    var viewBag = controller.ViewBag;

                    viewBag.Usuario = controller.Usuario;
                    viewBag.Perfil = controller.Perfil;
                }
            }
        }
    }
}
