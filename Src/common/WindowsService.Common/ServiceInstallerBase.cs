using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Common
{
    using System.Collections;
    using System.Configuration.Install;
    using System.Globalization;
    using System.ServiceProcess;

    [RunInstaller(true)]
    public partial class ServiceInstallerBase : Installer
    {
        private readonly ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
        private readonly ServiceInstaller serviceInstaller = new ServiceInstaller();

        protected string Name;
        protected string DisplayName;
        protected string Description;
        protected ServiceAccount ServiceAccount = ServiceAccount.LocalSystem;


        public ServiceInstallerBase()
        {
            InitializeComponent();
            PerformInstall();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            SetParameters();

            base.OnBeforeInstall(savedState);
        }

        private void SetParameters()
        {
            Name = Context.Parameters["name"] ?? Name;
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException("Name parameter was not supplied.");

            this.serviceInstaller.ServiceName = Name;
            this.serviceInstaller.StartType = ServiceStartMode.Automatic;
            this.processInstaller.Account = ServiceAccount;
            this.processInstaller.Password = null;

            DisplayName = Context.Parameters["displayname"] ?? DisplayName;
            if (!string.IsNullOrEmpty(DisplayName))
                this.serviceInstaller.DisplayName = DisplayName;

            Description = Context.Parameters["description"] ?? Description;
            if (!string.IsNullOrEmpty(Description))
                this.serviceInstaller.Description = Description;

            Context.LogMessage(string.Format(CultureInfo.InvariantCulture, "Name: {0}", this.serviceInstaller.ServiceName));
            Context.LogMessage(string.Format(CultureInfo.InvariantCulture, "Display Name: {0}", this.serviceInstaller.DisplayName));
            Context.LogMessage(string.Format(CultureInfo.InvariantCulture, "Description: {0}", this.serviceInstaller.Description));
        }

        private void PerformInstall()
        {
            Installers.Add(this.processInstaller);
            Installers.Add(this.serviceInstaller);
        }
    }
}
