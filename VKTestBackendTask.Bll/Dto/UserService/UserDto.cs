using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Bll.Dto.UserService;

public record UserDto(
    long Id,
    string Login,
    DateTime CreatedDate,
    UserGroup? UserGroup,
    UserState? UserState
);