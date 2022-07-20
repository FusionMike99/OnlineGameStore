using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories
{
    public interface INorthwindGenericRepository<TEntity, TKey>
        where TEntity : INorthwindBaseEntity<TKey>
    {
        TEntity Create(TEntity entity);
        
        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null);

        TEntity Update(Expression<Func<TEntity, bool>> predicate, TEntity entity);
    }
}