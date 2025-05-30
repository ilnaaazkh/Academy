using Academy.Accounts.Infrastructure.Models;
using Academy.Core.Abstractions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Application.RegisterAuthor
{
    public class RegisterAuthorCommandHandler : ICommandHandler<RegisterAuthorCommand>
    {
        private readonly UserManager<User> _userManager;

        public RegisterAuthorCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            RegisterAuthorCommand command,
            CancellationToken cancellationToken = default)
        {
            var user = new User { 
                Email = command.Email, 
                UserName = command.UserName, 
                FirstName = command.FirstName,
                LastName = command.LastName,
                MiddleName = command.MiddleName
            };
            var result = await _userManager.CreateAsync(user, command.Password);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description, null));
                return new ErrorList(errors);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.AUTHOR);

            if (roleResult.Succeeded == false)
            {
                var errors = roleResult.Errors.Select(e => Error.Validation(e.Code, e.Description, null));
                return new ErrorList(errors);
            }

            return Result.Success<ErrorList>();
        }
    }
}
