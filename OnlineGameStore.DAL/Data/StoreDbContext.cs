using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.DAL.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineGameStore.DAL.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new GameGenreConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GamePlatformTypeConfiguration());

            modelBuilder.StoreSeed();
        }
    }
}
