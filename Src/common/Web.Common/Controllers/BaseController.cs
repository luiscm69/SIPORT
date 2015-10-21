
namespace Web.Common.Controllers
{
    using System.Web.Mvc;

    using CommandContracts.Common;

    using QueryContracts.Common;

    using StructureMap.Pipeline;

    using Web.Common.ActionResults;
    using Seguridad.Common;

    using SessionWrapper = Seguridad.Common.SessionWrapper;

    public class BaseController : Controller
    {
        public CommandActionResult Command(Command command)
        {
            return new CommandActionResult(command);
        }

        public CommandActionResult Command(Command command, string urlResultRedirect)
        {
            return new CommandActionResult(command, urlResultRedirect);
        }

        public Transaccion ExecuteCommand(ControllerContext context, Command command)
        {
            return new CommandActionResult(command).ExecuteCommand(context);
        }

        public QueryViewResult<T> Query<T>(QueryParameter parameter) where T : class
        {
            return new QueryViewResult<T>(parameter);
        }

        public Perfil Perfil
        {
            get { return SessionWrapper.Perfil; }
        }

        public Usuario Usuario
        {
            get
            { 
                return SessionWrapper.Usuario; 
            }
        }


    }
}
