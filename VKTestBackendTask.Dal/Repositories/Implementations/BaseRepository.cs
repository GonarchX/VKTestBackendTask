using Microsoft.EntityFrameworkCore;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Dal.Repositories.Implementations;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> Entities;
    private readonly DbContext _context;

    protected BaseRepository(ApplicationDbContext context) 
    {
        Entities = context.Set<TEntity>();
        _context = context;
    }

    public virtual async Task<List<TEntity>> GetByPage(int page = 1, int pageSize = 25)
    {
        return await Entities
            .AsNoTracking()
            .OrderBy(x => x)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public virtual async Task<TEntity?> Get(object entityKey)
    {
        var entity = await Entities.FindAsync(entityKey);
        if (entity != null) Entities.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public virtual async Task<TEntity> Add(TEntity entity)
    {
        await Entities.AddAsync(entity);
        return entity;
    }

    public virtual async Task<List<TEntity>> AddRange(List<TEntity> entities)
    {
        await Entities.AddRangeAsync(entities);
        return entities;
    }

    public virtual void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public virtual TEntity Update(TEntity entity)
    {
        Entities.Update(entity);
        return entity;
    }

    public async Task SaveChangesToDb()
    {
        await _context.SaveChangesAsync();
    }
}