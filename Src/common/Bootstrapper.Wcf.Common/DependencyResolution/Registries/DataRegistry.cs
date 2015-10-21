namespace Bootstrapper.Wcf.Common.DependencyResolution.Registries
{
    using Data.Common;
    using Data.Common.UnitOfWork;

    using Domain.Common.Contracts;

    using StructureMap.Configuration.DSL;

    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For(typeof(IRepository<>)).Use(typeof(Repository<>));
            For<IUnitOfWork>().Use<UnitOfWork>();
        }
    }
}