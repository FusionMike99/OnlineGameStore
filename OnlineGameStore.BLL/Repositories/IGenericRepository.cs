using System;
using System.Collections.Generic;
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
            params string[] includeProperties);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }
}