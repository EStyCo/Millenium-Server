using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.EntityFramework.Models;

namespace Server.EntityFramework.Configurations
{
    public class CharacterConfig : IEntityTypeConfiguration<CharacterEF>
    {
        public void Configure(EntityTypeBuilder<CharacterEF> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User)
                   .WithOne(u => u.Character)
                   .HasForeignKey<CharacterEF>(c => c.UserId);
        }
    }
}
