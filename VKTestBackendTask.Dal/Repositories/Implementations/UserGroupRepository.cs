using Microsoft.EntityFrameworkCore;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Dal.Repositories.Implementations;

internal class UserGroupRepository : BaseRepository<UserGroup>, IUserGroupRepository
{
    public UserGroupRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserGroup?> GetByCode(UserGroupCode userGroupCode)
    {
        return await Entities
            .Where(ug => ug.Code == userGroupCode.ToString())
            .FirstOrDefaultAsync();
    }
}