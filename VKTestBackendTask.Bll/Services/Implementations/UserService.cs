using ErrorOr;
using MapsterMapper;
using VKTestBackendTask.Bll.Dto.UserService;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Dal.Common.Errors;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Services.Implementations;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserStateRepository _userStateRepository;
    private readonly IAuthService _authService;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IUserStateRepository userStateRepository,
        IAuthService authService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userStateRepository = userStateRepository;
        _authService = authService;
    }

    public async Task<ErrorOr<UserDto>> GetUserById(long userId)
    {
        var user = await _userRepository.GetByIdWithFullInfo(userId);

        if (user == null)
            return Errors.User.NotFound;

        return _mapper.Map<UserDto>(user);
    }

    public async Task<ErrorOr<User>> GetUserFullInfoByLogin(string userLogin)
    {
        var user = await _userRepository.GetFullInfoByLogin(userLogin);

        if (user == null)
            return Errors.User.NotFound;

        return user;
    }

    public async Task<List<UserDto>> GetUsersByPage(int page = 1, int pageSize = 25)
    {
        var usersByPage = await _userRepository.GetByPageWithFullInfo(page, pageSize);

        return _mapper.Map<List<UserDto>>(usersByPage);
    }

    public async Task<ErrorOr<UserDto>> AddUser(AddUserRequestDto addUserRequestDto)
    {
        var errorsOrUser = await _authService.RegisterUser(
            addUserRequestDto.Login,
            addUserRequestDto.Password,
            addUserRequestDto.UserGroupCode);
        if (errorsOrUser.IsError)
            return errorsOrUser.Errors;

        await Task.Delay(5000); // Условия тестового задания, будем считать, что в этот промежуток происходит магия

        return _mapper.Map<UserDto>(errorsOrUser.Value);
    }

    public async Task<ErrorOr<UserDto>> BlockUser(long userId)
    {
        var user = await _userRepository.Get(userId);
        if (user == null)
            return Errors.User.NotFound;

        user.UserState = await _userStateRepository.GetByCode(UserStateCode.Blocked.ToString());

        _userRepository.Update(user);
        await _userRepository.SaveChangesToDb();

        return _mapper.Map<UserDto>(user);
    }
}