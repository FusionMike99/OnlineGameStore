using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.DAL.Utils;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindOrderRepository : NorthwindGenericRepository<NorthwindOrder>, INorthwindOrderRepository
    {
        private readonly INorthwindOrderDetailRepository _orderDetailRepository;
        private readonly INorthwindShipperRepository _shipperRepository;
        
        public NorthwindOrderRepository(IMongoDatabase database,
            INorthwindOrderDetailRepository orderDetailRepository,
            INorthwindShipperRepository shipperRepository) : base(database)
        {
            _orderDetailRepository = orderDetailRepository;
            _shipperRepository = shipperRepository;
        }

        public async Task<IEnumerable<NorthwindOrder>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var predicate = OrderPredicate.GetPredicate<NorthwindOrder>(filterOrderModel);

            var orders = await GetMany(predicate);
            var orderList = orders.ToList();
            
            orderList.ForEach(SetOrderDetailAndShipper);

            return orderList;
        }

        private async void SetOrderDetailAndShipper(NorthwindOrder o)
        {
            o.OrderDetails = await _orderDetailRepository.GetManyByOrderId(o.OrderId);
            o.Shipper = await _shipperRepository.GetByShipperId(o.ShipVia);
        }
    }
}