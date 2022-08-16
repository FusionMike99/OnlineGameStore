using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Repositories.MongoDb.Interfaces
{
    public interface IOrderMongoDbRepository : IMongoDbRepository<OrderMongoDbEntity>
    {
        Task<IEnumerable<OrderMongoDbEntity>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);
    }
}