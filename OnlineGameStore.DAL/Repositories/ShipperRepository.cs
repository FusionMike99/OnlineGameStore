using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly IShipperMongoDbRepository _shipperMongoDbRepository;
        private readonly IMapper _mapper;

        public ShipperRepository(IShipperMongoDbRepository shipperMongoDbRepository,
            IMapper mapper)
        {
            _shipperMongoDbRepository = shipperMongoDbRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipperModel>> GetAllAsync()
        {
            var shippers = await _shipperMongoDbRepository.GetAllAsync();
            var mappedShippers = _mapper.Map<IEnumerable<ShipperModel>>(shippers);

            return mappedShippers;
        }
    }
}