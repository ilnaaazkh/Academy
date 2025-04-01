using Academy.Accounts.Infrastructure.Models;
using Academy.Accounts.Infrastructure.Providers;
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Application.LoginUser
{
    public class LoginUserCommandHandler : ICommandHandler<string, LoginUserCommand>
    {
        private readonly JwtProvider _tokenProvider;
        private readonly UserManager<User> _userManager;

        public LoginUserCommandHandler(
            JwtProvider tokenProvider, 
            UserManager<User> userManager)
        {
            _tokenProvider = tokenProvider;
            _userManager = userManager;
        }
        public async Task<Result<string, ErrorList>> Handle(
            LoginUserCommand command, 
            CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
                return Errors.User.InvalidCredentials().ToErrorList();

            var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);

            if (passwordConfirmed == false)
                return Errors.User.InvalidCredentials().ToErrorList();

            var token = _tokenProvider.GenerateToken(user);

            return token;
        }
    }
}
