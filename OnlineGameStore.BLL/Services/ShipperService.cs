using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IGeneralShipperRepository _shipperRepository;

        public ShipperService(IGeneralShipperRepository shipperRepository)
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