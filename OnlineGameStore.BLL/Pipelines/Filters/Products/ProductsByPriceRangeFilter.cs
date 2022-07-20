using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Pipelines.Filters.Products
{
    public class ProductsByPriceRangeFilter : IFilter<SortFilterGameModel, Expression<Func<NorthwindProduct, bool>>>
    {
        public Expression<Func<NorthwindProduct, bool>> Execute(SortFilterGameModel input)
        {
            var priceRange = input?.PriceRange;
            
            if (priceRange == null)
            {
                return null;
            }
            
            Expression<Func<NorthwindProduct, bool>> filterExpression = null;
            
            if (priceRange.Min.HasValue)
            {
                filterExpression = p => p.Price >= priceRange.Min;
            }

            if (priceRange.Max.HasValue)
            {
                Expression<Func<NorthwindProduct, bool>> otherExpression = p => p.Price <= priceRange.Max;
                
                filterExpression = filterExpression != null 
                    ? filterExpression.AndAlso(otherExpression)
                    : otherExpression;
            }

            return filterExpression;
        }
    }
}