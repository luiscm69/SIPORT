

namespace Data.Common.DbConnectionFactories
{
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    using Infraestructure.Common.UserDatabaseConnection;

    public class UserSessionConnectionFactory : IDbConnectionFactory
    {
        private const string InvariantName = "System.Data.SqlClient";
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(InvariantName);
            if (providerFactory == null)
                throw new InvalidOperationException(String.Format("The '{0}' provider is not registered on the local machine.", InvariantName));

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = UserSessionConnectionString;
            return connection;

        }

        public static string UserSessionConnectionString
        {
            get
            {
                Connection connectionHeader = Connection.GetHeaderFromMessage();

                var builder = new SqlConnectionStringBuilder(DefaultConnectionFactory.DefaultConnectionString)
                {
                    UserID = connectionHeader.User,
                    Password = connectionHeader.Password
                };
                return builder.ConnectionString;
            }
        }

    }


}
