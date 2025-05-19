using Academy.Core.Extensions;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddLessonContent
{
    public class AddLessonContentCommandValidator : AbstractValidator<AddLessonContentCommand>
    {
        public AddLessonContentCommandValidator()
        {
            RuleFor(c => c.Content).MustBeValueObject(Content.Create);
        }
    }
}
