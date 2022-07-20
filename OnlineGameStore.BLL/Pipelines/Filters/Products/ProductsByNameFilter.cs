using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Products
{
    public class ProductsByNameFilter : IFilter<SortFilterGameModel, Expression<Func<NorthwindProduct, bool>>>
    {
        public Expression<Func<NorthwindProduct, bool>> Execute(SortFilterGameModel input)
        {
            var gameName = input?.GameName;
            
            Expression<Func<NorthwindProduct, bool>> filterExpression = null;
            
            if (!string.IsNullOrWhiteSpace(gameName))
            {
                filterExpression = g => g.Name.ToLower().Contains(gameName.ToLower());
            }

            return filterExpression;
        }
    }
}