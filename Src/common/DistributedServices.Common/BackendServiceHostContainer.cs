
namespace DistributedServices.Common
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Text;

    using Bootstrapper.Wcf.Common;

    using Infraestructure.Common.UserDatabaseConnection;

    using log4net;

    public class BackendServiceHostContainer
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Type serviceType;

        private ServiceHost host;

        public BackendServiceHostContainer(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public void StartService()
        {
            try
            {
                this.host = new ServiceHost(this.serviceType);
                WcfStartUp.Init(ConnectionStringInHeaderMessage);
                this.host.Open();
                Log.Info("El Servicio ha sido iniciado correctamente.");
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message, e);
                if (this.host != null) this.host.Abort();
                throw;
            }
        }
        public void StopService()
        {
            try
            {
                if (this.host == null){
                    return;
                }
                this.host.Close();
                Log.Info("El Servicio ha sido detenido.");
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message, e);
                var serviceHost = this.host;
                if (serviceHost != null){
                    serviceHost.Abort();
                }
            }
        }

        private bool ConnectionStringInHeaderMessage
        {
            get
            {
                var mainEndpoint = this.host.Description.Endpoints.SingleOrDefault(x => x.Name == "main");
                if (mainEndpoint == null)
                {
                    throw new Exception("Endpoint 'main' was not found ");
                }
                var res = mainEndpoint.Behaviors.Find<UserDatabaseConnectionBehavior>();
                //return false;
                return res != null;
            }
        }

        public string GetHostedServiceDescription()
        {
            StringBuilder svcInfo = new StringBuilder();
            svcInfo.AppendLine(host.Description.ConfigurationName);

            foreach (ServiceEndpoint endPoint in host.Description.Endpoints)
            {
                svcInfo.AppendLine(endPoint.ListenUri.ToString());
            }

            return svcInfo.ToString();
        }
    }
}
