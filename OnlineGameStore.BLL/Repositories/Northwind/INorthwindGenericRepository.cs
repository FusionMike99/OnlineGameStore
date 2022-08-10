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
        Task<TEntity> GetById(ObjectId id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Update(TEntity entity);
    }
}