using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Dal.Repositories.Abstractions;

public interface IUserGroupRepository : IBaseRepository<UserGroup>
{
    Task<UserGroup?> GetByCode(UserGroupCode userGroupCode);
}