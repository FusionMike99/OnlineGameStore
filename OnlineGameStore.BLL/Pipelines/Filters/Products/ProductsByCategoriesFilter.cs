using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Products
{
    public class ProductsByCategoriesFilter : IFilter<SortFilterGameModel, Expression<Func<NorthwindProduct, bool>>>
    {
        public Expression<Func<NorthwindProduct, bool>> Execute(SortFilterGameModel input)
        {
            var selectedCategories = input?.SelectedCategories;
            
            Expression<Func<NorthwindProduct, bool>> filterExpression = null;
            
            if (selectedCategories != null)
            {
                filterExpression = p => selectedCategories.Contains(p.CategoryId.ToString());
            }

            return filterExpression;
        }
    }
}