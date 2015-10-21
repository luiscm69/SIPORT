using System.Web.Mvc;

namespace Web.Siport.Controllers
{
    using System.Web.Security;

    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("SignIn", "Account", null);
        }
    }
}
