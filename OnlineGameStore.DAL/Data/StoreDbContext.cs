using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;

namespace OnlineGameStore.DAL.Data
{
    public class StoreDbContext : DbContext
    {
        private readonly INorthwindUnitOfWork _northwindUnitOfWork;
        
        public StoreDbContext([NotNull] DbContextOptions<StoreDbContext> options,
            INorthwindUnitOfWork northwindUnitOfWork) : base(options)
        {
            _northwindUnitOfWork = northwindUnitOfWork;
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                    entityType.AddSoftDeleteQueryFilter();

            modelBuilder.StoreSeed(_northwindUnitOfWork);
        }
    }
}