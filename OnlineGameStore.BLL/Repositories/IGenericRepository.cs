using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : IBaseEntity<TKey>
    {
        TEntity Create(TEntity entity);

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate,
            bool includeDeleteEntities = false,
            params string[] includeProperties);

        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeleteEntities = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params string[] includeProperties);

        int CountEntities(Expression<Func<TEntity, bool>> predicate = null);

        TEntity Update(TEntity entity, Expression<Func<TEntity, bool>> predicate = null);

        void Delete(TEntity entity);
    }
}