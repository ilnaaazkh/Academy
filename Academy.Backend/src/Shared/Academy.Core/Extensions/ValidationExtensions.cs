using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;

namespace Academy.Core.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject, Error>> factoryMethod)
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsSuccess)
                {
                    return;
                }

                context.AddFailure(result.Error.Serialize());
            });
        }

        public static IRuleBuilderOptions<T, TElement> WithError<T, TElement>(
            this IRuleBuilderOptions<T, TElement> ruleBuilder,
            Error error)
        {
            return ruleBuilder.WithMessage(error.Serialize());
        }

        public static ErrorList ToErrorList(this ValidationResult validationResult)
        {
            if (validationResult.IsValid) throw new InvalidOperationException("Cannot convert valid validation result to Error List");

            var validationErrors = validationResult.Errors;
            var errors = validationErrors.Select(ve => Error.Deserialize(ve.ErrorMessage));
            return new ErrorList(errors);
        }
    }
}
