using Microsoft.Extensions.Options;
using Moq;
using VKTestBackendTask.Bll.Options;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Bll.Services.Implementations;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Tests.Helpers;

public static class AuthServiceTestsHelper
{
    internal static (string, string, string) GetDefaultUserCredentials()
    {
        return ("TestLogin", "TestPassword", UserGroupCode.User.ToString());
    }

    internal static (string, string, string) GetDefaultAdminCredentials()
    {
        return ("TestLogin", "TestPassword", UserGroupCode.Admin.ToString());
    }

    /// <summary>
    /// Creates mock for <see cref="IUserGroupRepository"/> which returns true on "GetByCode" method with specified user group code
    /// </summary>
    /// <param name="userGroupCode">User group code for checking</param>
    internal static Mock<IUserGroupRepository> CreateMockUserGroupRepositoryWithSpecifiedGroup(string userGroupCode)
    {
        var userGroup = new UserGroup
        {
            Id = 1,
            Code = userGroupCode
        };

        var mockUserGroupRepository = new Mock<IUserGroupRepository>();

        mockUserGroupRepository
            .Setup(x => x.GetByCode(It.Is<string>(s => s == userGroupCode)))
            .Returns(Task.FromResult(userGroup)!);

        return mockUserGroupRepository;
    }

    /// <summary>
    /// Creates mock for <see cref="IUserStateRepository"/> which returns true on "GetByCode" method with specified user group code
    /// </summary>
    /// <param name="userStateCode">User state code for checking</param>
    internal static Mock<IUserStateRepository> CreateMockUserStateRepositoryWithSpecifiedGroup(string userStateCode)
    {
        var userState = new UserState
        {
            Id = 1,
            Code = userStateCode
        };

        var mockUserStateRepository = new Mock<IUserStateRepository>();

        mockUserStateRepository
            .Setup(x => x.GetByCode(It.Is<string>(s => s == userStateCode)))
            .Returns(Task.FromResult(userState)!);

        return mockUserStateRepository;
    }

    internal static IAuthService CreateMockAuthService(
        IUserRepository userRepository,
        IUserStateRepository userStateRepository,
        IUserGroupRepository userGroupRepository,
        int maxAdminCount)
    {
        var mockPasswordHasher = new Mock<IPasswordHasher>().Object;
        var mockDateTimeProvider = new Mock<IDateTimeProvider>().Object;
        var mockApplicationOptions = new Mock<IOptionsSnapshot<ApplicationSettings>>();
        mockApplicationOptions
            .Setup(o => o.Value)
            .Returns(new ApplicationSettings { MaxAdminsCount = maxAdminCount });

        var mockAuthService = new AuthService(
            CommonTestsHelper.CreateMapper(),
            userRepository,
            mockPasswordHasher,
            mockDateTimeProvider,
            userStateRepository,
            userGroupRepository,
            mockApplicationOptions.Object
        );

        return mockAuthService;
    }
}