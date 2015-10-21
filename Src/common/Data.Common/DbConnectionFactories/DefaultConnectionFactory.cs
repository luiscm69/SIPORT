

namespace Data.Common.DbConnectionFactories
{
    using Infraestructure.Common.UserDatabaseConnection;
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;

    public class DefaultConnectionFactory : IDbConnectionFactory
    {
        private const string InvariantName = "System.Data.SqlClient";
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(InvariantName);
            if (providerFactory == null)
                throw new InvalidOperationException(String.Format("The '{0}' provider is not registered on the local machine.", InvariantName));

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = DefaultConnectionString;
            return connection;

        }

        public static string DefaultConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SiportSeg"].ConnectionString;
            }
        }



    
    }

    
}
