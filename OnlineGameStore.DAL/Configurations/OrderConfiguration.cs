using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .ValueGeneratedOnAdd();

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.IsDeleted)
                .HasDefaultValue(false);
            
            builder.Property(o => o.Freight)
                .HasColumnType("money")
                .HasDefaultValue(0D);
            
            builder.Property(o => o.ShipName)
                .HasDefaultValue(string.Empty);
            
            builder.Property(o => o.ShipAddress)
                .HasDefaultValue(string.Empty);
            
            builder.Property(o => o.ShipCity)
                .HasDefaultValue(string.Empty);
            
            builder.Property(o => o.ShipRegion)
                .HasDefaultValue(string.Empty);
            
            builder.Property(o => o.ShipPostalCode)
                .HasDefaultValue(string.Empty);
            
            builder.Property(o => o.ShipCountry)
                .HasDefaultValue(string.Empty);
        }
    }
}