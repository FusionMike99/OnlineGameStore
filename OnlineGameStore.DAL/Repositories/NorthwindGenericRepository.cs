using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Utils;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories
{
    public class NorthwindGenericRepository<TEntity, TKey> : INorthwindGenericRepository<TEntity, TKey>
        where TEntity : class, INorthwindBaseEntity<TKey>
    {
        private readonly IMongoCollection<TEntity> _collection;

        public NorthwindGenericRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            _collection.InsertOne(entity);

            return entity;
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _collection.AsQueryable();

            var foundEntity = query.FirstOrDefault(predicate);

            return foundEntity;
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null,
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
            
            var foundList = query.ToList();
            
            return foundList;
        }

        public TEntity Update(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Where(predicate);
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
                
            _collection.UpdateMany(filter, update);

            return entity;
        }
    }
}