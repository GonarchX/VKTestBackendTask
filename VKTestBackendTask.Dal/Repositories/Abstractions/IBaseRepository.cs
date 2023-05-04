namespace VKTestBackendTask.Dal.Repositories.Abstractions;

public interface IBaseRepository<T>
{
    Task<List<T>> GetByPageAsync(int page = 1, int pageSize = 25);
    Task<T?> GetAsync(object entityKey);
    Task<T> AddAsync(T entity);
    Task<List<T>> AddRangeAsync(List<T> entities);
    void Delete(T entity);
    T Update(T entity);
}