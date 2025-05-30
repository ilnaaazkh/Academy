using Academy.Framework;
using Academy.Framework.Auth;
using Academy.Management.Contracts.Requests;
using Academy.Management.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Academy.Framework.Processors;
using Microsoft.AspNetCore.Authorization;
using Academy.Management.Application.Authorings.Command.ApproveAuthoring;
using Academy.Management.Application.Authorings.Command.AddFilesToAuthoring;
using Academy.Management.Application.Authorings.Command.AttachmentDownloadLink;
using Academy.Management.Application.Authorings.Command.CreateAuthoring;
using Academy.Management.Application.Authorings.Command.SubmitAuthoring;
using Academy.Management.Application.Authorings.Command.RejectAuthoring;
using Academy.Management.Application.Authorings.Query.GetAuthoringsQuery;
using Newtonsoft.Json;
using Academy.Management.Application.Authorings.Query.GetMyAuthorings;
using Academy.Management.Application.Authorings.Query.GetAuthoringQuery;

namespace Academy.Management.Presentation
{
    public class AuthoringsController : ApplicationController
    {
        [HttpPost]
        [HasPermission(Permissions.Authorings.CreateAuthoring)]
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

        [HttpPost("{authoringId:guid}/files")]
        [HasPermission(Permissions.Authorings.CreateAuthoring)]
        public async Task<ActionResult> AddAttachmentsToAuthoring(
            [FromForm] IFormFileCollection files,
            [FromRoute] Guid authoringId,
            [FromServices] AddAttachemntsToAuthoringCommandHandler handler,
            CancellationToken cancellationToken)
        {
            await using var formFileProcessor = new FormFileProcessor();
            var processedFiles = formFileProcessor.Process(files);

            var command = new AddAttachemntsToAuthoringCommand(authoringId, processedFiles);
            var result = await handler.Handle(command, cancellationToken);

            if(result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpGet("{authoringId:guid}/files/{fileId}")]
        [Authorize]
        public async Task<ActionResult> AddAttachmentsToAuthoring(
            [FromRoute] string fileId,
            [FromRoute] Guid authoringId,
            [FromServices] GetAttachmentDownloadLinkCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var command = new GetAttachmentDownloadLinkCommand(fileId);
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpGet]
        [HasPermission(Permissions.Authorings.ApproveAuthoring)]
        public async Task<ActionResult> GetAuthorings(
            [FromQuery] GetAuthoringsRequest query,
            [FromServices] GetAuthoringsQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var handleResult = await handler.Handle(query.ToQuery(), cancellationToken);

            var result = handleResult.Value;

            var metadata = new
            {
                result.TotalCount,
                result.PageSize,
                result.CurrentPage,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious
            };

            Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);

            return Ok(result);
        }

        [HttpGet("my-authorings")]
        [Authorize]
        public async Task<ActionResult> MyAuthorings(
            [FromServices] GetMyAuthoringsQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetMyAuthoringsQuery(UserId), cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> MyAuthoring(
            [FromRoute] Guid id,
            [FromServices] GetAuthoringQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetAuthoringQuery(id), cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}
