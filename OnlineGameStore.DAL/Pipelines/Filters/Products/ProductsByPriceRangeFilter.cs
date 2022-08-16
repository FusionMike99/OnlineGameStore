using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Utils;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Pipelines.Filters.Products
{
    public class ProductsByPriceRangeFilter : IFilter<SortFilterGameModel, Expression<Func<ProductEntity, bool>>>
    {
        public Expression<Func<ProductEntity, bool>> Execute(SortFilterGameModel input)
        {
            var priceRange = input?.PriceRange;
            
            if (priceRange == null)
            {
                return null;
            }
            
            Expression<Func<ProductEntity, bool>> filterExpression = null;
            
            if (priceRange.Min.HasValue)
            {
                filterExpression = p => p.Price >= priceRange.Min;
            }

            if (priceRange.Max.HasValue)
            {
                Expression<Func<ProductEntity, bool>> otherExpression = p => p.Price <= priceRange.Max;
                
                filterExpression = filterExpression != null 
                    ? filterExpression.AndAlso(otherExpression)
                    : otherExpression;
            }

            return filterExpression;
        }
    }
}