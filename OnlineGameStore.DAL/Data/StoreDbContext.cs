using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext([NotNull] DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        public DbSet<GameEntity> Games { get; set; }

        public DbSet<PublisherEntity> Publishers { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<GenreEntity> Genres { get; set; }

        public DbSet<PlatformTypeEntity> PlatformTypes { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                    entityType.AddSoftDeleteQueryFilter();

            modelBuilder.StoreSeed();
        }
    }
}