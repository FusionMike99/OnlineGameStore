using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Pipelines.Filters
{
    public class GamesByPriceRangeFilter : IFilter<SortFilterGameModel, Expression<Func<Game, bool>>>
    {
        public Expression<Func<Game, bool>> Execute(SortFilterGameModel input)
        {
            var priceRange = input?.PriceRange;
            
            if (priceRange == null)
            {
                return null;
            }
            
            Expression<Func<Game, bool>> filterExpression = null;
            
            if (priceRange.Min.HasValue)
            {
                filterExpression = g => g.Price >= priceRange.Min;
            }

            if (priceRange.Max.HasValue)
            {
                Expression<Func<Game, bool>> otherExpression = g => g.Price <= priceRange.Max;
                
                filterExpression = filterExpression != null 
                    ? filterExpression.AndAlso(otherExpression)
                    : otherExpression;
            }

            return filterExpression;
        }
    }
}