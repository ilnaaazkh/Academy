using Academy.Accounts.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Infrastructure.Models
{
    public class User : IdentityUser<Guid>
    {
        private List<Role> _roles = [];
        public IReadOnlyList<Role> Roles => _roles;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;

        public string FullName => FirstName + LastName + MiddleName;

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
