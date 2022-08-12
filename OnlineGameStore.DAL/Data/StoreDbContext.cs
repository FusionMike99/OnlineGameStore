using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Data
{
    public class StoreDbContext : DbContext
    {
        private readonly INorthwindCategoryRepository _categoryRepository;
        
        public StoreDbContext([NotNull] DbContextOptions<StoreDbContext> options,
            INorthwindCategoryRepository categoryRepository) : base(options)
        {
            _categoryRepository = categoryRepository;
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                    entityType.AddSoftDeleteQueryFilter();

            await modelBuilder.StoreSeed(_categoryRepository);
        }
    }
}