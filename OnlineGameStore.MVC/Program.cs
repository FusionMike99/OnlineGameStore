using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OnlineGameStore.MVC.Infrastructure;
using Serilog;
using Serilog.Events;

namespace OnlineGameStore.MVC
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .CreateBootstrapLogger();
            
            try
            {
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
                .UseSerilog((hostingContext, logging) => logging.ConfigureSerilog(hostingContext))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}