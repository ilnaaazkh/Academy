using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.DeleteModule
{
    public record DeleteModuleCommand(
        Guid CourseId,
        Guid ModuleId,
        Guid UserId) : ICommand;
}
