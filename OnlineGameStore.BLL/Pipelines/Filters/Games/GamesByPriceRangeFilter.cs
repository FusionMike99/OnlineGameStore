using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Pipelines.Filters.Games
{
    public class GamesByPriceRangeFilter : IFilter<SortFilterGameModel, Expression<Func<GameEntity, bool>>>
    {
        public Expression<Func<GameEntity, bool>> Execute(SortFilterGameModel input)
        {
            var priceRange = input?.PriceRange;
            
            if (priceRange == null)
            {
                return null;
            }
            
            Expression<Func<GameEntity, bool>> filterExpression = null;
            
            if (priceRange.Min.HasValue)
            {
                filterExpression = g => g.Price >= priceRange.Min;
            }

            if (priceRange.Max.HasValue)
            {
                Expression<Func<GameEntity, bool>> otherExpression = g => g.Price <= priceRange.Max;
                
                filterExpression = filterExpression != null 
                    ? filterExpression.AndAlso(otherExpression)
                    : otherExpression;
            }

            return filterExpression;
        }
    }
}