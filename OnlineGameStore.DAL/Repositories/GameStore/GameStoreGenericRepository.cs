﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public abstract class GameStoreGenericRepository<TEntity> : IGameStoreGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly StoreDbContext Context;
        private readonly DbSet<TEntity> _entities;

        protected GameStoreGenericRepository(StoreDbContext context)
        {
            Context = context;
            
            _entities = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            await Update(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(bool includeDeleted = false, params string[] includeProperties)
        {
            return await GetMany(includeDeleted: includeDeleted,
                includeProperties: includeProperties);
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            var exist = await _entities.FindAsync(entity.Id);

            foreach (var navEntity in Context.Entry(entity).Navigations)
            {
                if (navEntity.CurrentValue == null)
                {
                    continue;
                }

                var navEntityName = navEntity.Metadata.Name;

                var navExist = Context.Entry(exist).Navigation(navEntityName);

                await navExist.LoadAsync();

                navExist.CurrentValue = navEntity.CurrentValue;
            }

            Context.Entry(exist).CurrentValues.SetValues(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> GetById(Guid id, bool includeDeleted = false, params string[] includeProperties)
        {
            Expression<Func<TEntity,bool>> predicate = m => m.Id == id;
            
            return await GetSingle(predicate, includeDeleted, includeProperties);
        }

        protected async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var query = IncludeProperties(includeDeleted, includeProperties);

            var foundEntity = await query.SingleOrDefaultAsync(predicate);
            
            return foundEntity;
        }

        protected async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeleted = false,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            params string[] includeProperties)
        {
            var query = IncludeProperties(includeDeleted, includeProperties);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            var foundList = await query.ToListAsync();
            
            return foundList;
        }

        private IQueryable<TEntity> IncludeProperties(bool includeDeleteEntities = false,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (includeDeleteEntities)
            {
                query = query.IgnoreQueryFilters();
            }

            return query;
        }

        private IQueryable<TEntity> Include(params string[] includeProperties)
        {
            var query = _entities.AsQueryable();

            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}