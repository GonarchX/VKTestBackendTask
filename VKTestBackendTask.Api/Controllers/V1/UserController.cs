using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Api.Contracts.V1.UserController;
using VKTestBackendTask.Bll.Dto.UserService;
using VKTestBackendTask.Bll.Services.Abstractions;

namespace VKTestBackendTask.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ApiController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;;
        _mapper = mapper;
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetUserById(long userId)
    {
        var user = await _userService.GetUserById(userId);

        return user.Match(
            result => Ok(result),
            _ => Problem(user.Errors)
        );
    }

    [HttpGet("{page:int}/{pageSize:int}")]
    public async Task<IActionResult> GetUsersByPage(int page = 1, int pageSize = 25)
    {
        var users = await _userService.GetUsersByPage(page, pageSize);

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
    {
        var addUserRequestDto = _mapper.Map<AddUserRequestDto>(addUserRequest);
        var user = await _userService.AddUser(addUserRequestDto);

        return user.Match(
            result => Ok(result),
            _ => Problem(user.Errors)
        );
    }
    
    [HttpDelete("{userId:long}")]
    public async Task<IActionResult> BlockUser(long userId)
    {
        var user = await _userService.BlockUser(userId);

        return user.Match(
            result => Ok(result),
            _ => Problem(user.Errors)
        );
    }
}