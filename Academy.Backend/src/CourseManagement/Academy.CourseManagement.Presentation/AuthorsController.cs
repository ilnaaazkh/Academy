using Academy.CourseManagement.Application.Authors.Create;
using Academy.CourseManagement.Application.Authors.Update.UpdateMainInfo;
using Academy.CourseManagement.Contracts.Requests;
using Academy.CourseManagement.Presentation.Extensions;
using Academy.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Academy.CourseManagement.Presentation
{
    public class AuthorsController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult> CreateAuthor(
            [FromBody] CreateAuthorRequest request,
            [FromServices] CreateAuthorCommandHandler handler,
            CancellationToken cancellationToken = default)
        {
            throw new Exception("Poshel nahuui");
            var result = await handler.Handle(request.ToCommand(), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromBody] UpdateAuthorMainInfo request,
            [FromServices] UpdateMainInfoCommandHandler handler,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(request.ToCommand(id), cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }
    }
}
