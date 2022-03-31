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
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Repositories;
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
            string connection = Configuration.GetConnectionString("LocalDb");

            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGameService, GameService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddAutoMapper(typeof(CommentMappingProfile), typeof(GameMappingProfile));

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
