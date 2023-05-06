using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Dal.Repositories.Abstractions;

public interface IUserStateRepository : IBaseRepository<UserState>
{
    Task<UserState?> GetByCode(string userStateCode);
}