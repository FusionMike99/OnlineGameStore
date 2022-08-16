using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories.MongoDb;
using OnlineGameStore.BLL.Utils;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public abstract class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity>
        where TEntity : MongoBaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        protected readonly IMongoQueryable<TEntity> Query;
        private readonly ILogger<MongoDbRepository<TEntity>> _logger;

        protected MongoDbRepository(IMongoDatabase database, ILoggerFactory loggerFactory)
        {
            _collection = database.GetCollection<TEntity>();
            Query = _collection.AsQueryable();
            _logger = loggerFactory.CreateLogger<MongoDbRepository<TEntity>>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var foundList = await Query.ToListAsync();

            return foundList;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(m => m.Id, entity.Id);
            var updateBuilder = Builders<TEntity>.Update;
            var updateDefinitions = new List<UpdateDefinition<TEntity>>();
            var existEntity = await _collection.Find(filter).FirstOrDefaultAsync();
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