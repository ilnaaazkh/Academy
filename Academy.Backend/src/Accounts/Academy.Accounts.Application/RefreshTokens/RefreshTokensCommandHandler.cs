using Academy.Accounts.Contracts.Responses;
using Academy.Accounts.Infrastructure.Managers;
using Academy.Accounts.Infrastructure.Models;
using Academy.Accounts.Infrastructure.Providers;
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Academy.Accounts.Application.RefreshTokens
{
    public class RefreshTokensCommandHandler : ICommandHandler<LoginResponse, RefreshTokensCommand>
    {
        private readonly RefreshSessionManager _sessionManager;
        private readonly JwtProvider _tokenProvider;

        public RefreshTokensCommandHandler(
            RefreshSessionManager sessionManager, 
            JwtProvider tokenProvider)
        {
            _sessionManager = sessionManager;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<LoginResponse, ErrorList>> Handle(RefreshTokensCommand command, CancellationToken cancellationToken = default)
        {
            var refreshSession = await _sessionManager.GetByRefreshSession(command.RefreshToken, cancellationToken);

            if (refreshSession is null)
            {
                return Errors.General.NotFound(command.RefreshToken).ToErrorList();
            }

            if(refreshSession.ExpiresAt <= DateTime.UtcNow)
            {
                return Errors.Tokens.ExpiredToken().ToErrorList();
            }

            var claimsResult = await _tokenProvider.GetUserClaims(command.AccessToken, cancellationToken); 

            if(claimsResult.IsFailure)
                return Errors.Tokens.InvalidToken().ToErrorList();

            var userIdString = claimsResult.Value.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if(!Guid.TryParse(userIdString, out var userId))
                return Errors.General.Failure().ToErrorList();

            if(refreshSession.UserId != userId)
                return Errors.Tokens.InvalidToken().ToErrorList();

            var userJtiString = claimsResult.Value.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            if(!Guid.TryParse(userJtiString, out var jti))
            {
                return Errors.Tokens.InvalidToken().ToErrorList();
            }

            if(refreshSession.Jti != jti)
            {
                return Errors.Tokens.InvalidToken().ToErrorList();
            }

            await _sessionManager.Delete(refreshSession, cancellationToken);

            var tokenResult = await _tokenProvider.GenerateAccessToken(refreshSession.User!, cancellationToken);
            var newRefreshToken = await _tokenProvider.GenerateRefreshToken(refreshSession.User!, tokenResult.Jti, cancellationToken);

            var result = new LoginResponse(tokenResult.AccessToken, newRefreshToken);

            return result;
        }
    }
}
