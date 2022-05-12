using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(g => g.CompanyName)
                .HasColumnType("nvarchar(40)")
                .IsRequired();

            builder.Property(g => g.Description)
                .HasColumnType("ntext")
                .IsRequired();

            builder.Property(g => g.HomePage)
                .HasColumnType("ntext")
                .IsRequired();

            builder.HasMany(c => c.Games)
                .WithOne(g => g.Publisher)
                .HasForeignKey(g => g.PublisherId);

            builder.HasIndex(g => g.CompanyName).IsUnique();
        }
    }
}