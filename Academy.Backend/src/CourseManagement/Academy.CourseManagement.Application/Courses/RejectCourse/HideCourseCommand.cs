using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.RejectCourse
{
    public record RejectCourseCommand(Guid CourseId) : ICommand;
}
