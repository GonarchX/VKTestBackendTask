using ErrorOr;
using VKTestBackendTask.Bll.Dto.UserService;

namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IUserService
{
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    Task<ErrorOr<UserDto>> GetUserById(long userId);

    /// <summary>
    /// Получить список пользователей, используя пагинацию
    /// </summary>
    /// <param name="page">Номер страницы (отсчет начинается с 1)</param>
    /// <param name="pageSize">Размер страницы</param>
    Task<List<UserDto>> GetUsersByPage(int page = 1, int pageSize = 25);
}