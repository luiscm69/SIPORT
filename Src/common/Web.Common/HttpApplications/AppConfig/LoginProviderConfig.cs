
namespace Web.Common.HttpApplications.AppConfig
{
    using Infraestructure.Common.Logging.DataProviders;
    using log4net;

    public class LoginProviderConfig
    {
        public static void RegisterLoggingProviders()
        {
            GlobalContext.Properties["user"] = new HttpContextUserNameProvider();
        }
    }
}
