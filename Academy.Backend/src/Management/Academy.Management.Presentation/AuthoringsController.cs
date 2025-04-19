using Academy.Framework;
using Academy.Framework.Auth;
using Academy.Management.Application.Authorings.CreateAuthoring;
using Academy.Management.Contracts.Requests;
using Academy.Management.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Academy.Management.Application.Authorings.SubmitAuthoring;
using Academy.Management.Application.Authorings.ApproveAuthoring;
using Academy.Management.Application.Authorings.RejectAuthoring;

namespace Academy.Management.Presentation
{
    public class AuthoringsController : ApplicationController
    {
        [HttpPost]
        [HasPermission(Permissions.Authorings.CreateAuhtoring)]
        public async Task<ActionResult> CreateAuthoring(
            [FromBody] CreateAuthoringRequest request,
            [FromServices] CreateAuthoringCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var userIdString = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if(!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var result = await handler.Handle(request.ToCommand(userId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost("{authoringId:guid}/submit")]
        [HasPermission(Permissions.Authorings.SubmitAuthoring)]
        public async Task<ActionResult> SubmitAuthoring(
            [FromRoute] Guid authoringId,
            [FromServices] SubmitAuthoringCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var userIdString = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var result = await handler.Handle(new SubmitAuthoringCommand(authoringId, userId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpPost("{authoringId:guid}/approve")]
        [HasPermission(Permissions.Authorings.ApproveAuthoring)]
        public async Task<ActionResult> ApproveAuthoring(
            [FromRoute] Guid authoringId,
            [FromServices] ApproveAuthoringCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new ApproveAuthoringCommand(authoringId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }


        [HttpPost("{authoringId:guid}/reject")]
        [HasPermission(Permissions.Authorings.RejectAuthoring)]
        public async Task<ActionResult> RejectAuthoring(
            [FromRoute] Guid authoringId,
            [FromBody] RejectAuthoringRequest request,
            [FromServices] RejectAuthoringCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(authoringId), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }
    }
}
