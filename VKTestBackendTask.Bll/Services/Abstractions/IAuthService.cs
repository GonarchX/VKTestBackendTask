using ErrorOr;
using VKTestBackendTask.Bll.Dto.AuthService.Login;
using VKTestBackendTask.Bll.Dto.AuthService.Register;
using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IAuthService
{
    Task<ErrorOr<RegisterResponseDto>> RegisterAsAdmin(RegisterRequestDto registerRequestDto);
    Task<ErrorOr<User>> RegisterUser(string login, string password, UserGroup userGroup);
    Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
}