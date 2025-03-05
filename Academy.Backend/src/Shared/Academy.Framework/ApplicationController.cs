using Academy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Framework
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        public override OkObjectResult Ok(object? value)
        {
            var envelope = Envelope.Ok(value);
            return new OkObjectResult(envelope);
        }
    }
}
