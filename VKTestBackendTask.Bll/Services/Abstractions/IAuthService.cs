using ErrorOr;
using VKTestBackendTask.Bll.Dto.AuthService.Login;
using VKTestBackendTask.Bll.Dto.AuthService.Register;
using VKTestBackendTask.Bll.Dto.UserService;
using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IAuthService
{
    Task<ErrorOr<UserDto>> RegisterAsAdmin(RegisterRequestDto registerRequestDto);
    Task<ErrorOr<User>> RegisterUser(string login, string password, string userGroupCode);
    Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
}