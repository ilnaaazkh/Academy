

using Academy.Accounts.Application.RegisterUser;
using Academy.Core.Abstractions;

namespace Academy.Accounts.Application.RegisterAuthor
{
    public record RegisterAuthorCommand(
        string Email, 
        string Password, 
        string UserName, 
        string FirstName, 
        string LastName, 
        string MiddleName) : ICommand;
}
