using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Pipelines
{
    public class ProductsFilterPipeline : Pipeline<SortFilterGameModel, Expression<Func<ProductEntity, bool>>>
    {
        public override Expression<Func<ProductEntity, bool>> Process(SortFilterGameModel input)
        {
            Expression<Func<ProductEntity, bool>> targetExpression = null;

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