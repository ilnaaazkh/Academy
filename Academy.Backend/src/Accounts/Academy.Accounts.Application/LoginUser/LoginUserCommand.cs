using Academy.Core.Abstractions;

namespace Academy.Accounts.Application.LoginUser
{
    public record LoginUserCommand(string Email, string Password) : ICommand;
}
