using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> Create(TEntity entity);

        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate,
            bool includeDeleted = false,
            params string[] includeProperties);

        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeleted = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            params string[] includeProperties);

        Task<TEntity> GetById(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties);

        Task<IEnumerable<TEntity>> GetAll(bool includeDeleted = false,
            params string[] includeProperties);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}