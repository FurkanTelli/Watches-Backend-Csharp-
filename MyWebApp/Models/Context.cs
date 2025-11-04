using Microsoft.EntityFrameworkCore;

namespace MyWebApp.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Watch> WatchesTable { get; set; }
        public DbSet<User> UsersTable { get; set; }
    }
}
