using MapsterMapper;
using VKTestBackendTask.Bll.Services.Abstractions;
using VKTestBackendTask.Bll.Services.Implementations;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Tests.Helpers;

public class UserServiceTestsHelper
{
    internal static IUserService CreateMockUserService(
        IUserRepository mockUserRepository,
        Mapper mapper,
        IUserStateRepository mockUserStateRepository,
        IUserGroupRepository mockUserGroupRepository,
        int maxAdminCount)
    {
        return new UserService(
            mockUserRepository,
            mapper,
            mockUserStateRepository,
            AuthServiceTestsHelper.CreateMockAuthService(
                mockUserRepository,
                mockUserStateRepository,
                mockUserGroupRepository,
                maxAdminCount: maxAdminCount
            )
        );
    }
}