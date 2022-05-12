using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        private readonly StoreDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
            
            _entities = context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _entities.Add(entity);

            return entity;
        }

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            Update(entity);
        }

        public TEntity Update(TEntity entity)
        {
            var exist = _entities.Find(entity.Id);

            foreach (var navEntity in _context.Entry(entity).Navigations)
            {
                if (navEntity.CurrentValue == null)
                {
                    continue;
                }

                var navEntityName = navEntity.Metadata.Name;

                var navExist = _context.Entry(exist).Navigation(navEntityName);

                navExist.Load();

                navExist.CurrentValue = navEntity.CurrentValue;
            }

            _context.Entry(exist).CurrentValues.SetValues(entity);

            return entity;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate,
            bool includeDeleteEntities = false,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (includeDeleteEntities)
            {
                query = query.IgnoreQueryFilters();
            }

            var foundEntity = query.SingleOrDefault(predicate);
            
            return foundEntity;
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeleteEntities = false,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (includeDeleteEntities)
            {
                query = query.IgnoreQueryFilters();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var foundList = query.ToList();
            
            return foundList;
        }

        private IQueryable<TEntity> Include(params string[] includeProperties)
        {
            var query = _entities.AsQueryable();

            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}