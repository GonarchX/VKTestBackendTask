using Microsoft.EntityFrameworkCore;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Dal.Repositories.Implementations;

internal class UserStateRepository : BaseRepository<UserState>, IUserStateRepository
{
    public UserStateRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserState?> GetByCode(string userStateCode)
    {
        return await Entities
            .Where(ug => ug.Code == userStateCode)
            .FirstOrDefaultAsync();
    }
}