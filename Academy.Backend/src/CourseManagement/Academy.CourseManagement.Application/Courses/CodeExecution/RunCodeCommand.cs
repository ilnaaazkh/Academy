using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Courses.CodeExecution
{
    public record RunCodeCommand(string Code) : ICommand;
}
