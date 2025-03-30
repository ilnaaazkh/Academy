using Academy.Core.Abstractions;

namespace Academy.Accounts.Application.RegisterUser
{
    public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand;
}
