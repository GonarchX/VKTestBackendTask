using System.Net;
using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Api.Contracts.V1.UserController;
using VKTestBackendTask.Bll.Dto.UserService;
using VKTestBackendTask.Bll.Services.Abstractions;

namespace VKTestBackendTask.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]
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

    /// <summary>
    /// Find user by specified ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <response code="404">
    /// Can't find user with provided ID: <br/>
    /// Code: **User.NotFound** <br/>
    /// </response>
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetUserById(long userId)
    {
        var user = await _userService.GetUserById(userId);

        return user.Match(
            result => Ok(result),
            _ => Problem(user.Errors)
        );
    }

    /// <summary>
    /// Return list of users on specified page
    /// </summary>
    /// <param name="page">Number of page (count starts from 1)</param>
    /// <param name="pageSize">Page size</param>
    [ProducesResponseType(typeof(List<UserDto>), (int)HttpStatusCode.OK)]
    [HttpGet("{page:int}/{pageSize:int}")]
    public async Task<IActionResult> GetUsersByPage(int page = 1, int pageSize = 25)
    {
        var users = await _userService.GetUsersByPage(page, pageSize);

        return Ok(users);
    }

    /// <summary>
    /// Register new user with provided credentials
    /// </summary>
    /// <param name="addUserRequest">Credentials to register new user</param>
    /// <response code="409">
    /// User with specified login already exists: <br/>
    /// Code: **Auth.AlreadyExistedUser** <br/>
    /// </response>
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
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
    
    /// <summary>
    /// Block user by specified ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <response code="404">
    /// User with specified ID doesn't exist: <br/>
    /// Code: **User.NotFound** <br/>
    /// </response>
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
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