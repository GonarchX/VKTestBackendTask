using ErrorOr;
using MapsterMapper;
using VKTestBackendTask.Bll.Dto.UserService;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Dal.Common.Errors;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Services.Implementations;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserStateRepository _userStateRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IAuthService _authService;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IUserStateRepository userStateRepository,
        IUserGroupRepository userGroupRepository,
        IAuthService authService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userStateRepository = userStateRepository;
        _userGroupRepository = userGroupRepository;
        _authService = authService;
    }

    public async Task<ErrorOr<UserDto>> GetUserById(long userId)
    {
        var user = await _userRepository.GetByIdWithFullInfo(userId);

        if (user == null)
            return Errors.User.NotFound;

        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetUsersByPage(int page = 1, int pageSize = 25)
    {
        return _mapper.Map<List<UserDto>>(await _userRepository.GetByPageWithFullInfo(page, pageSize));
    }

    public async Task<ErrorOr<UserDto>> AddUser(AddUserRequestDto addUserRequestDto)
    {
        var userGroup = await _userGroupRepository.GetByCode(addUserRequestDto.UserGroupCode);
        if (userGroup == null)
            return Errors.UserGroup.NotFound;

        var errorsOrUser = await _authService.RegisterUser(
            addUserRequestDto.Login,
            addUserRequestDto.Password,
            userGroup);
        if (errorsOrUser.IsError)
            return errorsOrUser.Errors;

        await Task.Delay(5000); // Условия тестового задания, будем считать, что в этот промежуток происходит магия

        return _mapper.Map<UserDto>(errorsOrUser.Value);
    }

    public async Task<ErrorOr<UserDto>> BlockUser(BlockUserRequestDto blockUserRequestDto)
    {
        var user = await _userRepository.GetByLogin(blockUserRequestDto.Login);
        if (user == null)
            return Errors.User.NotFound;

        user.UserState = await _userStateRepository.GetByCode(UserStateCode.Blocked.ToString());

        _userRepository.Update(user);
        await _userRepository.SaveChangesToDb();

        return _mapper.Map<UserDto>(user);
    }
}