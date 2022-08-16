using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    public class GamePlatformTypeConfiguration : IEntityTypeConfiguration<GamePlatformTypeEntity>
    {
        public void Configure(EntityTypeBuilder<GamePlatformTypeEntity> builder)
        {
            builder.ToTable("GamePlatformType");
            builder.HasKey(x => new { x.GameId, x.PlatformId});
            
            builder.Property(gp => gp.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(gg => gg.Game)
                .WithMany(g => g.GamePlatformTypes)
                .HasForeignKey(gg => gg.GameId);

            builder.HasOne(gg => gg.PlatformType)
                .WithMany(g => g.GamePlatformTypes)
                .HasForeignKey(gg => gg.PlatformId);
        }
    }
}