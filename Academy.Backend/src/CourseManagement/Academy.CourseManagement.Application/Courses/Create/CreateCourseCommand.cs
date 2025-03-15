using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.Create
{
    public record CreateCourseCommand(
        string Title,
        string Description,
        Guid AuthorId) : ICommand;
}
