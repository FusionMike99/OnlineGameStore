using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces
{
    public interface IGamesPipelineBuilder : IPipelineBuilder<SortFilterGameModel, Expression<Func<GameEntity,bool>>>
    {
        
    }
}