using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Api.Controllers.V1;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("users")]
    public async Task<User> GetById(long userId)
    {
        var searchResult = await _userService.GetById(userId); 
        
        return  searchResult.Match(
            searchResult => Ok(searchResult),
            )
            );
    }

    [HttpGet("users/{page:int}/{pageSize:int}")]
    public async Task<List<User>> GetById(int page = 1, int pageSize = 25)
    {
        return await _userService.GetRange(page, pageSize);
    }
}