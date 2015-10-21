namespace Bootstrapper.Web.Common.DependencyResolution.Registries
{
    using ServiceAgents.Common;

    using StructureMap.Configuration.DSL;

    using Infraestructure.Common.Module;

    public class BackendClientRegistry : Registry
    {
        public BackendClientRegistry()
        {
            this.Scan(x =>
            {
                x.AssemblyFromModule("ServiceAgents");
                x.Include(t => typeof(IBackendClient).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
                x.SingleImplementationsOfInterface()
                    .OnAddedPluginTypes(t => t.HttpContextScoped());
            }
            );
        }
    }
}
