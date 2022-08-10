using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> Create(TEntity entity);

        Task<TEntity> GetById(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties);

        Task<IEnumerable<TEntity>> GetAll(bool includeDeleted = false,
            params string[] includeProperties);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}