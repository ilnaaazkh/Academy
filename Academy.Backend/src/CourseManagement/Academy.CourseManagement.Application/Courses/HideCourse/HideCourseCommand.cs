using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.HideCourse
{
    public record HideCourseCommand(Guid CourseId, Guid UserId) : ICommand;
}
