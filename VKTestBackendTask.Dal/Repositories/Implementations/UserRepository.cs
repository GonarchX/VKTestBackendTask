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
        return await GetByLogin(userLogin) != null;
    }

    public async Task<User?> GetByLogin(string userLogin)
    {
        var user = await Entities
            .AsNoTracking()
            .Where(u => u.Login == userLogin)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<User?> GetByIdWithFullInfo(long userId)
    {
        var user = await Entities
            .AsNoTracking()
            .Include(u => u.UserGroup)
            .Include(u => u.UserState)
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<List<User>> GetByPageWithFullInfo(int page, int pageSize)
    {
        return await Entities
            .AsNoTracking()
            .Include(u => u.UserGroup)
            .Include(u => u.UserState)
            .OrderBy(x => x)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<User>> GetActiveAdminsByGroup(
        UserGroup userGroup,
        UserState activeUserState)
    {
        return await Entities
            .AsNoTracking()
            .Where(u => u.UserGroupId == userGroup.Id &&
                        u.UserState!.Id == activeUserState.Id)
            .ToListAsync();
    }

    public async Task<User?> GetFullInfoByLogin(string userLogin)
    {
        var user = await Entities
            .AsNoTracking()
            .Include(u=>u.UserGroup)
            .Include(u=>u.UserState)
            .Where(u => u.Login == userLogin)
            .FirstOrDefaultAsync();

        return user;
    }
}