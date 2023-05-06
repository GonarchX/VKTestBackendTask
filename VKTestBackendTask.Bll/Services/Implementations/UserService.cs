using ErrorOr;
using MapsterMapper;
using VKTestBackendTask.Bll.Dto.UserService;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Dal.Common.Errors;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Services.Implementations;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
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
}