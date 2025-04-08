using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Infrastructure.Models
{
    public class Role : IdentityRole<Guid> 
    {
        public ICollection<Permission> Permissions { get; set; }
    }
}
