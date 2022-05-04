using OnlineGameStore.BLL.Entities;
using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : IBaseEntity<TKey>
    {
        TEntity Create(TEntity entity);

        TEntity GetSingle(Func<TEntity, bool> predicate = null,
            bool includeDeleteEntities = false,
            params string[] includeProperties);

        IEnumerable<TEntity> GetMany(Func<TEntity, bool> predicate = null,
            bool includeDeleteEntities = false,
            params string[] includeProperties);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
