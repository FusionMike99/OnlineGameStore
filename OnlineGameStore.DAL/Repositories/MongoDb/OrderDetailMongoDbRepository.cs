using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;

namespace OnlineGameStore.DAL.Repositories.MongoDb
{
    public class OrderDetailMongoDbRepository : MongoDbRepository<OrderDetailMongoDbEntity>,
        IOrderDetailMongoDbRepository
    {
        public OrderDetailMongoDbRepository(IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(database, loggerFactory)
        {
        }

        public async Task<IEnumerable<OrderDetailMongoDbEntity>> GetManyByOrderIdAsync(int orderId)
        {
            var orderDetails = await Query.Where(od => od.OrderId == orderId).ToListAsync();

            return orderDetails;
        }
    }
}