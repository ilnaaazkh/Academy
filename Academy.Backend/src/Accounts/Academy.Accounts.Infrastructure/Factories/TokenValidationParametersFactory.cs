using Academy.Accounts.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Academy.Accounts.Infrastructure.Factories
{
    public static class TokenValidationParametersFactory
    {
        public static TokenValidationParameters Create(JwtOptions jwtOptions, bool validateLifetime = true)
        {
            return new TokenValidationParameters()
            {
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = validateLifetime,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
            };
        }
    }
}
