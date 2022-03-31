using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class PlatformTypeConfiguration : IEntityTypeConfiguration<PlatformType>
    {
        public void Configure(EntityTypeBuilder<PlatformType> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(c => c.Type)
                .IsRequired();

            builder.HasIndex(g => g.Type).IsUnique();
        }
    }
}
