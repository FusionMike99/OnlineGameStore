using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Utils;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Pipelines
{
    public class GamesFilterPipeline : Pipeline<SortFilterGameModel, Expression<Func<GameEntity, bool>>>
    {
        public override Expression<Func<GameEntity, bool>> Process(SortFilterGameModel input)
        {
            Expression<Func<GameEntity, bool>> targetExpression = null;

            var filterExpressions = filters.Select(filter => filter.Execute(input))
                .Where(filterExpression => filterExpression != null);
            
            foreach (var filterExpression in filterExpressions)
            {
                targetExpression = targetExpression != null 
                    ? targetExpression.AndAlso(filterExpression) 
                    : filterExpression;
            }

            return targetExpression;
        }
    }
}