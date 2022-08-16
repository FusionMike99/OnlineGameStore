using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Pipelines;
using OnlineGameStore.DAL.Pipelines.Filters.Games;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Builders.PipelineBuilders
{
    public class GamesPipelineBuilder : IGamesPipelineBuilder
    {
        public Pipeline<SortFilterGameModel, Expression<Func<GameEntity,bool>>> CreatePipeline()
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