using Academy.Accounts.Contracts;
using Academy.Accounts.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Academy.Accounts.Presentation
{
    public class AccountsContract : IAccountsContract
    {
        private readonly UserManager<User> _userManager;

        public AccountsContract(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsUserExist(Guid userId, CancellationToken cancellationToken)
        {
            var result =  await _userManager.Users.AnyAsync(u => u.Id == userId);
            return result;
        }
    }
}
