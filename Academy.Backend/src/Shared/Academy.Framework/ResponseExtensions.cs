using Academy.Core.Models;
using Academy.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Framework
{
    public static class ResponseExtensions
    {
        public static ObjectResult ToResponse(this ErrorList errors)
        {
            if (!errors.Any())
            {
                return new ObjectResult(null)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            var errorTypes = errors.Select(error => error.Type).Distinct().ToList();


            var statusCode = errorTypes.Count() > 1
                ? StatusCodes.Status500InternalServerError
                : GetStatusCodeForErrorType(errorTypes.First());

            var envelope = Envelope.Error(errors);

            return new ObjectResult(envelope) { StatusCode = statusCode };
        }

        private static int GetStatusCodeForErrorType(ErrorType errorType)
        {
            var statusCode = errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return statusCode;
        }
    }
}
