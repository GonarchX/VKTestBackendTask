using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Dal.Repositories.Abstractions;

public interface IUserStateRepository : IBaseRepository<UserState>
{
    Task<UserState?> GetByCode(UserStateCode userStateCode);
}