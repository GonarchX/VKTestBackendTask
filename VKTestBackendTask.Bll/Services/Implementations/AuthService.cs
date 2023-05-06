using ErrorOr;
using MapsterMapper;
using VKTestBackendTask.Bll.Common;
using VKTestBackendTask.Bll.Dto.AuthService.Login;
using VKTestBackendTask.Bll.Dto.AuthService.Register;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Dal.Common.Errors;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserStateRepository _userStateRepository;
    private readonly IUserGroupRepository _userGroupRepository;

    public AuthService(
        IMapper mapper,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IDateTimeProvider dateTimeProvider,
        IUserStateRepository userStateRepository,
        IUserGroupRepository userGroupRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _dateTimeProvider = dateTimeProvider;
        _userStateRepository = userStateRepository;
        _userGroupRepository = userGroupRepository;
    }

    #region Register

    public async Task<ErrorOr<RegisterResponseDto>> Register(RegisterRequestDto registerRequestDto)
    {
        var isAlreadyExistedUser = await _userRepository.IsAlreadyExistedUser(registerRequestDto.Login);
        if (isAlreadyExistedUser)
            return Errors.Authentication.AlreadyExistedUser;

        var defaultState = await _userStateRepository.GetByCode(UserStateCode.Active);
        var defaultGroup = await _userGroupRepository.GetByCode(UserGroupCode.User);

        var user = await RegisterUser(registerRequestDto, defaultState!, defaultGroup!);
        await Task.Delay(5000); // Условия тестового задания, будем считать, что в этот промежуток происходит магия

        return _mapper.Map<RegisterResponseDto>(user);
    }

    private async Task<User> RegisterUser(
        RegisterRequestDto registerRequestDto,
        UserState defaultState,
        UserGroup defaultGroup)
    {
        var user = new User
        {
            Login = registerRequestDto.Login,
            Password = _passwordHasher.Hash(registerRequestDto.Password),
            CreatedDate = _dateTimeProvider.UtcNow(),
            UserState = defaultState,
            UserGroup = defaultGroup
        };

        await _userRepository.Add(user);
        await _userRepository.SaveChangesToDb();
        return user;
    }

    #endregion

    public async Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _userRepository.GetByLogin(loginRequestDto.Login);
        if (user == null)
            return Errors.User.NotFound;

        if (!_passwordHasher.IsSamePasswords(user.Password, loginRequestDto.Password))
            return Errors.Authentication.InvalidCredentials;

        string basicCredentials = "Basic " + BasicAuthHelper.EncodeCredentials(
            loginRequestDto.Login,
            loginRequestDto.Password);

        return new LoginResponseDto(basicCredentials);
    }
}