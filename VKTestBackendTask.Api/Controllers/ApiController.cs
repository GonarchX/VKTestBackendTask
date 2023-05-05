using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace VKTestBackendTask.Api.Controllers.Common;

public class ApiController : ControllerBase
{
    public IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items["errors"] = errors;
        
        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status422UnprocessableEntity,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}