using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/develop")]
public class InitAppController : ControllerBase
{
    private readonly IUserStateRepository _userStateRepository;
    private readonly IUserGroupRepository _userGroupRepository;

    public InitAppController(
        IUserStateRepository userStateRepository,
        IUserGroupRepository userGroupRepository)
    {
        _userStateRepository = userStateRepository;
        _userGroupRepository = userGroupRepository;
    }


    /// <summary>
    /// Use this to fill database with initial data
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FillDbWithDefaultValues()
    {
        await _userGroupRepository.AddRange(new List<UserGroup>
        {
            new() { Code = UserGroupCode.Admin.ToString() },
            new() { Code = UserGroupCode.User.ToString() }
        });

        await _userStateRepository.AddRange(new List<UserState>
        {
            new() { Code = UserStateCode.Active.ToString() },
            new() { Code = UserStateCode.Blocked.ToString() }
        });

        await _userGroupRepository.SaveChangesToDb();

        return Ok();
    }
}