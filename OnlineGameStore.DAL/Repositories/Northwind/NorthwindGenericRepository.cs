using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Utils;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindGenericRepository<TEntity> : INorthwindGenericRepository<TEntity>
        where TEntity : NorthwindBaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public NorthwindGenericRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TEntity>();
        }

        public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _collection.AsQueryable();

            var foundEntity = await query.FirstOrDefaultAsync(predicate);

            return foundEntity;
        }

        public async Task<IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null)
        {
            var query = _collection.AsQueryable();
            
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = (IOrderedMongoQueryable<TEntity>)orderBy(query);
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

        public virtual async Task<TEntity> GetById(ObjectId id)
        {
            Expression<Func<TEntity, bool>> predicate = m => m.Id == id;

            return await GetFirst(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await GetMany();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
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

            return entity;
        }
    }
}