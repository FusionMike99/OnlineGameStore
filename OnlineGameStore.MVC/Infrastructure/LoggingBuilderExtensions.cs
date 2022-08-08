using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineGameStore.DAL.Repositories;
using OnlineGameStore.DAL.Repositories.GameStore;
using OnlineGameStore.DAL.Repositories.Northwind;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace OnlineGameStore.MVC.Infrastructure
{
    public static class LoggingBuilderExtension
    {
        private const string OutputTemplate =
            @"[{Level:u3}]|{Timestamp:yyyy-MM-dd HH:mm:ss.fff}|{CorrelationId}|{SourceContext}|{Message:lj}|{NewLine}{Exception}";

        public static void AddGenericLogging(this ILoggingBuilder loggingBuilder, HostBuilderContext context)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .WriteTo.Logger(l =>
                {
                    l.WriteTo.MongoDBBson(cfg =>
                    {
                        var connectionString = context.Configuration.GetConnectionString("NorthwindConnection");
                        var connection = new MongoUrlBuilder(connectionString);
                        
                        var mongoClient = new MongoClient(connectionString);
                        var mongoDatabase = mongoClient.GetDatabase(connection.DatabaseName);
                        
                        cfg.SetMongoDatabase(mongoDatabase);
                        cfg.SetCollectionName("Logs");
                        cfg.SetBatchPeriod(TimeSpan.FromSeconds(10));
                        cfg.SetCreateCappedCollection(10);
                    });
                    l.Filter.ByIncludingOnly(e =>
                        e.Properties.GetValueOrDefault("SourceContext") is ScalarValue sv &&
                        (sv.Value.ToString()!.Contains(typeof(GenericRepository<>).Namespace!)
                        || sv.Value.ToString()!.Contains(typeof(NorthwindGenericRepository<>).Namespace!)));
                })
                .WriteTo.Console(outputTemplate: OutputTemplate)
                .WriteTo.File(
                    "Logs/log.log", 
                    outputTemplate: OutputTemplate, 
                    shared: true, 
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 1000000,
                    rollOnFileSizeLimit: true);

            if (context.Configuration.GetSection("Serilog") != null)
            {
                loggingBuilder.AddConfiguration(context.Configuration);
                loggerConfiguration.ReadFrom.Configuration(context.Configuration);
            }

            loggerConfiguration = loggerConfiguration
                .Enrich.FromLogContext();

            var logger = loggerConfiguration.CreateLogger();
            Log.Logger = logger;

            loggingBuilder.AddProvider(new SerilogLoggerProvider(logger));
        }
    }
}