using System.Collections.Generic;
using System.Globalization;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.MVC.DependencyInjections;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Infrastructure.Configurations;
using OnlineGameStore.MVC.Strategies.PaymentMethods;
using Serilog;
using Serilog.Events;

namespace OnlineGameStore.MVC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStrings = new Dictionary<string, string>
            {
                ["GameStore"] = _configuration.GetConnectionString("DockerSqlServerConnection"),
                ["Northwind"] = _configuration.GetConnectionString("NorthwindConnection")
            };
            
            services.AddRepositories(connectionStrings);
            services.AddServices();

            services.AddScoped<ICustomerIdAccessor, CustomerIdAccessor>();
            services.AddHttpContextAccessor();

            services.AddAutoMapper(configuration =>
            {
                configuration.AddEntitiesProfiles();
                configuration.AddModelsProfiles();
            });

            services.AddPaymentMethods();

            services.AddHangfire(configuration => configuration
                .SetHangfireConfiguration(connectionStrings["GameStore"]));
            services.AddHangfireServer();
            
            services.AddConfiguredAuthentication();

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    //new CultureInfo("en"),
                    new CultureInfo("uk"),
                    new CultureInfo("ru-UA")
                };

                options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOrderService orderService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseRequestLocalization();
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });
            
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "Request {RequestPath} handled by {IpAdress} and elapsed in {Elapsed} ms";
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("IpAdress", httpContext.Connection.RemoteIpAddress?.ToString());
                };
            });

            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            ConfigureCancellingOrderTask(orderService);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }

        private static void ConfigureCancellingOrderTask(IOrderService orderService)
        {
            RecurringJob.AddOrUpdate("cancellingOrders",
                () => orderService.CancelOrdersWithTimeoutAsync(),
                Cron.Minutely);
        }
    }
}