﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
            ILoggerFactory loggerFactory,
            INorthwindOrderDetailRepository orderDetailRepository,
            INorthwindShipperRepository shipperRepository) : base(database, loggerFactory)
        {
            _orderDetailRepository = orderDetailRepository;
            _shipperRepository = shipperRepository;
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
            o.OrderDetails = await _orderDetailRepository.GetManyByOrderIdAsync(o.OrderId);
            o.Shipper = await _shipperRepository.GetByShipperIdAsync(o.ShipVia);
        }
    }
}