using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Builders.PipelineBuilders;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters.Games;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreGameRepository : GameStoreGenericRepository<GameEntity>, IGameStoreGameRepository
    {
        public GameStoreGameRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<GameEntity> GetByKeyAsync(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<GameEntity, bool>> predicate = g => g.Key == gameKey;
            var game = await IncludeProperties(includeDeleted, includeProperties).SingleOrDefaultAsync(predicate);
            
            return game;
        }

        public async Task<IEnumerable<GameEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel)
        {
            var predicate = GetGameStorePredicate(sortFilterModel);
            var games = await IncludeProperties(includeDeleted: true).Where(predicate).ToListAsync();
            
            return games;
        }
        
        private static Expression<Func<GameEntity, bool>> GetGameStorePredicate(SortFilterGameModel model)
        {
            var gamesFilterPipeline = GamesPipelineBuilder.CreatePipeline();
            var predicate = gamesFilterPipeline.Process(model);

            return predicate;
        }
    }
}