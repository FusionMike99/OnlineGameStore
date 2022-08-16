using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.MongoDb;
using OnlineGameStore.BLL.Repositories.SqlServer;
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
            
            services.AddScoped<ICommentSqlServerRepository, CommentSqlServerRepository>();
            services.AddScoped<IGameSqlServerRepository, GameSqlServerRepository>();
            services.AddScoped<IGenreSqlServerRepository, GenreSqlServerRepository>();
            services.AddScoped<IOrderSqlServerRepository, OrderSqlServerRepository>();
            services.AddScoped<IPlatformTypeSqlServerRepository, PlatformTypeSqlServerRepository>();
            services.AddScoped<IPublisherSqlServerRepository, PublisherSqlServerRepository>();
        }

        private static void AddNorthwindRepositories(this IServiceCollection services, string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
                        
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(connection.DatabaseName);
            
            services.AddScoped<ICategoryMongoDbRepository>(provider => 
                new CategoryMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
            services.AddScoped<IOrderDetailMongoDbRepository>(provider => 
                new OrderDetailMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
            services.AddScoped<IOrderMongoDbRepository>(provider => 
                new OrderMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>(),
                    provider.GetService<IOrderDetailMongoDbRepository>(),
                    provider.GetService<IShipperMongoDbRepository>()));
            services.AddScoped<IProductMongoDbRepository>(provider => 
                new ProductMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>(),
                    provider.GetService<ISupplierMongoDbRepository>()));
            services.AddScoped<IShipperMongoDbRepository>(provider => 
                new ShipperMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
            services.AddScoped<ISupplierMongoDbRepository>(provider => 
                new SupplierMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
        }
    }
}
