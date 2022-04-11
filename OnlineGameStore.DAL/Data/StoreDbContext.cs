using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.StoreSeed();
        }
    }
}
