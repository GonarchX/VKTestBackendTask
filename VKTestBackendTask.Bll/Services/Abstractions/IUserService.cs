using ErrorOr;
using VKTestBackendTask.Bll.Dto.UserService;

namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IUserService
{
    /// <summary>
    /// Get user by identifier
    /// </summary>
    /// <param name="userId">User identifier</param>
    Task<ErrorOr<UserDto>> GetUserById(long userId);

    /// <summary>
    /// Get list of users using pagination
    /// </summary>
    /// <param name="page">Number of page (starts with 1)</param>
    /// <param name="pageSize">Page size </param>
    Task<List<UserDto>> GetUsersByPage(int page = 1, int pageSize = 25);

    /// <summary>
    /// Add user
    /// </summary>
    /// <param name="addUserRequestDto">Information for user creating</param>
    /// <returns>Created user</returns>
    Task<ErrorOr<UserDto>> AddUser(AddUserRequestDto addUserRequestDto);

    /// <summary>
    /// Block user
    /// </summary>
    /// <param name="blockUserRequestDto">User ID to block him</param>
    /// <returns>Blocked user</returns>
    Task<ErrorOr<UserDto>> BlockUser(long userId);
}