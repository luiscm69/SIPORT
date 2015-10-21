namespace Infraestructure.Common.Validation
{
    using System.Collections.Generic;

    using FluentValidation.Results;

    public interface IValidationEngine
    {
        IList<ValidationFailure> Validate<T>(T objectToValidate)
            where T : class;
    }
}
