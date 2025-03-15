using Academy.Core.Extensions;
using Academy.SharedKernel.ValueObjects;
using FluentValidation;

namespace Academy.CourseManagement.Application.Courses.AddTestToLesson
{
    public class AddTestToLessonCommandValidator : AbstractValidator<AddTestToLessonCommand>
    {
        public AddTestToLessonCommandValidator()
        {
            RuleForEach(c => c.Questions)
                .MustBeValueObject(q => Question.Create(q.Title,
                    q.Answers.Select(a => Answer.Create(a.Title, a.IsCorrect).Value).ToList()));
        }
    }
}
