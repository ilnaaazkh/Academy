using Academy.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Academy.Framework
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        public Guid UserId
        {
            get
            {
                var userIdString = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if(!Guid.TryParse(userIdString, out var userId))
                {
                    throw new UnauthorizedAccessException("User ID is invalid or missing");
                }

                return userId;
            }
        }

        public override OkObjectResult Ok(object? value)
        {
            var envelope = Envelope.Ok(value);
            return new OkObjectResult(envelope);
        }
    }
}
