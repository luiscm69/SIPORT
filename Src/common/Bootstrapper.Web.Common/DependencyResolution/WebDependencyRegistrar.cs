namespace Bootstrapper.Web.Common.DependencyResolution
{
    using System.Reflection;
    using Bootstrapper.Web.Common.DependencyResolution.Registries;
    using StructureMap;

    public class WebDependencyRegistrar
    {
        public static void RegisterDependencies()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
            {
                scan.Assembly(Assembly.GetExecutingAssembly());
                scan.LookForRegistries();
                scan.IncludeNamespace(typeof(WebDependencyRegistrar).Namespace);
                scan.IncludeNamespace(typeof(ContractValidatorsRegistry).Namespace);
            }));
        }
    }
}
