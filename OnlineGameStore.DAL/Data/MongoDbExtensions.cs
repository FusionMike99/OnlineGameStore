using System;
using System.Collections.Generic;
using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.DAL.Data
{
    public static class MongoDbExtensions
    {
        private static IDictionary<Type, string> CollectionNames { get; }

        static MongoDbExtensions()
        {
            CollectionNames = new Dictionary<Type, string>
            {
                [typeof(NorthwindCategory)] = "categories",
                [typeof(NorthwindOrderDetail)] = "order-details",
                [typeof(NorthwindOrder)] = "orders",
                [typeof(NorthwindProduct)] = "products",
                [typeof(NorthwindShipper)] = "shippers",
                [typeof(NorthwindSupplier)] = "suppliers",
                [typeof(LogModel)] = "logs"
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