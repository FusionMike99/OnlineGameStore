using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Configurations
{
    internal class GameGenreConfiguration : IEntityTypeConfiguration<GameGenreEntity>
    {
        public void Configure(EntityTypeBuilder<GameGenreEntity> builder)
        {
            builder.ToTable("GameGenre");
            builder.HasKey(x => new { x.GameId, x.GenreId});
            
            builder.Property(gg => gg.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GameId);

            builder.HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GenreId);
        }
    }
}