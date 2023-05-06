namespace VKTestBackendTask.Dal.Repositories.Abstractions;

public interface IBaseRepository<T>
{
    Task<List<T>> GetByPage(int page = 1, int pageSize = 25);
    Task<T?> Get(object entityKey);
    Task<T> Add(T entity);
    Task<List<T>> AddRange(List<T> entities);
    void Delete(T entity);
    T Update(T entity);
}