using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Products
{
    public class ProductsBySuppliersFilter : IFilter<SortFilterGameModel, Expression<Func<NorthwindProduct, bool>>>
    {
        public Expression<Func<NorthwindProduct, bool>> Execute(SortFilterGameModel input)
        {
            var selectedSuppliers = input?.SelectedSuppliers;
            
            Expression<Func<NorthwindProduct, bool>> filterExpression = null;
            
            if (selectedSuppliers?.Any() == true)
            {
                filterExpression = p => selectedSuppliers.Contains(p.SupplierId);
            }

            return filterExpression;
        }
    }
}