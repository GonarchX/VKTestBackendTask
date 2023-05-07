using Moq;
using VKTestBackendTask.Bll.Dto.UserService;
using VKTestBackendTask.Bll.Services.Implementations;
using VKTestBackendTask.Bll.Tests.Helpers;
using VKTestBackendTask.Dal.Common.Errors;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Tests.SUT.Services;

public class UserServiceTests
{
    #region GetUserById

    [Fact]
    public async Task GetUserById_ExistingUser_ReturnUser()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Login = "TestLogin"
        };
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(x => x.GetByIdWithFullInfo(It.IsAny<long>()))
            .ReturnsAsync(user);

        var mapper = CommonTestsHelper.CreateMapper();
        var mockUserStateRepository = new Mock<IUserStateRepository>();
        var mockUserGroupRepository = new Mock<IUserGroupRepository>();
        var userService = UserServiceTestsHelper.CreateMockUserService(
            mockUserRepository.Object,
            mapper,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: 1
        );

        // Act
        var userFromService = await userService.GetUserById(user.Id);

        // Assert
        Assert.Equal(mapper.Map<UserDto>(user), userFromService.Value);
    }

    [Fact]
    public async Task GetUserById_NonExistingUser_ReturnError()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Login = "TestLogin"
        };
        var mockUserRepository = new Mock<IUserRepository>();
        var mapper = CommonTestsHelper.CreateMapper();
        var mockUserStateRepository = new Mock<IUserStateRepository>();
        var mockUserGroupRepository = new Mock<IUserGroupRepository>();

        var userService = new UserService(
            mockUserRepository.Object,
            mapper,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            AuthServiceTestsHelper.CreateMockAuthService(
                mockUserRepository.Object,
                mockUserStateRepository.Object,
                mockUserGroupRepository.Object,
                maxAdminCount: 1
            )
        );

        // Act
        var userFromService = await userService.GetUserById(user.Id);

        // Assert
        Assert.Equal(Errors.User.NotFound, userFromService.FirstError);
    }

    #endregion

    #region GetUsersByPage

    [Fact]
    public async Task GetUsersByPage_NoUsers_ReturnEmptyArray()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(x => x.GetByPageWithFullInfo(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(Enumerable.Empty<User>().ToList());

        var mapper = CommonTestsHelper.CreateMapper();
        var mockUserStateRepository = new Mock<IUserStateRepository>();
        var mockUserGroupRepository = new Mock<IUserGroupRepository>();
        var userService = UserServiceTestsHelper.CreateMockUserService(
            mockUserRepository.Object,
            mapper,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: 1
        );

        // Act
        var usersFromService = await userService.GetUsersByPage();

        // Assert
        Assert.Empty(usersFromService);
    }

    [Theory]
    [InlineData(10, 10)]
    [InlineData(25, 25)]
    [InlineData(5, 25)]
    [InlineData(50, 25)]
    public async Task GetUsersByPage_UsersExist_ReturnArrayWithUsers(int usersCount, int pageSize)
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(x => x.GetByPageWithFullInfo(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(Enumerable.Repeat<User>(null!, usersCount).ToList());

        var mapper = CommonTestsHelper.CreateMapper();
        var mockUserStateRepository = new Mock<IUserStateRepository>();
        var mockUserGroupRepository = new Mock<IUserGroupRepository>();
        var userService = UserServiceTestsHelper.CreateMockUserService(
            mockUserRepository.Object,
            mapper,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: 1
        );

        // Act
        var usersFromService = await userService.GetUsersByPage(page: 1, pageSize);

        // Assert
        Assert.Equal(usersCount % pageSize, usersFromService.Count % pageSize);
    }

    #endregion

    #region BlockUser

    /*
     * Удалить:
     * Пользователь не удаляется из бд, а просто имеет статус "Blocked"
    */

    [Fact]
    public async Task BlockUser_NonExistingUser_ReturnError()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Login = "TestLogin"
        };
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(x => x.Get(It.IsAny<long>()))
            .ReturnsAsync(user);

        var mapper = CommonTestsHelper.CreateMapper();
        var mockUserStateRepository = AuthServiceTestsHelper
            .CreateMockUserStateRepositoryWithSpecifiedGroup(UserStateCode.Blocked.ToString());
        var mockUserGroupRepository = new Mock<IUserGroupRepository>();

        var userService = new UserService(
            mockUserRepository.Object,
            mapper,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            AuthServiceTestsHelper.CreateMockAuthService(
                mockUserRepository.Object,
                mockUserStateRepository.Object,
                mockUserGroupRepository.Object,
                maxAdminCount: 1
            )
        );

        // Act
        var userFromService = await userService.BlockUser(user.Id);

        // Assert
        Assert.Equal(UserStateCode.Blocked.ToString(), userFromService.Value.UserState!.Code);
    }

    #endregion
}