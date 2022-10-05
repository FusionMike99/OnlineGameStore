﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.ExtensionsUtility.Extensions;

namespace OnlineGameStore.DAL.Repositories.SqlServer
{
    public abstract class SqlServerRepository<TEntity> : ISqlServerRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly StoreDbContext Context;
        protected readonly DbSet<TEntity> Entities;
        private readonly ILogger<SqlServerRepository<TEntity>> _logger;

        protected SqlServerRepository(StoreDbContext context, ILoggerFactory logger)
        {
            Context = context;
            _logger = logger.CreateLogger<SqlServerRepository<TEntity>>();

            Entities = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await Entities.AddAsync(entity);
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

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var foundEntities = await Entities.ToListAsync();
            
            return foundEntities;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var exist = await Entities.FindAsync(entity.Id);
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

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var foundEntity = await Entities.FirstOrDefaultAsync(m => m.Id == id);
            
            return foundEntity;
        }
    }
}