using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(o => o.OrderStatusId)
                .HasDefaultValue(1);

            builder.HasOne(o => o.OrderStatus)
                .WithMany(os => os.Orders)
                .HasForeignKey(o => o.OrderStatusId);
        }
    }
}