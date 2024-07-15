using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.EntityFramework.Models;

namespace Server.EntityFramework.Configurations
{
    public class StatsConfig : IEntityTypeConfiguration<Stats>
    {
        public void Configure(EntityTypeBuilder<Stats> builder)
        {
            builder.HasKey(u => u.CharacterId);

            builder.HasOne(c => c.Character)
                   .WithOne(u => u.Stats)
                   .HasForeignKey<Character>(c => c.Id);
        }
    }
}