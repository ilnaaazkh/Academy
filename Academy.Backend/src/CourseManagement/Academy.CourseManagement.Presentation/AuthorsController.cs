using Academy.CourseManagement.Application.Authors.Create;
using Academy.CourseManagement.Contracts.Requests;
using Academy.CourseManagement.Presentation.Extensions;
using Academy.Framework;
using Microsoft.AspNetCore.Mvc;

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
            var result = await handler.Handle(request.ToCommand(), cancellationToken);

            if (result.IsFailure)
            {
                result.Error.ToResponse();
            }

            return Ok(result.Value);
        }
    }
}
