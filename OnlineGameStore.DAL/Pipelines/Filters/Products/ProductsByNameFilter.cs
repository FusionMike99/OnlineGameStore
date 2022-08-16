using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Pipelines.Filters.Products
{
    public class ProductsByNameFilter : IFilter<SortFilterGameModel, Expression<Func<ProductEntity, bool>>>
    {
        public Expression<Func<ProductEntity, bool>> Execute(SortFilterGameModel input)
        {
            var gameName = input?.GameName;
            
            Expression<Func<ProductEntity, bool>> filterExpression = null;
            
            if (!string.IsNullOrWhiteSpace(gameName))
            {
                filterExpression = g => g.Name.ToLower().Contains(gameName.ToLower());
            }

            return filterExpression;
        }
    }
}