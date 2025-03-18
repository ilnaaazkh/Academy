using Academy.FilesService.Services;
using Academy.Framework;
using Academy.Framework.Processors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.FilesService.Presentation
{
    public class FilesController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult> UploadFile(
            IFormFileCollection files,
            [FromServices] Services.FilesService filesService,
            CancellationToken cancellationToken)
        {
            await using var formFileProcessor = new FormFileProcessor();

            var processedFiles = formFileProcessor.Process(files);

            var result = await filesService.UploadFiles(processedFiles, "qqqq", cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
