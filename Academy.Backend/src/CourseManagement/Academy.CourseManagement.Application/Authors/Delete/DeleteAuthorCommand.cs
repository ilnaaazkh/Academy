using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Authors.Delete
{
    public record DeleteAuthorCommand(Guid Id) : ICommand;
}
