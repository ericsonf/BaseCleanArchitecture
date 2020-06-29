using BaseCleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseCleanArchitecture.Infra.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public DbSet<User> User { get; set; }
    }
}
