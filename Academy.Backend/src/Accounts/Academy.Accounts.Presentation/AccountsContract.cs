using Academy.Accounts.Contracts;
using Academy.Accounts.Infrastructure.Models;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Presentation
{
    public class AccountsContract : IAccountsContract
    {
        private readonly UserManager<User> _userManager;

        public AccountsContract(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UnitResult<ErrorList>> ApproveAuthoringRequest(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                return Errors.General.NotFound(userId).ToErrorList();

            var result = await _userManager.AddToRoleAsync(user, Roles.AUTHOR);

            if (result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description, null));
                return new ErrorList(errors);
            }

            var deleteRoleResult = await _userManager.RemoveFromRoleAsync(user, Roles.STUDENT);

            if (deleteRoleResult.Succeeded == false)
            {
                var errors = result.Errors.Select(e => Error.Validation(e.Code, e.Description, null));
                return new ErrorList(errors);
            }

            return UnitResult.Success<ErrorList>();
        }
    }
}
