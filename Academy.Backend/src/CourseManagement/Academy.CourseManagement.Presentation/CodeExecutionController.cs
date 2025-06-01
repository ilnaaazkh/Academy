using Academy.CourseManagement.Application.Courses.CodeExecution;
using Academy.CourseManagement.Contracts.Requests;
using Academy.CourseManagement.Presentation.Extensions;
using Academy.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Academy.CourseManagement.Presentation
{
    [Route("/api")]
    public class CodeExecutionController : ApplicationController
    {
        [HttpPost("execute")]
        public async Task<ActionResult> Execute(
            [FromBody] RunCodeRequest request,
            [FromServices] RunCodeCommandHandler handler, 
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(), cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
