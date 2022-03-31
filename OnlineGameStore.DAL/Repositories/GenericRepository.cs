using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineGameStore.DAL.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
    {
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(StoreDbContext context)
        {
            _entities = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _entities.Add(entity);

            return entity;
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _entities.Update(entity);

            return entity;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate = null,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.SingleOrDefault();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        private IQueryable<TEntity> Include(params string[] includeProperties)
        {
            IQueryable<TEntity> query = _entities.AsNoTracking();

            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
