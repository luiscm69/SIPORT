
namespace ServiceAgents.Common
{
    using System;

    using CommandContracts.Common;

    using QueryContracts.Common;

    public interface IBackendClient : IDisposable
    {
        CommandResult ExecuteCommand(Command command);

        QueryResult ExecuteQuery(QueryParameter parameter);
    }
}
