using System;
using Hangfire;
using Hangfire.SqlServer;

namespace OnlineGameStore.MVC.Infrastructure.Configurations
{
    public static class HangfireConfiguration
    {
        public static void SetHangfireConfiguration(this IGlobalConfiguration configuration, string connectionString)
        {
            configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString,
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    });
        }
    }
}