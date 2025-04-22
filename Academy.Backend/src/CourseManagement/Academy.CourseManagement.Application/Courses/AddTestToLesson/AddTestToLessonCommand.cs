
using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.AddTestToLesson
{
    public record AddTestToLessonCommand(
         Guid CourseId,
         Guid ModuleId,
         Guid LessonId,
         IEnumerable<TestQuestionDto> Questions,
         Guid UserId
        ) : ICommand;
}
