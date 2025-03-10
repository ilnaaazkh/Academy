using Academy.Core.Extensions;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.UpdateModule
{
    public class UpdateModuleCommandValidator : AbstractValidator<UpdateModuleCommand> 
    {
        public UpdateModuleCommandValidator()
        {
            RuleFor(m => m.Title).MustBeValueObject(Title.Create);
            RuleFor(m => m.Description).MustBeValueObject(Description.Create);
        }
    }
}
