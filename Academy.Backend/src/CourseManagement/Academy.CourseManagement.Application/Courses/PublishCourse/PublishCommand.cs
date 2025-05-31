using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.Publish
{

    public record PublishCommand(Guid CourseId) : ICommand;
}
