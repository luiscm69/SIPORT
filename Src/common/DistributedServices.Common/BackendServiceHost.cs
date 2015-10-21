
namespace DistributedServices.Common
{
    using System;
    using System.ServiceProcess;

    public class BackendServiceHost<T> where T : IBackendService
    {
        public static void Run(string[] args)
        {
            if (IsConsoleHost(args))
            {
                var container = new BackendServiceHostContainer(typeof(T));
                container.StartService();
                Console.WriteLine("Hosting the following services:");
                var name = container.GetHostedServiceDescription();
                Console.WriteLine("Service: " + name);
                Console.WriteLine("Press <Enter> to close.");
                Console.ReadLine();

                container.StopService();
            }
            else
            {
                var servicesToRun = new ServiceBase[] { new WindowsServiceHost(typeof(T)) };
                ServiceBase.Run(servicesToRun);
            }
        }

        private static bool IsConsoleHost(string[] args)
        {
            return args.Length > 0 && args[0].Trim().ToUpperInvariant() == "-D";
        }
    }
}
