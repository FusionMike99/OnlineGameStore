using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public TEntity GetSingle(Func<TEntity, bool> predicate = null,
            bool includeDeleteEntities = false,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (includeDeleteEntities)
            {
                query = query.IgnoreQueryFilters();
            }

            var localQuery = query.AsEnumerable();

            if (predicate != null)
            {
                localQuery = localQuery.Where(predicate);
            }

            return localQuery.SingleOrDefault();
        }

        public IEnumerable<TEntity> GetMany(Func<TEntity, bool> predicate = null,
            bool includeDeleteEntities = false,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (includeDeleteEntities)
            {
                query = query.IgnoreQueryFilters();
            }

            var localQuery = query.AsEnumerable();

            if (predicate != null)
            {
                localQuery = localQuery.Where(predicate);
            }

            var localList = localQuery.ToList();

            return localList;
        }

        private IQueryable<TEntity> Include(params string[] includeProperties)
        {
            var query = _entities.AsNoTrackingWithIdentityResolution();

            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
