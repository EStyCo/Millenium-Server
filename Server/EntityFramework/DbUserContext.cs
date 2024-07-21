using Microsoft.EntityFrameworkCore;
using Server.EntityFramework.Configurations;
using Server.EntityFramework.Models;

namespace Server.EntityFramework
{
    public class DbUserContext : DbContext
    {
        public DbUserContext(DbContextOptions<DbUserContext> options) : base(options) { }

        public DbSet<UserEF> Users { get; set; }
        public DbSet<CharacterEF> Characters { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<ItemEF> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new CharacterConfig());
            modelBuilder.ApplyConfiguration(new StatsConfig());
            modelBuilder.ApplyConfiguration(new ItemConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
