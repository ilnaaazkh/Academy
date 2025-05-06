using Academy.Accounts.Application.LoginUser;
using Academy.Accounts.Application.RefreshTokens;
using Academy.Accounts.Application.RegisterUser;
using Academy.Accounts.Contracts.Requests;
using Academy.Accounts.Presentation.Extensions;
using Academy.Framework;
using Academy.Framework.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Accounts.Presentation
{
    public class AccountsController : ApplicationController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(
            [FromBody] RegisterUserRequest request,
            [FromServices] RegisterUserCommandHandler handler,
            CancellationToken ct)
        {
            var result = await handler.Handle(request.ToCommand(), ct);

            return result.IsSuccess ? Ok() : result.Error.ToResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(
            [FromBody] LoginUserRequest request,
            [FromServices] LoginUserCommandHandler handler,
            CancellationToken ct)
        {
            var result = await handler.Handle(request.ToCommand(), ct);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            SetRefreshTokenCookie(result.Value.RefreshToken.ToString());

            return  Ok(result.Value);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshTokens(
            [FromServices] RefreshTokensCommandHandler handler,
            CancellationToken ct)
        {
            string? refreshTokenStr = HttpContext.Request.Cookies["refresh_token"];

            if (!Guid.TryParse(refreshTokenStr, out var refreshToken))
            {
                return Unauthorized("Refresh token is required");
            }

            var result = await handler.Handle(new RefreshTokenCommand(refreshToken), ct);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            SetRefreshTokenCookie(result.Value.RefreshToken.ToString());

            return Ok(result.Value);
        }

        private void SetRefreshTokenCookie(string token)
        {
            HttpContext.Response.Cookies.Append(
                "refresh_token",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(30)
                }
            );
        }
    }
}
