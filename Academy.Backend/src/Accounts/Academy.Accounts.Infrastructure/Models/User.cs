using Academy.Accounts.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Infrastructure.Models
{
    public class User : IdentityUser<Guid>
    {
        private List<Role> _roles = [];

        public IReadOnlyList<Role> Roles => _roles;

        public static User CreateAdmin(string email, string username, Role role)
        {
            return new User
            {
                UserName = username,
                Email = email,
                _roles = [role]
            };
        }
    }
}
