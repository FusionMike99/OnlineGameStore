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

            var createdPlatformType = await _platformTypeRepository.CreateAsync(platformType);

            platformTypeModel.Id = createdPlatformType.Id;
        }

        public async Task UpdateAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);

            await _platformTypeRepository.UpdateAsync(platformType);
        }

        public async Task DeleteAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);

            await _platformTypeRepository.DeleteAsync(platformType);
        }

        public async Task<PlatformTypeModel> GetByIdAsync(Guid id, bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformType = await _platformTypeRepository.GetByIdAsync(id, includeDeleted, includeProperties);
            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);

            return mappedPlatformType;
        }

        public async Task<PlatformTypeModel> GetByTypeAsync(string type,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformType = await _platformTypeRepository.GetByTypeAsync(type, includeDeleted, includeProperties);
            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);

            return mappedPlatformType;
        }

        public async Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types)
        {
            return await _platformTypeRepository.GetIdsByTypesAsync(types);
        }

        public async Task<IEnumerable<PlatformTypeModel>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformTypes = await _platformTypeRepository.GetAllAsync(includeDeleted, includeProperties);
            var mappedPlatformTypes = _mapper.Map<IEnumerable<PlatformTypeModel>>(platformTypes);

            return mappedPlatformTypes;
        }
    }
}