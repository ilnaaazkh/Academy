using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Authors.Update.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid Id,
        string Email,
        string PhoneNumber,
        string FirstName,
        string LastName
    ) : ICommand;
}
