using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace OnlineGameStore.MVC
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                const string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}]" +
                                              " {Message}{NewLine}{Exception}";

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "Logs\\log.txt"),
                        rollingInterval: RollingInterval.Day, outputTemplate:
                        outputTemplate).CreateLogger();

                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}