
namespace CommandHandlers.Common
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;

    using CommandContracts.Common;

    using Data.Common.UnitOfWork;

    using Domain.Common.Exceptions;

    using Infraestructure.Common.Validation;

    using log4net;

    using StructureMap;

    public class CommandDispatcher
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CommandResult Dispatch<T>(T command) where T : Command
        {
            var validationEngine = ObjectFactory.GetInstance<IValidationEngine>();
            var validationErrors = validationEngine.Validate(command);

            if (validationErrors.Any())
            {
                var commandErrors = validationErrors.Select(e => new CommandError(e.PropertyName, e.ErrorMessage)).ToList();
                throw new FaultException<CommandFault>(new CommandFault(commandErrors), new FaultReason("El command tiene errores de validación"));
            }

            var sw = Stopwatch.StartNew();
          
            try
            {
                using (var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>())
                {
                    var totalMemoriaInicial = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
                    var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

                    dynamic handler = ObjectFactory.With(unitOfWork).GetInstance(handlerType);
                    var rnd = new Random();

                    var idThread = rnd.Next();
                    var parametros = ObtenerParametros(command);

                    log.Debug(string.Format("|Ini|Command|{0}|{1}|{2}{3}", idThread, handler.ToString(), totalMemoriaInicial.ToString(), parametros));
                    var result = handler.Handle(command as dynamic);
                    unitOfWork.Commit();
                    sw.Stop();

                    var totalMemoriaFinal = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;
                    GC.Collect();
                    var totalMemoriaReducida = System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024;

                    var elapsedTime = sw.Elapsed;

                    log.Debug(string.Format("|Fin|Command|{0}|{1}|{2}|{3}|{4}|{5}", idThread, handler.ToString(), elapsedTime, totalMemoriaInicial.ToString(), totalMemoriaFinal.ToString(), totalMemoriaReducida.ToString()));

                    return result;
                }
            }
            catch (DomainException e)
            {
                sw.Stop();
                log.Error(e.Message, e);
                //enviarCorreoError(e);
                throw new FaultException<CommandFault>(new CommandFault(e.Message), new FaultReason(e.Message));
            }
            catch (Exception e)
            {
                sw.Stop();
                //enviarCorreoError(e);
                log.Error(e.Message, e);
                e = this.Unwrap(e);
                log.Error(e.Message, e);
                throw new FaultException<CommandFault>(new CommandFault("Se ha producido un error"), new FaultReason("Se ha producido un error"));
            }


        }

        private Exception Unwrap(Exception ex)
        {
            while (null != ex.InnerException)
            {
                ex = ex.InnerException;
            }

            return ex;
        }

        public string ObtenerParametros(object objeto)
        {
            var parametros = string.Empty;
            try
            {
                foreach (var infoMiembro in objeto.GetType().GetMembers())
                {
                    if (infoMiembro.MemberType == MemberTypes.Property)
                    {
                        var valorParam = string.Empty;
                        if (((PropertyInfo)infoMiembro).GetValue(objeto, null) != null)
                        {
                            valorParam = ((PropertyInfo)infoMiembro).GetValue(objeto, null).ToString();
                        }
                        parametros = parametros + "|" + (infoMiembro).Name + ": " + valorParam;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                parametros = ex.Message;
            }

            return parametros;
        }
    }
}
