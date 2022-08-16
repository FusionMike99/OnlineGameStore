using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class GameConfiguration : IEntityTypeConfiguration<GameEntity>
    {
        public void Configure(EntityTypeBuilder<GameEntity> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(g => g.Key)
                .IsRequired();

            builder.Property(g => g.Name)
                .IsRequired();

            builder.Property(g => g.Price)
                .HasColumnType("money")
                .IsRequired();

            builder.Property(g => g.UnitsInStock)
                .IsRequired();

            builder.Property(g => g.Discontinued)
                .IsRequired();

            builder.Property(g => g.IsDeleted)
                .HasDefaultValue(false);
            
            builder.Property(g => g.ViewsNumber)
                .HasDefaultValue(0);
            
            builder.Property(g => g.QuantityPerUnit)
                .HasDefaultValue(string.Empty);
            
            builder.Ignore(g => g.Publisher);
            
            builder.Ignore(g => g.OrderDetails);

            builder.HasIndex(g => g.Key).IsUnique();

            builder.HasIndex(g => g.Name).IsUnique();
        }
    }
}