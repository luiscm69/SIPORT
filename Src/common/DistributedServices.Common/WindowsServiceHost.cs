
namespace DistributedServices.Common
{
    using System;
    using System.ComponentModel;
    using System.ServiceProcess;

    public partial class WindowsServiceHost : ServiceBase
    {
        private readonly BackendServiceHostContainer container;

        public WindowsServiceHost(Type serviceType)
        {
            container = new BackendServiceHostContainer(serviceType);
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            container.StartService();
        }

        protected override void OnStop()
        {
            container.StopService();
        }
    }
}
