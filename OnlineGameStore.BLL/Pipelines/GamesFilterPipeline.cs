using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Pipelines
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