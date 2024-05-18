using Microsoft.EntityFrameworkCore;
using Server.Configurations;
using Server.Models.EntityFramework;

namespace Server.Models
{
    public class DbUserContext(DbContextOptions<DbUserContext> options) : DbContext(options) 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Stats> Stats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new CharacterConfig());
            modelBuilder.ApplyConfiguration(new StatsConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
