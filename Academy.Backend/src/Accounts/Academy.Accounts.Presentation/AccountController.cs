using Academy.Accounts.Application.RegisterUser;
using Academy.Accounts.Contracts.Requests;
using Academy.Accounts.Presentation.Extensions;
using Academy.Framework;
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
    }
}
