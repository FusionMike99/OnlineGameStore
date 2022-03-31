using Microsoft.Extensions.DependencyInjection;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.Infrastructure
{
    public static class BLLConfigurator
    {
        public static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();

            services.AddScoped<ICommentService, CommentService>();
        }
    }
}
