namespace Infraestructure.Common.Configuration
{
    using System;
    using System.Configuration;

    public class AppConfigurationSettings
    {
        public static bool IsLocalEnvironment
        {
            get
            {
                return Environment.Equals("local", StringComparison.OrdinalIgnoreCase);
            }
        }

        public static bool IsProductionEnvironment
        {
            get
            {
                return Environment.Equals("produccion", StringComparison.OrdinalIgnoreCase);
            }
        }

        public static bool IsDebugEnvironment
        {
            get
            {
                return Environment.Equals("debug", StringComparison.OrdinalIgnoreCase);
            }
        }

        public static string Environment
        {
            get
            {
                var environment = ConfigurationManager.AppSettings["Environment"];
                if (string.IsNullOrEmpty(environment))
                    environment = "Local";
                return environment;
            }
        }

    }
}
