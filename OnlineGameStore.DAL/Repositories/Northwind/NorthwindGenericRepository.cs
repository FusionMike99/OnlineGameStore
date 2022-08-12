using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Utils;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public abstract class NorthwindGenericRepository<TEntity> : INorthwindGenericRepository<TEntity>
        where TEntity : NorthwindBaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        private readonly ILogger<NorthwindGenericRepository<TEntity>> _logger;

        protected NorthwindGenericRepository(IMongoDatabase database, ILoggerFactory loggerFactory)
        {
            _collection = database.GetCollection<TEntity>();
            _logger = loggerFactory.CreateLogger<NorthwindGenericRepository<TEntity>>();
        }

        protected async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _collection.AsQueryable();

            var foundEntity = await query.FirstOrDefaultAsync(predicate);

            return foundEntity;
        }

        protected async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = _collection.AsQueryable();
            
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            
            var foundList = await query.ToListAsync();
            
            return foundList;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetMany();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(m => m.Id, entity.Id);
            var updateBuilder = Builders<TEntity>.Update;
            var updateDefinitions = new List<UpdateDefinition<TEntity>>();

            var existEntity = _collection.Find(filter).FirstOrDefault();

            var entityType = entity.GetType();

            foreach (var propertyInfo in entityType.GetProperties())
            {
                var isAllow = propertyInfo.GetCustomAttributes(typeof(UpdatableAttribute), true)
                                  .FirstOrDefault() is UpdatableAttribute
                              && propertyInfo.PropertyType != typeof(ObjectId);
                
                var existEntityProperty = propertyInfo.GetValue(existEntity);
                var updatingEntityProperty = propertyInfo.GetValue(entity);

                if (!isAllow || updatingEntityProperty == null ||
                    updatingEntityProperty.Equals(existEntityProperty))
                {
                    continue;
                }
                
                var updateDefinition = updateBuilder.Set(propertyInfo.Name, updatingEntityProperty);
                updateDefinitions.Add(updateDefinition);
            }

            if (!updateDefinitions.Any())
            {
                return entity;
            }
            
            var update = updateBuilder.Combine(updateDefinitions);
                
            await _collection.UpdateOneAsync(filter, update);
            
            _logger.LogInformation("Action: {Action}\nEntity Type: {EntityType}\nOld Object: {@OldObject}\nNew Object: {@NewObject}",
                ActionTypes.Update, typeof(TEntity), existEntity, entity);

            return entity;
        }
    }
}