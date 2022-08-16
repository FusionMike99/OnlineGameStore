using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Extensions
{
    public static class IncludeExtensions
    {
        public static IQueryable<GameEntity> IncludeForGames(this IQueryable<GameEntity> query)
        {
            return query.Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
                .Include(g => g.GamePlatformTypes).ThenInclude(gp => gp.PlatformType);
        }
        
        public static IQueryable<GenreEntity> IncludeForGenres(this IQueryable<GenreEntity> query)
        {
            return query.Include(g => g.SubGenres);
        }
        
        public static IQueryable<OrderEntity> IncludeForOrders(this IQueryable<OrderEntity> query)
        {
            return query.Include(o => o.OrderDetails);
        }
    }
}