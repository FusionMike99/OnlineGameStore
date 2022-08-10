using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.DAL.Utils
{
    public static class OrderPredicate
    {
        public static Expression<Func<TEntity, bool>> GetPredicate<TEntity>(FilterOrderModel model)
        {
            if (model == null)
            {
                return null;
            }
            
            Expression<Func<TEntity, bool>> filterExpression = null;
            
            var orderParameter = Expression.Parameter(typeof(TEntity));
            var orderDateProperty = Expression.Property(orderParameter,
                typeof(TEntity).GetProperty("OrderDate")!);
            
            if (model.MinDate.HasValue)
            {
                model.MinDate = DateTime.SpecifyKind(model.MinDate.Value, DateTimeKind.Utc);
                
                var minDateConstant = Expression.Constant(model.MinDate.Value.Date, model.MinDate.GetType());
                var comparison = Expression.GreaterThanOrEqual(orderDateProperty, minDateConstant);
                filterExpression = Expression.Lambda<Func<TEntity, bool>>(comparison, orderParameter);
            }
            
            if (model.MaxDate.HasValue)
            {
                model.MaxDate = DateTime.SpecifyKind(model.MaxDate.Value, DateTimeKind.Utc);
                
                var maxDateConstant = Expression.Constant(model.MaxDate.Value.Date, model.MaxDate.GetType());
                var comparison = Expression.LessThanOrEqual(orderDateProperty, maxDateConstant);
                var otherExpression = Expression.Lambda<Func<TEntity, bool>>(comparison, orderParameter);
                
                filterExpression = filterExpression != null 
                    ? filterExpression.AndAlso(otherExpression)
                    : otherExpression;
            }

            return filterExpression;
        }
    }
}