using Academy.Accounts.Infrastructure.Managers;
using Academy.Accounts.Infrastructure.Models;
using Academy.Accounts.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Academy.Accounts.Infrastructure.Providers
{
    public class JwtProvider 
    {
        private readonly PermissionManager _permissionManager;
        private readonly JwtOptions _options;

        public JwtProvider(PermissionManager permissionManager, IOptions<JwtOptions> options)
        {
            _permissionManager = permissionManager;
            _options = options.Value;
        }

        public async Task<string> GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? "")
            };

            var roleClaims = user.Roles.Select(r => CustomClaims.Role(r.Name));

            var permissions = await _permissionManager.GetPermissions(user.Id);
            var permissionsClaims = permissions.Select(p => CustomClaims.Permission(p.Code));

            claims.AddRange(roleClaims);
            claims.AddRange(permissionsClaims);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                expires: DateTime.Now.AddMinutes(_options.LifetimeInMinutes),
                signingCredentials: credentials,
                claims: claims);

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return stringToken;
        }
    }
}
