using Academy.Core.Extensions;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;


namespace Academy.CourseManagement.Application.Authors.Create
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(c => c.Email).MustBeValueObject(Email.Create);
            RuleFor(c => c.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
            RuleFor(c => new { c.FirstName, c.LastName })
                .MustBeValueObject(fl => FullName.Create(fl.FirstName, fl.LastName));
        }
    }
}
