namespace VKTestBackendTask.Bll.Dto.UserService;

public record AddUserRequestDto(
    string Login,
    string Password,
    string UserGroupCode
);