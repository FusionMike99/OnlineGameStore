using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Repositories;

namespace OnlineGameStore.Infrastructure
{
    public static class DALConfigurator
    {
        public static void ConfigureService(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
