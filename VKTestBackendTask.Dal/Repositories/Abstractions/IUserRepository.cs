using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Dal.Repositories.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> IsAlreadyExistedUser(string userLogin);
}