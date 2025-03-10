using Academy.Core.Extensions;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.Update
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand> 
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(c => c.Title).MustBeValueObject(Title.Create);
            RuleFor(c => c.Description).MustBeValueObject(Description.Create);
        }
    }
}
