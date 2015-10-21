namespace Bootstrapper.Web.Common
{
    using System.Web.Mvc;

    using Bootstrapper.Web.Common.DependencyResolution;

    using FluentValidation.Mvc;

    using Infraestructure.Common.Logging;
    using Infraestructure.Common.Logging.AppenderBuilders;
    using Infraestructure.Common.Module;
    using Infraestructure.Common.Validation;

    using StructureMap;

    public class WebStartUp
    {
        public static void Init()
        {
            RegisterModuleName();
            ConfigureLogging();
            ConfigureDependencies();
            ConfigureValidation();
        }

        private static void RegisterModuleName()
        {
            ModuleStorage.RegisterCurrentModuleFromConfigFile();
        }

        private static void ConfigureValidation()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Clear();
            FluentValidationModelValidatorProvider.Configure(
                provider => { provider.ValidatorFactory = new IocValidatorFactory(); });
        }

        private static void ConfigureDependencies()
        {
            WebDependencyRegistrar.RegisterDependencies();
            DependencyResolver.SetResolver(new WebDependencyResolver(ObjectFactory.Container));
        }

        private static void ConfigureLogging()
        {
            LoggingConfigurator.AddAppenderBuilder<FileAppenderBuilder>();
            LoggingConfigurator.Configure();
        }
    }
}