using System;

namespace Web.Common.ActionResults
{
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;
    using System.Web.Mvc;

    using CommandContracts.Common;

    using log4net;

    using ServiceAgents.Common;

    using StructureMap;

    public class CommandActionResult : ActionResult
    {
        private Command command;
        private ActionResult successAction;
        private ViewResult errorView;
        private ActionResult DEFAULT_SUCCESS_ACTION = new RedirectResult("Index");
        private ViewResult DEFAULT_ERROR_VIEW = new ViewResult();
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CommandActionResult()
        {
        }

        //Se puede modificar esta funcion para recibir tambien otro actionResult
        public CommandActionResult(Command command)
        {
            this.command = command;
            this.successAction = DEFAULT_SUCCESS_ACTION;
            this.errorView = DEFAULT_ERROR_VIEW;
        }

        public CommandActionResult(Command command, string urlResultRedirect)
        {
            DEFAULT_SUCCESS_ACTION = new RedirectResult(urlResultRedirect);
            this.command = command;
            this.successAction = DEFAULT_SUCCESS_ACTION;
            this.errorView = DEFAULT_ERROR_VIEW;

        }

        public override void ExecuteResult(ControllerContext context)
        {
            CommandResult resultadobackendClient = null;
            if (context.Controller.ViewData.ModelState.IsValid)
            {
                var backendClient = ObjectFactory.GetInstance<IBackendClient>();
                try
                {
                    resultadobackendClient = backendClient.ExecuteCommand(command);
                }
                catch (FaultException<CommandFault> e)
                {
                    foreach (var error in e.Detail.CommandErrors)
                    {
                        context.Controller.ViewData.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }

            if (!context.Controller.ViewData.ModelState.IsValid)
            {
                errorView.ViewData = context.Controller.ViewData;
                errorView.TempData = context.Controller.TempData;
                errorView.ExecuteResult(context);
                return;
            }
            //context.Controller.TempData["Resultado"] = "OK";
            context.Controller.TempData["ResultadoCommand"] = resultadobackendClient;
            successAction.ExecuteResult(context);

        }

        public Transaccion ExecuteCommand(ControllerContext context)
        {
            ObjetoMetadata(command);
            CommandResult resultadobackendClient = null;
            if (context.Controller.ViewData.ModelState.IsValid)
            {
                var backendClient = ObjectFactory.GetInstance<IBackendClient>();

                try
                {
                    resultadobackendClient = backendClient.ExecuteCommand(command);
                }
                catch (FaultException<CommandFault> e)
                {
                    foreach (var error in e.Detail.CommandErrors)
                    {
                        log.Error(error.ErrorMessage + "::::::" + error.PropertyName);
                        if (!error.ErrorMessage.Contains("EXCEPTION"))
                        {
                            context.Controller.ViewData.ModelState.AddModelError(
                                error.PropertyName,
                                error.ErrorMessage.Contains("MESSAGE")
                                    ? error.ErrorMessage.Split('|')[1]
                                    : error.ErrorMessage);
                        }
                        else
                        {
                            context.Controller.ViewData.ModelState.AddModelError(error.PropertyName, "Se ha producido un error en la base de datos");
                        }
                    }
                }
            }

            if (!context.Controller.ViewData.ModelState.IsValid)
            {
                errorView.ViewData = context.Controller.ViewData;
                errorView.TempData = context.Controller.TempData;

                return new Transaccion { ViewResult = errorView, ResultadoTransaccion = ResultadoTransaccion.Fallido };
            }
            //context.Controller.TempData["Resultado"] = "OK";
            context.Controller.TempData["ResultadoCommand"] = resultadobackendClient;
            return new Transaccion { ViewResult = successAction, CommandResult = resultadobackendClient, ResultadoTransaccion = ResultadoTransaccion.Exito };
        }
        
        public CommandActionResult OnSuccess(ActionResult successAction)
        {
            this.successAction = successAction;
            return this;
        }

        public CommandActionResult OnError(ViewResult errorView)
        {
            this.errorView = errorView;
            return this;
        }

        private static void ObjetoMetadata(object objeto)
        {
            foreach (var infoMiembro in objeto.GetType().GetMembers().Where(infoMiembro => infoMiembro.MemberType == MemberTypes.Property))
            {
                log.Debug("::::::METADATA SERVER WEB::::::" + objeto.GetType().FullName + "  " + (infoMiembro).Name + " = " + ((PropertyInfo)infoMiembro).GetValue(objeto, null) + "-" + ((PropertyInfo)infoMiembro).PropertyType.FullName);
            }
        }
    }

    public enum ResultadoTransaccion
    {
        Exito = 1,
        Fallido = 2
    }

    public class Transaccion : CommandResult
    {
        public ActionResult ViewResult { get; set; }

        public object CommandResult { get; set; }

        public ResultadoTransaccion ResultadoTransaccion { get; set; }
    }
}
