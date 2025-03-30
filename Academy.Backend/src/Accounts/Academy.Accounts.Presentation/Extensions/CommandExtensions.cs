using Academy.Accounts.Application.RegisterUser;
using Academy.Accounts.Contracts.Requests;

namespace Academy.Accounts.Presentation.Extensions
{
    internal static class CommandExtensions
    {
        public static RegisterUserCommand ToCommand(this RegisterUserRequest request) 
            => new(request.Email, request.UserName, request.Password);
    }
}
