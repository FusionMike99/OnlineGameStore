using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters.Games;

namespace OnlineGameStore.BLL.Builders.PipelineBuilders
{
    public static class GamesPipelineBuilder
    {
        public static Pipeline<SortFilterGameModel, Expression<Func<GameEntity,bool>>> CreatePipeline()
        {
            var gamesFilterPipeline = new GamesFilterPipeline()
                .Register(new GamesByGenresFilter())
                .Register(new GamesByPlatformTypesFilter())
                .Register(new GamesByPublishersFilter())
                .Register(new GamesByPriceRangeFilter())
                .Register(new GamesByDatePublishedFilter())
                .Register(new GamesByNameFilter());

            return gamesFilterPipeline;
        }
    }
}