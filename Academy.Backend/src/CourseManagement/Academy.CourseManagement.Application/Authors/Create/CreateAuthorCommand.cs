using Academy.Core.Abstractions;

namespace Academy.CourseManagement.Application.Authors.Create
{
    public record CreateAuthorCommand : ICommand
    {
        public CreateAuthorCommand(string firstName, string lastName, string email, string phoneNumber)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public string Email { get; } = string.Empty;
        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public string PhoneNumber { get; } = string.Empty;
    }
}
