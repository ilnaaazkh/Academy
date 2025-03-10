using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.Delete
{
    public record DeleteCourseCommand(Guid CourseId) : ICommand;
}
