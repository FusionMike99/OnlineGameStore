using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class PublisherConfiguration : IEntityTypeConfiguration<PublisherEntity>
    {
        public void Configure(EntityTypeBuilder<PublisherEntity> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.CompanyName)
                .HasColumnType("nvarchar(40)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("ntext")
                .IsRequired();

            builder.Property(p => p.HomePage)
                .HasColumnType("ntext")
                .IsRequired();
            
            builder.Property(p => p.CompanyName)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.ContactName)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.ContactTitle)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.Address)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.City)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.Region)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.PostalCode)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.Country)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.Phone)
                .HasDefaultValue(string.Empty);
            
            builder.Property(p => p.Fax)
                .HasDefaultValue(string.Empty);
            
            builder.Ignore(p => p.Games);

            builder.HasIndex(g => g.CompanyName).IsUnique();
        }
    }
}