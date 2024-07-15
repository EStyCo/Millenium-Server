using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Server.EntityFramework.Models;

namespace Server.EntityFramework.Configurations
{
    public class ItemConfig : IEntityTypeConfiguration<ItemEF>
    {
        public void Configure(EntityTypeBuilder<ItemEF> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(c => c.Character)
                   .WithMany(u => u.Items)

                   .HasForeignKey(c => c.CharacterId);

        }
    }
}
