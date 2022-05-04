using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Services.Contracts;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Repositories;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Mapper;
using OnlineGameStore.MVC.Utils;
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

            services.AddScoped<ICartService>(CookieCartService.GetCart);

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddHttpContextAccessor();

            services.AddAutoMapper(
                typeof(CommentMappingProfile),
                typeof(GameMappingProfile),
                typeof(GenreMappingProfile),
                typeof(PlatformTypeMappingProfile),
                typeof(PublisherMappingProfile),
                typeof(OrderDetailMappingProfile));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("en-US", "uk-UA", "ru-RU")
                    .AddSupportedUICultures("en-US", "uk-UA", "ru-RU")
                    .SetDefaultCulture("en-US");
            });

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
            app.UseSession();

            app.UseRouting();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
