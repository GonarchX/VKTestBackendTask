using ErrorOr;
using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IUserService
{
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    Task<ErrorOr<User>> GetById(long userId);

    /// <summary>
    /// Получить список пользователей, используя пагинацию
    /// </summary>
    /// <param name="page">Номер страницы (отсчет начинается с 1)</param>
    /// <param name="pageSize">Размер страницы</param>
    Task<List<User>> GetRange(int page = 1, int pageSize = 25);
}