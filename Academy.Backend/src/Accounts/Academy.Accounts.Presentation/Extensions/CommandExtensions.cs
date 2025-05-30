using Academy.Accounts.Application.LoginUser;
using Academy.Accounts.Application.RefreshTokens;
using Academy.Accounts.Application.RegisterAuthor;
using Academy.Accounts.Application.RegisterUser;
using Academy.Accounts.Contracts.Requests;

namespace Academy.Accounts.Presentation.Extensions
{
    internal static class CommandExtensions
    {
        public static RegisterUserCommand ToCommand(this RegisterUserRequest r) 
            => new(r.Email, r.Email, r.Password);

        public static RegisterAuthorCommand ToCommand(this RegisterAuthorRequest r)
            => new(r.Email, r.Password, r.Email, r.FirstName, r.LastName, r.MiddleName);

        public static LoginUserCommand ToCommand(this LoginUserRequest r) 
            => new(r.Email, r.Password);
    }
}
