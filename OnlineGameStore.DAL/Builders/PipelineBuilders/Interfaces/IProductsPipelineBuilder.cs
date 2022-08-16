using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces
{
    public interface IProductsPipelineBuilder : IPipelineBuilder<SortFilterGameModel, Expression<Func<ProductEntity,bool>>>
    {
        
    }
}