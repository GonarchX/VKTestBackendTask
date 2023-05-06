using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Dal.Repositories.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> IsAlreadyExistedUser(string userLogin);
    Task<User?> GetByLogin(string userLogin);
    Task<User?> GetByIdWithFullInfo(long userId);
    Task<List<User>> GetByPageWithFullInfo(int page, int pageSize);
}