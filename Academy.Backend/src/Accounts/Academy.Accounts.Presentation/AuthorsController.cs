using Academy.Accounts.Application.DeleteAuthor;
using Academy.Accounts.Application.GetAuthors;
using Academy.Accounts.Application.RegisterAuthor;
using Academy.Accounts.Contracts.Requests;
using Academy.Accounts.Presentation.Extensions;
using Academy.Framework;
using Academy.Framework.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Accounts.Presentation
{
    public class AuthorsController : ApplicationController
    {
        [HttpPost]
        [HasPermission(Permissions.Authors.Create)]
        public async Task<ActionResult> RegisterAuthor(
            [FromBody] RegisterAuthorRequest request,
            [FromServices] RegisterAuthorCommandHandler handler,
            CancellationToken ct)
        {
            var result = await handler.Handle(request.ToCommand(), ct);

            return result.IsSuccess ? Ok() : result.Error.ToResponse();
        }

        [HttpGet]
        [HasPermission(Permissions.Authors.Read)]
        public async Task<ActionResult> GetAuthors(
            [FromQuery] string? search,
            [FromServices] GetAuthorsQueryHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new GetAuthorsQuery(search), cancellationToken);

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        [HasPermission(Permissions.Authors.Read)]
        public async Task<ActionResult> DeleteFromAuthor(
            [FromRoute] Guid id,
            [FromServices] DeleteAuthorCommandHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(new DeleteAuthorCommand(id), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }
    }
}
