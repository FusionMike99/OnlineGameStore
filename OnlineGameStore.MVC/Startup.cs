using System;
using System.Globalization;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Repositories;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Strategies.PaymentMethods;
using OnlineGameStore.MVC.Mapper;
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

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGameService, GameService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IPlatformTypeService, PlatformTypeService>();

            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IOrderService, OrderService>();
            
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICustomerIdAccessor, CustomerIdAccessor>();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(
                typeof(CommentMappingProfile),
                typeof(GameMappingProfile),
                typeof(GenreMappingProfile),
                typeof(PlatformTypeMappingProfile),
                typeof(PublisherMappingProfile),
                typeof(OrderMappingProfile));

            services.AddScoped<IPaymentMethodStrategy, BankPaymentMethodStrategy>();
            services.AddScoped<IPaymentMethodStrategy, TerminalIBoxPaymentMethodStrategy>();
            services.AddScoped<IPaymentMethodStrategy, VisaPaymentMethodStrategy>();

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"),
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    }));

            services.AddHangfireServer();

            services.AddLocalization();
            
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("uk"),
                    new CultureInfo("ru-UA"),
                    new CultureInfo("ru")
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
                () => orderService.CancelOrdersWithTimeout(TimeSpan.FromMinutes(3)),
                Cron.Minutely);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOrderService orderService)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/error/{0}");
            
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