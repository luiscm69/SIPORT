namespace Bootstrapper.Wcf.Common.DependencyResolution.Registries
{
    using CommandContracts.Common;

    using CommandHandlers.Common;

    using Infraestructure.Common.Module;

    using StructureMap.Configuration.DSL;

    public class CommandsRegistry : Registry
    {
        public CommandsRegistry()
        {
            Scan(x =>
            {
                x.AssemblyFromModule("CommandHandlers");
                x.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
            });

            Scan(x =>
            {
                x.AssemblyFromModule("CommandContracts");
                x.AddAllTypesOf<Command>();
                x.AddAllTypesOf<CommandResult>();
            });
        }
    }
}