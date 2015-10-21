namespace Web.Common.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult NotFound(string aspxerrorpath)
        {
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return this.View();
        }

        public ActionResult ServerError()
        {
            this.Response.TrySkipIisCustomErrors = true;
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View();
        }
    }
}
