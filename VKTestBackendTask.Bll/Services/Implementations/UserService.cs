using ErrorOr;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Dal.Common.Errors;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Services.Implementations;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> GetById(long userId)
    {
        var user = await _userRepository.Get(userId);

        if (user == null)
            return Errors.User.NotFound;
        
        return user;
    }


    public async Task<List<User>> GetRange(int page = 1, int pageSize = 25)
    {
        return await _userRepository.GetByPage(page, pageSize);
    }
}