using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories.SqlServer;
using OnlineGameStore.BLL.Utils;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public abstract class SqlServerRepository<TEntity> : ISqlServerRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly StoreDbContext Context;
        private readonly DbSet<TEntity> _entities;
        private readonly ILogger<SqlServerRepository<TEntity>> _logger;

        protected SqlServerRepository(StoreDbContext context, ILoggerFactory logger)
        {
            Context = context;
            _logger = logger.CreateLogger<SqlServerRepository<TEntity>>();

            _entities = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await _entities.AddAsync(entity);
            await Context.SaveChangesAsync();

            _logger.LogDebug("Action: {Action}\nEntity Type: {EntityType}\nObject: {@Object}", ActionTypes.Create,
                typeof(TEntity), entity);
            
            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            await UpdateAsync(entity);
            
            _logger.LogDebug("Action: {Action}\nEntity Type: {EntityType}\nObject: {@Object}", ActionTypes.Delete,
                typeof(TEntity), entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false, params string[] includeProperties)
        {
            var foundEntities = await IncludeProperties(includeDeleted, includeProperties).ToListAsync();
            
            return foundEntities;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var exist = await _entities.FindAsync(entity.Id);
            var oldEntity = exist.DeepClone();

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
            
            _logger.LogInformation("Action: {Action}\nEntity Type: {EntityType}\nOld Object: {@OldObject}\nNew Object: {@NewObject}",
                ActionTypes.Update, typeof(TEntity), oldEntity, entity);

            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool includeDeleted = false, params string[] includeProperties)
        {
            Expression<Func<TEntity,bool>> predicate = m => m.Id == id;
            var foundEntity = await IncludeProperties(includeDeleted, includeProperties)
                .SingleOrDefaultAsync(predicate);
            
            return foundEntity;
        }

        /*protected async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var query = IncludeProperties(includeDeleted, includeProperties);

            var foundEntity = await query.SingleOrDefaultAsync(predicate);
            
            return foundEntity;
        }

        protected async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var query = IncludeProperties(includeDeleted, includeProperties);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var foundList = await query.ToListAsync();
            
            return foundList;
        }*/

        protected IQueryable<TEntity> IncludeProperties(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var query = Include(includeProperties);

            if (includeDeleted)
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