using FluentValidation;
using FluentValidation.Results;

namespace WaterTrans.Boilerplate.Domain.Abstractions
{
    public interface IValidatableObject<T>
    {
        IValidator<T> Validator { get; }
        ValidationResult Validate();
        void ValidateAndThrow();
    }
}
