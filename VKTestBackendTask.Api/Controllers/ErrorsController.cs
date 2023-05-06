using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VKTestBackendTask.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var appStage = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        string responseTittle;

        if (appStage == Environments.Development)
            responseTittle = exception?.Message ?? "An unexpected error occurred.";
        else
            responseTittle = "An unexpected error occurred.";

        return Problem(
            statusCode: (int)HttpStatusCode.BadRequest,
            title: responseTittle);
    }
}