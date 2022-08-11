using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Repositories;
using OnlineGameStore.DAL.Repositories.GameStore;
using OnlineGameStore.DAL.Repositories.Northwind;

namespace OnlineGameStore.Infrastructure.Injections
{
    public static class DalInjection
    {
        public static void AddRepositories(this IServiceCollection services,
            IDictionary<string, string> connectionStrings)
        {
            services.AddGameStoreRepositories(connectionStrings["GameStore"]);
            services.AddNorthwindRepositories(connectionStrings["Northwind"]);
            services.AddCommonRepositories();
        }
        
        private static void AddCommonRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPlatformTypeRepository, PlatformTypeRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IShipperRepository, ShipperRepository>();
        }

        private static void AddGameStoreRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));
            
            services.AddScoped<IGameStoreCommentRepository, GameStoreCommentRepository>();
            services.AddScoped<IGameStoreGameRepository, GameStoreGameRepository>();
            services.AddScoped<IGameStoreGenreRepository, GameStoreGenreRepository>();
            services.AddScoped<IGameStoreOrderRepository, GameStoreOrderRepository>();
            services.AddScoped<IGameStorePlatformTypeRepository, GameStorePlatformTypeRepository>();
            services.AddScoped<IGameStorePublisherRepository, GameStorePublisherRepository>();
        }

        private static void AddNorthwindRepositories(this IServiceCollection services, string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
                        
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(connection.DatabaseName);
            
            services.AddScoped<INorthwindCategoryRepository>(provider => 
                new NorthwindCategoryRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
            services.AddScoped<INorthwindOrderDetailRepository>(provider => 
                new NorthwindOrderDetailRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
            services.AddScoped<INorthwindOrderRepository>(provider => 
                new NorthwindOrderRepository(mongoDatabase, provider.GetService<ILoggerFactory>(),
                    provider.GetService<INorthwindOrderDetailRepository>(),
                    provider.GetService<INorthwindShipperRepository>()));
            services.AddScoped<INorthwindProductRepository>(provider => 
                new NorthwindProductRepository(mongoDatabase, provider.GetService<ILoggerFactory>(),
                    provider.GetService<INorthwindSupplierRepository>()));
            services.AddScoped<INorthwindShipperRepository>(provider => 
                new NorthwindShipperRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
            services.AddScoped<INorthwindSupplierRepository>(provider => 
                new NorthwindSupplierRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
        }
    }
}
