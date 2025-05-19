using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.AddLessonContent
{
    public record AddLessonContentCommand(
        string Content, 
        Guid CourseId,
        Guid ModuleId,
        Guid LessonId,
        Guid UserId) : ICommand;
}
