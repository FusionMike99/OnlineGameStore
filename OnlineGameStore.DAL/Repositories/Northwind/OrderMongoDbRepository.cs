using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories.MongoDb;
using OnlineGameStore.DAL.Utils;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class OrderMongoDbRepository : MongoDbRepository<NorthwindOrder>, IOrderMongoDbRepository
    {
        private readonly IOrderDetailMongoDbRepository _orderDetailMongoDbRepository;
        private readonly IShipperMongoDbRepository _shipperMongoDbRepository;

        public OrderMongoDbRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory,
            IOrderDetailMongoDbRepository orderDetailMongoDbRepository,
            IShipperMongoDbRepository shipperMongoDbRepository) : base(database, loggerFactory)
        {
            _orderDetailMongoDbRepository = orderDetailMongoDbRepository;
            _shipperMongoDbRepository = shipperMongoDbRepository;
        }

        public async Task<IEnumerable<NorthwindOrder>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var predicate = OrderPredicate.GetPredicate<NorthwindOrder>(filterOrderModel);
            var orders = await Query.Where(predicate).ToListAsync();
            orders.ForEach(SetOrderDetailAndShipper);

            return orders;
        }

        private async void SetOrderDetailAndShipper(NorthwindOrder o)
        {
            o.OrderDetails = await _orderDetailMongoDbRepository.GetManyByOrderIdAsync(o.OrderId);
            o.Shipper = await _shipperMongoDbRepository.GetByShipperIdAsync(o.ShipVia);
        }
    }
}