using Moq;
using VKTestBackendTask.Bll.Tests.Helpers;
using VKTestBackendTask.Dal.Common.Errors;
using VKTestBackendTask.Dal.Enums;
using VKTestBackendTask.Dal.Models;
using VKTestBackendTask.Dal.Repositories.Abstractions;

namespace VKTestBackendTask.Bll.Tests.SUT.Services;

public class AuthServiceTests
{
    #region RegisterUser

    [Fact]
    public async Task RegisterUser_AllOk_CreateNewUser()
    {
        // Arrange
        var defaultUserGroupCode = UserGroupCode.User.ToString();
        var (login, password, userGroupCode) = AuthServiceTestsHelper.GetDefaultUserCredentials();

        var mockUserRepository = new Mock<IUserRepository>();
        var mockUserStateRepository = AuthServiceTestsHelper
            .CreateMockUserStateRepositoryWithSpecifiedGroup(userGroupCode);

        var mockUserGroupRepository = AuthServiceTestsHelper
            .CreateMockUserGroupRepositoryWithSpecifiedGroup(defaultUserGroupCode);

        var authService = AuthServiceTestsHelper.CreateMockAuthService(
            mockUserRepository.Object,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: 1);

        // Act
        await authService.RegisterUser(login, password, userGroupCode);

        // Assert
        mockUserRepository.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task RegisterUser_ExistingLogin_ReturnError()
    {
        // Arrange
        var defaultUserGroupCode = UserGroupCode.User.ToString();
        var (login, password, userGroupCode) = AuthServiceTestsHelper.GetDefaultUserCredentials();

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(x => x.IsAlreadyExistedUser(It.Is<string>(s => s == login)))
            .ReturnsAsync(true);

        var mockUserStateRepository = AuthServiceTestsHelper
            .CreateMockUserStateRepositoryWithSpecifiedGroup(userGroupCode);

        var mockUserGroupRepository = AuthServiceTestsHelper
            .CreateMockUserGroupRepositoryWithSpecifiedGroup(defaultUserGroupCode);

        var authService = AuthServiceTestsHelper.CreateMockAuthService(
            mockUserRepository.Object,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: 1);

        // Act
        var errorsOrUser = await authService.RegisterUser(login, password, userGroupCode);

        // Assert
        Assert.Equal(Errors.Auth.AlreadyExistedUser, errorsOrUser.FirstError);
    }

    [Fact]
    public async Task RegisterUser_NonExistentGroup_ReturnError()
    {
        // Arrange
        var (login, password, userGroupCode) = AuthServiceTestsHelper.GetDefaultUserCredentials();

        var mockUserRepository = new Mock<IUserRepository>();
        var mockUserStateRepository = new Mock<IUserStateRepository>();
        var mockUserGroupRepository = new Mock<IUserGroupRepository>();

        var authService = AuthServiceTestsHelper.CreateMockAuthService(
            mockUserRepository.Object,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: 1);

        // Act
        var errorsOrUser = await authService.RegisterUser(login, password, userGroupCode);

        // Assert
        Assert.Equal(Errors.UserGroup.NotFound, errorsOrUser.FirstError);
    }

    [Theory]
    [InlineData(10, 11)]
    [InlineData(0, 1)]
    [InlineData(42, 43)]
    public async Task RegisterUser_OverAdminLimit_ReturnError(int adminCurrentCount, int maxAdminNumber)
    {
        // Arrange
        var (login, password, userGroupCode) = AuthServiceTestsHelper.GetDefaultAdminCredentials();
        var defaultUserStateCode = UserStateCode.Active.ToString();

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(x => x.GetUsersBy(
                It.IsAny<UserGroup>(),
                It.IsAny<UserState>()))
            .ReturnsAsync(Enumerable.Repeat<User>(null!, adminCurrentCount).ToList());

        var mockUserStateRepository = AuthServiceTestsHelper
            .CreateMockUserStateRepositoryWithSpecifiedGroup(defaultUserStateCode);

        var mockUserGroupRepository = AuthServiceTestsHelper
            .CreateMockUserGroupRepositoryWithSpecifiedGroup(userGroupCode);

        var authService = AuthServiceTestsHelper.CreateMockAuthService(
            mockUserRepository.Object,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: maxAdminNumber);

        // Act
        await authService.RegisterUser(login, password, userGroupCode);

        // Assert
        mockUserRepository.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
    }

    [Theory]
    [InlineData(11, 10)]
    [InlineData(1, 0)]
    [InlineData(43, 42)]
    public async Task RegisterUser_LessAdminLimit_CreateNewUser(int adminCurrentCount, int maxAdminNumber)
    {
        // Arrange
        var (login, password, userGroupCode) = AuthServiceTestsHelper.GetDefaultAdminCredentials();
        var adminGroupCode = UserGroupCode.Admin.ToString();
        var defaultUserStateCode = UserStateCode.Active.ToString();

        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository
            .Setup(x => x.GetUsersBy(
                It.IsAny<UserGroup>(),
                It.IsAny<UserState>()))
            .ReturnsAsync(Enumerable.Repeat<User>(null!, adminCurrentCount).ToList());
        var mockUserStateRepository = AuthServiceTestsHelper
            .CreateMockUserStateRepositoryWithSpecifiedGroup(defaultUserStateCode);
        var mockUserGroupRepository = AuthServiceTestsHelper
            .CreateMockUserGroupRepositoryWithSpecifiedGroup(userGroupCode);

        var authService = AuthServiceTestsHelper.CreateMockAuthService(
            mockUserRepository.Object,
            mockUserStateRepository.Object,
            mockUserGroupRepository.Object,
            maxAdminCount: maxAdminNumber);

        // Act
        var errorsOrUser = await authService.RegisterUser(login, password, adminGroupCode);

        // Assert
        Assert.Equal(Errors.User.AdminCountLimitExceeded, errorsOrUser.FirstError);
    }

    #endregion
}