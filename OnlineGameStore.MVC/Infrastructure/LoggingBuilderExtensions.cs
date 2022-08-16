using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using OnlineGameStore.DAL.Repositories.GameStore;
using OnlineGameStore.DAL.Repositories.Northwind;
using Serilog;
using Serilog.Filters;

namespace OnlineGameStore.MVC.Infrastructure
{
    public static class LoggerConfigurationExtension
    {
        private const string OutputTemplate =
            @"[{Level:u3}]|{Timestamp:yyyy-MM-dd HH:mm:ss.fff}|{CorrelationId}|{SourceContext}|{Message:lj}|{NewLine}{Exception}";

        public static LoggerConfiguration ConfigureSerilog(this LoggerConfiguration loggerConfiguration, HostBuilderContext context)
        {
            loggerConfiguration = loggerConfiguration
                .WriteTo.Console(outputTemplate: OutputTemplate)
                .WriteTo.File("Logs/log.log", outputTemplate: OutputTemplate, shared: true, 
                    rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1000000,
                    rollOnFileSizeLimit: true)
                .WriteTo.Logger(l =>
                {
                    l.WriteTo.MongoDBBson(cfg =>
                    {
                        var connectionString = context.Configuration.GetConnectionString("NorthwindConnection");
                        var connection = new MongoUrlBuilder(connectionString);
                        
                        var mongoClient = new MongoClient(connectionString);
                        var mongoDatabase = mongoClient.GetDatabase(connection.DatabaseName);
                        
                        cfg.SetMongoDatabase(mongoDatabase);
                        cfg.SetCollectionName("logs");
                        cfg.SetBatchPeriod(TimeSpan.FromSeconds(10));
                        cfg.SetCreateCappedCollection(10);
                    });
                    
                    var isFromGameStore = Matching.FromSource(typeof(SqlServerRepository<>).Namespace);
                    var isFromNorthwind = Matching.FromSource(typeof(MongoDbRepository<>).Namespace);
                    l.Filter.ByIncludingOnly(e => isFromGameStore(e) || isFromNorthwind(e));
                });

            loggerConfiguration = loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId();

            return loggerConfiguration;
        }
    }
}