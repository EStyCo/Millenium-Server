using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.EntityFramework.Models;

namespace Server.EntityFramework.Configurations
{
    public class StatsConfig : IEntityTypeConfiguration<Stats>
    {
        public void Configure(EntityTypeBuilder<Stats> builder)
        {
            builder.ToTable("Stats");
            builder.HasKey(u => u.Id);

            builder.HasOne(c => c.CharacterEF)
                   .WithOne(u => u.Stats)
                   .HasForeignKey<Stats>(c => c.CharacterEFId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}