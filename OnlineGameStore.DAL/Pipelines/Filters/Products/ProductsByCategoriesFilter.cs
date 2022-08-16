using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Pipelines.Filters.Products
{
    public class ProductsByCategoriesFilter : IFilter<SortFilterGameModel, Expression<Func<ProductEntity, bool>>>
    {
        public Expression<Func<ProductEntity, bool>> Execute(SortFilterGameModel input)
        {
            var selectedCategories = input?.SelectedCategories;
            
            Expression<Func<ProductEntity, bool>> filterExpression = null;
            
            if (selectedCategories != null)
            {
                filterExpression = p => selectedCategories.Contains(p.CategoryId.ToString());
            }

            return filterExpression;
        }
    }
}