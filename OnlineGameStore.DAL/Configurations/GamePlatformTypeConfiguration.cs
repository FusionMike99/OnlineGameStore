using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    public class GamePlatformTypeConfiguration : IEntityTypeConfiguration<GamePlatformTypeEntity>
    {
        public void Configure(EntityTypeBuilder<GamePlatformTypeEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(gg => gg.Game)
                .WithMany(g => g.GamePlatformTypes)
                .HasForeignKey(gg => gg.GameId);

            builder.HasOne(gg => gg.PlatformType)
                .WithMany(g => g.GamePlatformTypes)
                .HasForeignKey(gg => gg.PlatformId);
        }
    }
}