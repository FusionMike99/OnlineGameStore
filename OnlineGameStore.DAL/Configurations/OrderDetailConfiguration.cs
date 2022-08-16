using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");
            builder.HasKey(x => new { x.GameKey, x.OrderId});
            
            builder.Property(od => od.Id)
                .ValueGeneratedOnAdd();

            builder.Property(od => od.Price)
                .HasColumnType("money")
                .HasDefaultValue(0D);

            builder.Property(od => od.Quantity)
                .HasDefaultValue(1);

            builder.Property(od => od.Discount)
                .HasDefaultValue(0F);

            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            builder.Ignore(od => od.Product);
        }
    }
}