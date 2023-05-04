using Microsoft.EntityFrameworkCore;

namespace VKTestBackendTask.Dal.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}