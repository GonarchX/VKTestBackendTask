using Microsoft.EntityFrameworkCore;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Dal.Repositories.Implementations;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _entities;

    protected BaseRepository(ApplicationDbContext context)
    {
        _entities = context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetByPageAsync(int page = 1, int pageSize = 25)
    {
        return await _entities
            .AsNoTracking()
            .OrderBy(x => x)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(object entityKey)
    {
        var entity = await _entities.FindAsync(entityKey);
        if (entity != null) _entities.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
        return entity;
    }

    public virtual async Task<List<TEntity>> AddRangeAsync(List<TEntity> entities)
    {
        await _entities.AddRangeAsync(entities);
        return entities;
    }

    public virtual void Delete(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public virtual TEntity Update(TEntity entity)
    {
        _entities.Update(entity);
        return entity;
    }
}