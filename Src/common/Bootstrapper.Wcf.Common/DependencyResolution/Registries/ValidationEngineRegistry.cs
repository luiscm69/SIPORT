namespace Bootstrapper.Wcf.Common.DependencyResolution.Registries
{
    using FluentValidation;

    using Infraestructure.Common.Validation;

    using StructureMap.Configuration.DSL;

    public class ValidationEngineRegistry : Registry
    {
        public ValidationEngineRegistry()
        {
            For<IValidationEngine>().Singleton().Use<ValidationEngine>();
            For<IValidatorFactory>().Singleton().Use<IocValidatorFactory>();
        }
    }
}