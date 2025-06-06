﻿using Academy.Accounts.Contracts.Responses;
using Academy.Accounts.Infrastructure.Managers;
using Academy.Accounts.Infrastructure.Models;
using Academy.Accounts.Infrastructure.Providers;
using Academy.Core.Abstractions;
using Academy.Core.Extensions;
using Academy.SharedKernel;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;

namespace Academy.Accounts.Application.RefreshTokens
{
    public class RefreshTokensCommandHandler : ICommandHandler<LoginResponse, RefreshTokenCommand>
    {
        private readonly RefreshSessionManager _sessionManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtProvider _tokenProvider;

        public RefreshTokensCommandHandler(
            RefreshSessionManager sessionManager, 
            UserManager<User> userManager,
            JwtProvider tokenProvider)
        {
            _sessionManager = sessionManager;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }

        public async Task<Result<LoginResponse, ErrorList>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken = default)
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

            await _sessionManager.Delete(refreshSession, cancellationToken);

            var tokenResult = await _tokenProvider.GenerateAccessToken(refreshSession.User!, cancellationToken);
            var newRefreshToken = await _tokenProvider.GenerateRefreshToken(refreshSession.User!, tokenResult.Jti, cancellationToken);

            var user = await _userManager.FindByIdAsync(refreshSession.UserId.ToString());
            var roles = await _userManager.GetRolesAsync(user!);

            var result = new LoginResponse(tokenResult.AccessToken, newRefreshToken, roles ?? []);

            return result;
        }
    }
}
