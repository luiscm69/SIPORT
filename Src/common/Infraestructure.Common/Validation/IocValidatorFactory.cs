namespace Infraestructure.Common.Validation
{
    using System;

    using FluentValidation;

    using StructureMap;

    public class IocValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            return ObjectFactory.TryGetInstance(validatorType) as IValidator;
        }
    }
}
