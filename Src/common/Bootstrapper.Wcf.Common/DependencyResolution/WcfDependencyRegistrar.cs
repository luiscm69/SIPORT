

namespace Bootstrapper.Wcf.Common.DependencyResolution
{
    using System.Reflection;

    using Bootstrapper.Wcf.Common.DependencyResolution.Registries;

    using StructureMap;

    public class WcfDependencyRegistrar
    {
        public static void RegisterDependencies()
        {
            var getexecutingassembly = Assembly.GetExecutingAssembly();
            var namespaceWcfDependencyRegistrar = typeof(WcfDependencyRegistrar).Namespace;
            var nameContractValidatorsRegistry = typeof(ContractValidatorsRegistry).Namespace;


            ObjectFactory.Initialize(x => x.Scan(scan =>
            {
                scan.Assembly(getexecutingassembly);
                scan.LookForRegistries();
                scan.IncludeNamespace(namespaceWcfDependencyRegistrar);
                scan.IncludeNamespace(nameContractValidatorsRegistry);
            }));
        }
    }
}
