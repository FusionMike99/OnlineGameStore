using System;
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
using Microsoft.Extensions.Options;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.Infrastructure.Injections;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Infrastructure.Configurations;
using OnlineGameStore.MVC.Strategies.PaymentMethods;
using Serilog;
using Serilog.Events;

namespace OnlineGameStore.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStrings = new Dictionary<string, string>
            {
                ["GameStore"] = Configuration.GetConnectionString("DockerSqlServerConnection"),
                ["Northwind"] = Configuration.GetConnectionString("NorthwindConnection")
            };
            
            services.AddRepositories(connectionStrings);
            services.AddServices();

            services.AddScoped<ICustomerIdAccessor, CustomerIdAccessor>();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(configuration => configuration.AddModelsProfiles());

            services.AddPaymentMethods();

            services.AddHangfire(configuration => configuration
                .SetHangfireConfiguration(connectionStrings["GameStore"]));

            services.AddHangfireServer();

            services.AddLocalization();
            
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("uk"),
                    new CultureInfo("ru-UA")
                };

                options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder);
        }

        private static void ConfigureCancellingOrderTask(IOrderService orderService)
        {
            RecurringJob.AddOrUpdate("cancellingOrders",
                () => orderService.CancelOrdersWithTimeout(),
                Cron.Minutely);

            BackgroundJob.Enqueue(() => Console.WriteLine());
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

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()?.Value;
            
            app.UseRequestLocalization(localizationOptions);
            
            ConfigureCancellingOrderTask(orderService);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}