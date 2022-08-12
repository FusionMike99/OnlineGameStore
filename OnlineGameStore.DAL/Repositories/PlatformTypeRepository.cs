using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;

namespace OnlineGameStore.DAL.Repositories
{
    public class PlatformTypeRepository : IPlatformTypeRepository
    {
        private readonly IGameStorePlatformTypeRepository _platformTypeRepository;
        private readonly IMapper _mapper;

        public PlatformTypeRepository(IGameStorePlatformTypeRepository platformTypeRepository,
            IMapper mapper)
        {
            _platformTypeRepository = platformTypeRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);

            var createdPlatformType = await _platformTypeRepository.Create(platformType);

            platformTypeModel.Id = createdPlatformType.Id;
        }

        public async Task UpdateAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);

            await _platformTypeRepository.Update(platformType);
        }

        public async Task DeleteAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);

            await _platformTypeRepository.Delete(platformType);
        }

        public async Task<PlatformTypeModel> GetByIdAsync(Guid id, bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformType = await _platformTypeRepository.GetById(id, includeDeleted, includeProperties);
            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);

            return mappedPlatformType;
        }

        public async Task<PlatformTypeModel> GetByTypeAsync(string type,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformType = await _platformTypeRepository.GetByType(type, includeDeleted, includeProperties);
            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);

            return mappedPlatformType;
        }

        public async Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types)
        {
            return await _platformTypeRepository.GetIdsByTypes(types);
        }

        public async Task<IEnumerable<PlatformTypeModel>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformTypes = await _platformTypeRepository.GetAll(includeDeleted, includeProperties);
            var mappedPlatformTypes = _mapper.Map<IEnumerable<PlatformTypeModel>>(platformTypes);

            return mappedPlatformTypes;
        }
    }
}