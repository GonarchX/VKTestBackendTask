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

    public async Task<ErrorOr<RegisterResponseDto>> RegisterAsAdmin(RegisterRequestDto registerRequestDto)
    {
        var adminGroup = await _userGroupRepository.GetByCode(UserGroupCode.Admin.ToString());

        var errorsOrUser = await RegisterUser(registerRequestDto.Login, registerRequestDto.Password, adminGroup!);
        if (errorsOrUser.IsError)
            return errorsOrUser.Errors;

        return _mapper.Map<RegisterResponseDto>(errorsOrUser);
    }

    public async Task<ErrorOr<User>> RegisterUser(
        string login,
        string password,
        UserGroup userGroup)
    {
        var isAlreadyExistedUser = await _userRepository.IsAlreadyExistedUser(login);
        if (isAlreadyExistedUser)
            return Errors.Authentication.AlreadyExistedUser;

        if (userGroup.Code == UserGroupCode.Admin.ToString() &&
            await IsAdminAlreadyExist(userGroup))
            return Errors.User.AdminAlreadyExist;

        var userState = await _userStateRepository.GetByCode(UserStateCode.Active.ToString());
        
        var user = new User
        {
            Login = login,
            Password = _passwordHasher.Hash(password),
            CreatedDate = _dateTimeProvider.UtcNow(),
            UserState = userState,
            UserGroup = userGroup
        };

        await _userRepository.Add(user);
        await _userRepository.SaveChangesToDb();

        return user;
    }

    #endregion

    // If we will have problems with performance,
    // we could create another table when we would have information about count of admins in our system
    // in order not to iterate over the entire user table every time
    private async Task<bool> IsAdminAlreadyExist(UserGroup userGroup)
    {
        return await _userRepository.IsExistUserWithSpecifiedGroup(userGroup) != null;
    }

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