using Academy.Accounts.Application.LoginUser;
using Academy.Accounts.Application.RegisterUser;
using Academy.Accounts.Contracts.Requests;
using Academy.Accounts.Presentation.Extensions;
using Academy.Framework;
using Academy.Framework.Auth;
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

            return result.IsSuccess ? Ok(result.Value) : result.Error.ToResponse();
        }

        [HasPermission("delete.course")]
        [HttpGet("test")]
        public ActionResult Test()
        {
            var ctx = HttpContext.User.Claims;
            return Ok("dscsdcsd");
        }
    }
}
