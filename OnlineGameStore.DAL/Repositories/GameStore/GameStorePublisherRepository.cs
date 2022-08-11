using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStorePublisherRepository : GameStoreGenericRepository<Publisher>, IGameStorePublisherRepository
    {
        public GameStorePublisherRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<Publisher> GetByName(string companyName,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<Publisher, bool>> predicate = p => p.CompanyName == companyName;

            return await GetSingle(predicate, includeDeleted, includeProperties);
        }
    }
}