using System;
using System.Collections.Generic;
using MongoDB.Driver;
using OnlineGameStore.DAL.Entities.Northwind;

namespace OnlineGameStore.DAL.Data
{
    public static class MongoDbExtensions
    {
        private static IDictionary<Type, string> CollectionNames { get; }

        static MongoDbExtensions()
        {
            CollectionNames = new Dictionary<Type, string>
            {
                [typeof(CategoryEntity)] = "categories",
                [typeof(OrderDetailMongoDbEntity)] = "order-details",
                [typeof(OrderMongoDbEntity)] = "orders",
                [typeof(ProductEntity)] = "products",
                [typeof(ShipperEntity)] = "shippers",
                [typeof(SupplierEntity)] = "suppliers"
            };
        }

        public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase database)
        {
            var type = typeof(T);

            if (!CollectionNames.ContainsKey(type))
            {
                throw new ArgumentException("Collection of this type doesn't exist", type.Name);
            }

            var collectionName = CollectionNames[type];
            var mongoCollection = database.GetCollection<T>(collectionName);

            return mongoCollection;
        }
    }
}