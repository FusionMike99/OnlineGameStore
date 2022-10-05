using Microsoft.Extensions.DependencyInjection;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.Services.Interfaces;

namespace OnlineGameStore.MVC.DependencyInjections
{
    public static class BllInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPlatformTypeService, PlatformTypeService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShipperService, ShipperService>();
        }
    }
}
