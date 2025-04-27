using Academy.Core.Extensions;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddModule
{
    public class AddModuleCommandValidator : AbstractValidator<AddModuleCommand>
    {
        public AddModuleCommandValidator()
        {
            RuleFor(m => m.Title).MustBeValueObject(Title.Create);
        }
    }

}
