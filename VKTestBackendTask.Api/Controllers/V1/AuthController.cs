using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Api.Contracts.V1.AuthController.Login;
using VKTestBackendTask.Api.Contracts.V1.AuthController.Register;
using VKTestBackendTask.Bll.Dto.AuthService.Login;
using VKTestBackendTask.Bll.Dto.AuthService.Register;
using VKTestBackendTask.Bll.Services.Abstractions;

namespace VKTestBackendTask.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ApiController
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var registerRequestDto = _mapper.Map<RegisterRequestDto>(registerRequest);
        var user = await _authService.Register(registerRequestDto);

        return user.Match(
            result => Ok(result),
            _ => Problem(user.Errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var loginRequestDto = _mapper.Map<LoginRequestDto>(loginRequest);
        var user = await _authService.Login(loginRequestDto);

        return user.Match(
            result => Ok(result.BasicCredentials),
            _ => Problem(user.Errors)
        );
    }
}