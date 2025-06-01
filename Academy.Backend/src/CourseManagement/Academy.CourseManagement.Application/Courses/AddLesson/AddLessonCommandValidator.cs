using Academy.Core.Extensions;
using Academy.CourseManagement.Domain;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddLesson
{
    public class AddLessonCommandValidator : AbstractValidator<AddLessonCommand>
    {
        public AddLessonCommandValidator()
        {
            RuleFor(c => c.Title).MustBeValueObject(Title.Create);
            RuleFor(c => c.LessonType).MustBeValueObject(LessonType.Create);
        }
    }
}
