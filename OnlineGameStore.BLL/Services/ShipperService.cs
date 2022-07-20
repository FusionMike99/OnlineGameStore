using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class ShipperService : IShipperService
    {
        private readonly INorthwindUnitOfWork _northwindUnitOfWork;
        private readonly ILogger<ShipperService> _logger;

        public ShipperService(INorthwindUnitOfWork northwindUnitOfWork,
            ILogger<ShipperService> logger)
        {
            _northwindUnitOfWork = northwindUnitOfWork;
            _logger = logger;
        }

        public IEnumerable<NorthwindShipper> GetAllShippers()
        {
            var shippers = _northwindUnitOfWork.Shippers.GetMany();

            _logger.LogDebug($@"Class: {nameof(ShipperService)}; Method: {nameof(GetAllShippers)}.
                    Receiving shippers successfully", shippers);

            return shippers;
        }
    }
}