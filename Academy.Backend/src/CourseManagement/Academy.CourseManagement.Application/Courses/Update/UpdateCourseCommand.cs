
using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.Update
{
    public record UpdateCourseCommand(
        Guid Id,
        string Title,
        string Description,
        Guid UserId
        ) : ICommand;
}
