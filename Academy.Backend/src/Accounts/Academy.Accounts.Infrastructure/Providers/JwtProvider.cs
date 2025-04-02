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
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("Permission", "create.course"),
                new Claim("Permission", "update.course"),
                new Claim("Permission", "delete.course")
            };

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
