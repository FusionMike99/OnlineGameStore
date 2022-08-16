using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<IEnumerable<ShipperModel>> GetAllShippersAsync()
        {
            var shippers = await _shipperRepository.GetAllAsync();

            return shippers;
        }
    }
}