namespace DistributedServices.Siport
{
    using DistributedServices.Common;

    class Program
    {
        static void Main(string[] args)
        {
            BackendServiceHost<SiportBackendService>.Run(args);
        }
    }
}
