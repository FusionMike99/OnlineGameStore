using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly INorthwindShipperRepository _shipperRepository;
        private readonly IMapper _mapper;

        public ShipperRepository(INorthwindShipperRepository shipperRepository,
            IMapper mapper)
        {
            _shipperRepository = shipperRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipperModel>> GetAllAsync()
        {
            var shippers = await _shipperRepository.GetAll();

            var mappedShippers = _mapper.Map<IEnumerable<ShipperModel>>(shippers);

            return mappedShippers;
        }
    }
}