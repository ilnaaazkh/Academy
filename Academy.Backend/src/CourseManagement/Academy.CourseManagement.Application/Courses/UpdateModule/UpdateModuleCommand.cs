using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.UpdateModule
{
    public record UpdateModuleCommand(
        Guid CourseId,
        Guid ModuleId,
        string Title,
        string Description) : ICommand;
}
