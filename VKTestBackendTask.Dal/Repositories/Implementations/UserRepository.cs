using Microsoft.EntityFrameworkCore;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Dal.Repositories.Implementations;

internal class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsAlreadyExistedUser(string userLogin)
    {
        var user = await Entities
            .AsNoTracking()
            .Where(u => u.Login == userLogin)
            .FirstOrDefaultAsync();

        return user != null;
    }
}