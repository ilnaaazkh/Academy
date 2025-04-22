using Academy.Accounts.Infrastructure.Factories;
using Academy.Accounts.Infrastructure.Managers;
using Academy.Accounts.Infrastructure.Models;
using Academy.Accounts.Infrastructure.Options;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
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
        private readonly RefreshSessionManager _refreshSessionManager;
        private readonly JwtOptions _options;

        public JwtProvider(
            PermissionManager permissionManager,
            RefreshSessionManager refreshSessionManager,
            IOptions<JwtOptions> options)
        {
            _permissionManager = permissionManager;
            _refreshSessionManager = refreshSessionManager;
            _options = options.Value;
        }

        public async Task<Guid> GenerateRefreshToken(User user, Guid accessTokenJti, CancellationToken ct)
        {
            var refreshToken = Guid.NewGuid();

            var refreshSession = new RefreshSession() 
            { 
                Id = Guid.NewGuid(),
                User = user,
                CreatedAt = DateTime.UtcNow,
                Jti = accessTokenJti,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                RefreshToken = refreshToken
            };

            await _refreshSessionManager.CreateRefreshSession(refreshSession, ct);

            return refreshToken;
        }

        public async Task<JwtTokenResult> GenerateAccessToken(User user, CancellationToken ct)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jti = Guid.NewGuid();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, jti.ToString())
            };

            var roleClaims = user.Roles.Select(r => CustomClaims.Role(r.Name));

            var permissions = await _permissionManager.GetPermissions(user.Id, ct);
            var permissionsClaims = permissions.Select(p => CustomClaims.Permission(p.Code));

            claims.AddRange(roleClaims);
            claims.AddRange(permissionsClaims);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                expires: DateTime.Now.AddMinutes(_options.LifetimeInMinutes),
                signingCredentials: credentials,
                claims: claims);

            var jwtStringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtTokenResult(jwtStringToken, jti);
        }

        public async Task<Result<IReadOnlyList<Claim>, Error>> GetUserClaims(string jwtToken, CancellationToken ct)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var validationParameter = TokenValidationParametersFactory.Create(_options, false);
            
            var validationResult = await jwtHandler.ValidateTokenAsync(jwtToken, validationParameter);
            if (validationResult.IsValid == false)
            {
                return Errors.Tokens.InvalidToken();
            }

            return validationResult.ClaimsIdentity.Claims.ToList();
        }
    }

    public record JwtTokenResult(string AccessToken, Guid Jti);
}
