using Academy.Accounts.Infrastructure.Models;
using Academy.Core.Abstractions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Application.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            RegisterUserCommand command, 
            CancellationToken cancellationToken = default)
        {
            var user = new User { Email = command.Email, UserName = command.UserName };
            var result = await _userManager.CreateAsync(user, command.Password);

            if(result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description, null));
                return new ErrorList(errors);
            }

            await _userManager.AddToRoleAsync(user, Roles.STUDENT);

            return Result.Success<ErrorList>();
        }
    }
}
