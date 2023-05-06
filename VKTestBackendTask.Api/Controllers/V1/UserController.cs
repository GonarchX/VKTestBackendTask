using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Bll.Services.Abstractions;

namespace VKTestBackendTask.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ApiController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetById(long userId)
    {
        var user = await _userService.GetById(userId);

        return user.Match(
            result => Ok(result),
            _ => Problem(user.Errors)
        );
    }

    [HttpGet("{page:int}/{pageSize:int}")]
    public async Task<IActionResult> GetById(int page = 1, int pageSize = 25)
    {
        var users = await _userService.GetRange(page, pageSize);
        
        return Ok(users);
    }
}