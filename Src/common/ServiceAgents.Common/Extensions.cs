

namespace ServiceAgents.Common
{
    using System.Reflection;
    using System.ServiceModel;

    using CommandContracts.Common;

    using log4net;

    using QueryContracts.Common;

    using StructureMap;

    public static class Extensions
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static object Execute(this QueryParameter parameter)
        {
            var backendClient = ObjectFactory.GetInstance<IBackendClient>();
            parameter.ServidorWeb = System.Environment.MachineName;

            try
            {
                object objServicio = backendClient.ExecuteQuery(parameter);
                return objServicio;
            }
            catch (EndpointNotFoundException ex)
            {
                log.Error(ex.Message, ex);
                throw;
             }
        }

        public static object Execute(this Command command)
        {
            var backendClient = ObjectFactory.GetInstance<IBackendClient>();

            try
            {
                object objServicio = backendClient.ExecuteCommand(command);
                return objServicio;
            }
            catch (EndpointNotFoundException ex)
            {
                        
                log.Error(ex.Message, ex);
                throw;
            }
        }

    }
}
