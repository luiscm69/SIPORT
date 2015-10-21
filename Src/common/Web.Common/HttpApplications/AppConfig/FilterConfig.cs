using System.Web.Mvc;
using Web.Common.HttpApplications.ActionFilters;
namespace Web.Common.HttpApplications.AppConfig
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

            //if (!AppConfigurationSettings.IsLocalEnvironment)
            //{
            //    filters.Add(new RequireHttpsAttribute(), 1);
            //}
            //filters.Add(new HandleErrorAttribute(), 1);
            filters.Add(new CustomAuthorizeAttribute(), 2);
            //filters.Add(new SessionExpiredAttribute(), 3);
            filters.Add(new PerfilLoadAttribute(), 4);
            filters.Add(new EncryptedActionParameterAttribute(), 5);
            filters.Add(new GlobalViewDataAttribute(), 6);

            ////if (!AppConfigurationSettings.IsProductionEnvironment)
            ////{
            ////    filters.Add(new LoggingAttribute(), 6);
            ////}

        }
    }
}