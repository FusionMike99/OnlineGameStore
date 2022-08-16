using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IOrderMongoDbRepository : IMongoDbRepository<NorthwindOrder>
    {
        Task<IEnumerable<NorthwindOrder>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);
    }
}