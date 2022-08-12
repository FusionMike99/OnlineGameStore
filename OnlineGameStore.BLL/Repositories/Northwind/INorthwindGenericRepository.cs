﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindGenericRepository<TEntity>
        where TEntity : NorthwindBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Update(TEntity entity);
    }
}