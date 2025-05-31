using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.SendOnModeration
{
    public record SendOnModerationCommand(Guid CourseId, Guid UserId) : ICommand;
}
