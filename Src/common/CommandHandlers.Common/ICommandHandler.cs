namespace CommandHandlers.Common
{
    using CommandContracts.Common;

    public interface ICommandHandler<in T> where T : Command
    {
        CommandResult Handle(T command);
    }
}