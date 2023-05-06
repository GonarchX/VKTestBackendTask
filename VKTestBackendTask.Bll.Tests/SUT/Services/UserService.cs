namespace VKTestBackendTask.Bll.Tests.SUT.Services;

public class UserService
{
    #region AddUser

    /*
     * Добавить:
     * Можно создать
     * Логин занят
     * Админов больше лимита
     * Поменять лимит админов и проверить логин
     * Несуществующая группа
     * Зарегаться как админ и попробовать снова зарегать админа
     * Зарегаться как админ, заблокировать себя и попробовать снова зарегать админа
     * Добавить пользователя с несуществующей группой
     * Добавить пользователя с несуществующим состоянием
     */

    #endregion

    #region GetUserById

    /*
     * Получить одного:
     * Несуществующий идентификатор
     * 
    */
    
    #endregion

    #region GetUserFullInfoByLogin

    #endregion

    #region GetUsersByPage

    /*
     * Получить страницу:
     */
    
    #endregion
    
    #region BlockUser

    /*
     * Удалить:
     * Пользователь не удаляется из бд, а просто имеет статус "Blocked"
    */
    
    #endregion   
}