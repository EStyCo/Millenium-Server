using Microsoft.EntityFrameworkCore;
using Server.Configurations;

namespace Server.Models
{
    public class DbUserContext(DbContextOptions<DbUserContext> options) : DbContext(options) 
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
