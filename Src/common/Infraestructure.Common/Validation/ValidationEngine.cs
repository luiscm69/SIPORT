namespace Infraestructure.Common.Validation
{
    using System.Collections.Generic;

    using FluentValidation;
    using FluentValidation.Results;

    public class ValidationEngine : IValidationEngine
    {
        private readonly IValidatorFactory validatorFactory;

        public ValidationEngine(IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
        }

        public IList<ValidationFailure> Validate<T>(T objectToValidate)
            where T : class
        {
            var validator = this.validatorFactory.GetValidator(objectToValidate.GetType());

            if (validator == null)
                return new List<ValidationFailure>();
            var result = validator.Validate(objectToValidate);
            return result.Errors;
        }
    }
}
