using Academy.Accounts.Application.LoginUser;
using Academy.Accounts.Application.RefreshTokens;
using Academy.Accounts.Application.RegisterUser;
using Academy.Accounts.Contracts.Requests;

namespace Academy.Accounts.Presentation.Extensions
{
    internal static class CommandExtensions
    {
        public static RegisterUserCommand ToCommand(this RegisterUserRequest request) 
            => new(request.Email, request.Email, request.Password);
        
        public static LoginUserCommand ToCommand(this LoginUserRequest request) 
            => new(request.Email, request.Password);
    }
}
