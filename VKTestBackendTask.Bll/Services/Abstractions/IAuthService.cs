using ErrorOr;
using VKTestBackendTask.Bll.Dto.AuthService.Login;
using VKTestBackendTask.Bll.Dto.AuthService.Register;

namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IAuthService
{
    Task<ErrorOr<RegisterResponseDto>> Register(RegisterRequestDto registerRequestDto);
    Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
}