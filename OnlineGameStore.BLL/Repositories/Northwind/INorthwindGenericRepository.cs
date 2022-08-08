using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindGenericRepository<TEntity>
        where TEntity : NorthwindBaseEntity
    {
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null);
        
        Task<TEntity> GetById(ObjectId id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Update(TEntity entity);
    }
}