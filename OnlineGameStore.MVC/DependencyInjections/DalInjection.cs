using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DAL.Builders.PipelineBuilders;
using OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Repositories;
using OnlineGameStore.DAL.Repositories.MongoDb;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;
using OnlineGameStore.DAL.Repositories.SqlServer;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;

namespace OnlineGameStore.MVC.DependencyInjections
{
    public static class DalInjection
    {
        public static void AddRepositories(this IServiceCollection services,
            IDictionary<string, string> connectionStrings)
        {
            AddSqlServerRepositories(services, connectionStrings["GameStore"]);
            AddPipelineBuilders(services);
            AddMongoDbRepositories(services, connectionStrings["Northwind"]);
            AddCommonRepositories(services);
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

        private static void AddSqlServerRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));
            
            services.AddScoped<ICommentSqlServerRepository, CommentSqlServerRepository>();
            services.AddScoped<IGameSqlServerRepository, GameSqlServerRepository>();
            services.AddScoped<IGenreSqlServerRepository, GenreSqlServerRepository>();
            services.AddScoped<IOrderSqlServerRepository, OrderSqlServerRepository>();
            services.AddScoped<IPlatformTypeSqlServerRepository, PlatformTypeSqlServerRepository>();
            services.AddScoped<IPublisherSqlServerRepository, PublisherSqlServerRepository>();
        }
        
        private static void AddPipelineBuilders(this IServiceCollection services)
        {
            services.AddScoped<IGamesPipelineBuilder, GamesPipelineBuilder>();
            services.AddScoped<IProductsPipelineBuilder, ProductsPipelineBuilder>();
        }

        private static void AddMongoDbRepositories(this IServiceCollection services, string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            var clientSettings = MongoClientSettings.FromConnectionString(connectionString);
            clientSettings.LinqProvider = LinqProvider.V3;
            var mongoClient = new MongoClient(clientSettings);
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
                    provider.GetService<ISupplierMongoDbRepository>(), provider.GetService<IProductsPipelineBuilder>()));
            services.AddScoped<IShipperMongoDbRepository>(provider => 
                new ShipperMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
            services.AddScoped<ISupplierMongoDbRepository>(provider => 
                new SupplierMongoDbRepository(mongoDatabase, provider.GetService<ILoggerFactory>()));
        }
    }
}
