using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;
using OnlineGameStore.DAL.Utils;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Repositories.MongoDb
{
    public class OrderMongoDbRepository : MongoDbRepository<OrderMongoDbEntity>, IOrderMongoDbRepository
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

        public async Task<IEnumerable<OrderMongoDbEntity>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var orders = Query;
            
            var predicate = OrderPredicate.GetPredicate<OrderMongoDbEntity>(filterOrderModel);
            if (predicate != null)
            {
                orders = orders.Where(predicate);
            }
            
            var ordersList = await orders.ToListAsync();
            ordersList.ForEach(SetOrderDetailAndShipper);

            return ordersList;
        }

        private async void SetOrderDetailAndShipper(OrderMongoDbEntity o)
        {
            o.OrderDetails = await _orderDetailMongoDbRepository.GetManyByOrderIdAsync(o.OrderId);
            o.Shipper = await _shipperMongoDbRepository.GetByShipperIdAsync(o.ShipVia);
        }
    }
}