using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Accounts.Infrastructure.Models
{
    public static class CustomClaims
    {
        public static Claim Role(string? name) => new Claim("Role", name ?? string.Empty);
        public static Claim Permission(string code) => new Claim("Permission", code); 
    }
}
