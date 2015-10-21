namespace Bootstrapper.Wcf.Common.DependencyResolution.Registries
{
    using Infraestructure.Common.Module;

    using QueryContracts.Common;

    using QueryHandlers.Common;

    using StructureMap.Configuration.DSL;

    public class QueriesRegistry : Registry
    {
        public QueriesRegistry()
        {
            Scan(x =>
            {
                x.AssemblyFromModule("QueryHandlers");
                x.Assembly("QueryHandlers.Common");
                x.ConnectImplementationsToTypesClosing(typeof(IQueryHandler<>));
            });

            Scan(x =>
            {
                x.AssemblyFromModule("QueryContracts");
                x.Assembly("QueryContracts.Common");
                x.AddAllTypesOf<QueryParameter>();
                x.AddAllTypesOf<QueryResult>();
            });
        }
    }
}