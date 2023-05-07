using Microsoft.EntityFrameworkCore;
using VKTestBackendTask.Dal.Models;

namespace VKTestBackendTask.Bll.Tests.Helpers;

public class TestApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<UserState> UserStates { get; set; } = null!;

    public TestApplicationDbContext(DbContextOptions<TestApplicationDbContext> options) : base(options)
    {
    }

    /*public static DbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dbContext = new LicenseDbContext(options);

        return dbContext;
    }*/
}