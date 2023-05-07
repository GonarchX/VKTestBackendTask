using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKTestBackendTask.Api.Contracts.V1.AuthController;
using VKTestBackendTask.Bll.Dto.AuthService.Login;
using VKTestBackendTask.Bll.Dto.AuthService.Register;
using VKTestBackendTask.Bll.Services.Abstractions;

namespace VKTestBackendTask.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[AllowAnonymous]
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

    [HttpPost("registerAsAdmin")]
    public async Task<IActionResult> RegisterAsAdmin(RegisterRequest registerRequest)
    {
        var registerRequestDto = _mapper.Map<RegisterRequestDto>(registerRequest);
        var user = await _authService.RegisterAsAdmin(registerRequestDto);

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