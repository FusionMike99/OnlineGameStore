using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class PlatformTypeConfiguration : IEntityTypeConfiguration<PlatformTypeEntity>
    {
        public void Configure(EntityTypeBuilder<PlatformTypeEntity> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(pt => pt.Type)
                .IsRequired();

            builder.Property(pt => pt.IsDeleted)
                .HasDefaultValue(false);

            builder.HasIndex(pt => pt.Type).IsUnique();
        }
    }
}