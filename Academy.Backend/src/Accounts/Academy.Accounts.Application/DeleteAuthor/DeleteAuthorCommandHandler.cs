using Academy.Accounts.Infrastructure.Models;
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Application.DeleteAuthor
{
    public record DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand>
    {
        private readonly UserManager<User> _userManager;

        public DeleteAuthorCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UnitResult<ErrorList>> Handle(
            DeleteAuthorCommand command, 
            CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(command.AuthorId.ToString());

            if (user == null)
                return UnitResult.Success<ErrorList>();

            var result = await _userManager.RemoveFromRoleAsync(user, Roles.AUTHOR);

            if (result.Succeeded == false)
            {
                return Errors.General.Failure().ToErrorList();
            }

            return UnitResult.Success<ErrorList>();
        }
    }
}
