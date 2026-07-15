using Microsoft.EntityFrameworkCore;

namespace PlatformService.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }
        public DbSet<Platform> Platforms { get; set; }
    }
}
