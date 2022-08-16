using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Extensions;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Repositories.SqlServer
{
    public class GameSqlServerRepository : SqlServerRepository<GameEntity>, IGameSqlServerRepository
    {
        private readonly IGamesPipelineBuilder _gamesPipelineBuilder;
        
        public GameSqlServerRepository(StoreDbContext context, ILoggerFactory logger,
            IGamesPipelineBuilder gamesPipelineBuilder) : base(context, logger)
        {
            _gamesPipelineBuilder = gamesPipelineBuilder;
        }

        public async Task<GameEntity> GetByKeyAsync(string gameKey)
        {
            var game = await Entities.IncludeForGames().FirstOrDefaultAsync(g => g.Key == gameKey);
            
            return game;
        }

        public async Task<GameEntity> GetByKeyIncludeDeletedAsync(string gameKey)
        {
            var game = await Entities.IncludeDeleted().FirstOrDefaultAsync(g => g.Key == gameKey);
            
            return game;
        }

        public async Task<IEnumerable<GameEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel)
        {
            var predicate = GetGameStorePredicate(sortFilterModel);
            var games = await Entities.IncludeDeleted().Where(predicate).ToListAsync();
            
            return games;
        }
        
        private Expression<Func<GameEntity, bool>> GetGameStorePredicate(SortFilterGameModel model)
        {
            var gamesFilterPipeline = _gamesPipelineBuilder.CreatePipeline();
            var predicate = gamesFilterPipeline.Process(model);

            return predicate;
        }
    }
}