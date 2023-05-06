namespace VKTestBackendTask.Api.Contracts.V1.UserController;

public record AddUserRequest(
    string Login,
    string Password,
    string UserGroupCode
);