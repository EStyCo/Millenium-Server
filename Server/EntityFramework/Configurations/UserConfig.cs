using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.EntityFramework.Models;

namespace Server.EntityFramework.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<UserEF>
    {
        public void Configure(EntityTypeBuilder<UserEF> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();
        }
    }
}
