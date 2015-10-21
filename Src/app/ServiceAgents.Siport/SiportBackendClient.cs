using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceAgents.Siport
{
    using System.Reflection;

    using CommandContracts.Common;

    using QueryContracts.Common;

    using ServiceAgents.Common;
    using ServiceAgents.Siport.SiportReference;

    public class SiportBackendClient : IBackendClient
    {
        static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        readonly BackendServiceClient client = new BackendServiceClient("SiportWSHttpBinding_IBackendService", BindingClient.UrlDisponible(AssemblyName));

        public void Dispose()
        {
            client.Close();
        }

        public CommandResult ExecuteCommand(Command command)
        {
            return client.ExecuteCommand(command);
        }

        public QueryResult ExecuteQuery(QueryParameter parameter)
        {
            return this.client.ExecuteQuery(parameter);
        }
    }
}
