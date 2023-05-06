using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Bll.Dto.AuthService.Register;

public record RegisterResponseDto(
    string Login,
    DateTime CreatedDate,
    UserGroup UserGroup,
    UserState UserState
);