using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Pipelines.Filters.Products
{
    public class ProductsBySuppliersFilter : IFilter<SortFilterGameModel, Expression<Func<ProductEntity, bool>>>
    {
        public Expression<Func<ProductEntity, bool>> Execute(SortFilterGameModel input)
        {
            var selectedSuppliers = input?.SelectedSuppliers;
            
            Expression<Func<ProductEntity, bool>> filterExpression = null;
            
            if (selectedSuppliers?.Any() == true)
            {
                filterExpression = p => selectedSuppliers.Contains(p.SupplierId.ToString());
            }

            return filterExpression;
        }
    }
}