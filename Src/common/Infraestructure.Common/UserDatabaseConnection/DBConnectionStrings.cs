
using System;
using System.Configuration;


namespace Infraestructure.Common.UserDatabaseConnection
{
    public class DBConnectionStrings
    {
        private string _USER = "UserDB";
        private string _PASSWD = "PassDB";
        private string _SERVERNAME = "ServerName";
        private string _DATABASE = "DataBaseName";
        private string _PROVIDERNAME = "System.Data.SqlClient";

        public string UserDB { get { return GetUserDb(); } }
        public string PassDB { get { return GetPassDb(); } }
        public string ServerName { get { return GetServerName(); } }
        public string DatabaseName { get { return GetDataBaseName(); } }
        public string ProviderName { get { return _PROVIDERNAME; } }


        private string GetDataBaseName()
        {
            if (ConfigurationManager.AppSettings[_DATABASE] == null)
                throw new InvalidOperationException(String.Format("No se ha encontrado el {0} en el archivo de configuración.", _DATABASE));
            return ConfigurationManager.AppSettings[_DATABASE].ToString();
        }

        private string GetPassDb()
        {
            if (ConfigurationManager.AppSettings[_PASSWD] == null)
                throw new InvalidOperationException(String.Format("No se ha encontrado el {0} en el archivo de configuración.", _PASSWD));
            return ConfigurationManager.AppSettings[_PASSWD].ToString();
        }

        private string GetUserDb()
        {
            if (ConfigurationManager.AppSettings[_USER] == null)
                throw new InvalidOperationException(String.Format("No se ha encontrado el {0} en el archivo de configuración.", _USER));
            return ConfigurationManager.AppSettings[_USER].ToString();
        }

        private string GetServerName()
        {
            if (ConfigurationManager.AppSettings[_SERVERNAME] == null)
                throw new InvalidOperationException(String.Format("No se ha encontrado el {0} en el archivo de configuración.", _SERVERNAME));
            return ConfigurationManager.AppSettings[_SERVERNAME].ToString();
        }
    }
}
