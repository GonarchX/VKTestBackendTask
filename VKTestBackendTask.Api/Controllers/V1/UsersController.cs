using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Api.Controllers.Common;
using VKTestBackendTask.Bll.Services.Abstractions;

namespace VKTestBackendTask.Api.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users/{userId:int}")]
    public async Task<IActionResult> GetById(long userId)
    {
        var user = await _userService.GetById(userId);

        return user.Match(
            result => Ok(result),
            _ => Problem(user.Errors)
        );
    }

    [HttpGet("users/{page:int}/{pageSize:int}")]
    public async Task<IActionResult> GetById(int page = 1, int pageSize = 25)
    {
        var users = await _userService.GetRange(page, pageSize);
        
        return Ok(users);
    }
}