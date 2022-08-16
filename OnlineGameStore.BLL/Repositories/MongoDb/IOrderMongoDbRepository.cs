using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IOrderMongoDbRepository : IMongoDbRepository<OrderMongoDbEntity>
    {
        Task<IEnumerable<OrderMongoDbEntity>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);
    }
}