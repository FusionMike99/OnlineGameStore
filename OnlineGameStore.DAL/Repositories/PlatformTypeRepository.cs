using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Repositories
{
    public class PlatformTypeRepository : IPlatformTypeRepository
    {
        private readonly IPlatformTypeSqlServerRepository _platformTypeSqlServerRepository;
        private readonly IMapper _mapper;

        public PlatformTypeRepository(IPlatformTypeSqlServerRepository platformTypeSqlServerRepository,
            IMapper mapper)
        {
            _platformTypeSqlServerRepository = platformTypeSqlServerRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);
            var createdPlatformType = await _platformTypeSqlServerRepository.CreateAsync(platformType);
            platformTypeModel.Id = createdPlatformType.Id;
        }

        public async Task UpdateAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);
            await _platformTypeSqlServerRepository.UpdateAsync(platformType);
        }

        public async Task DeleteAsync(PlatformTypeModel platformTypeModel)
        {
            var platformType = _mapper.Map<PlatformTypeEntity>(platformTypeModel);
            await _platformTypeSqlServerRepository.DeleteAsync(platformType);
        }

        public async Task<PlatformTypeModel> GetByIdAsync(Guid id, bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformType = await _platformTypeSqlServerRepository.GetByIdAsync(id, includeDeleted, includeProperties);
            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);

            return mappedPlatformType;
        }

        public async Task<PlatformTypeModel> GetByTypeAsync(string type,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformType = await _platformTypeSqlServerRepository.GetByTypeAsync(type, includeDeleted, includeProperties);
            var mappedPlatformType = _mapper.Map<PlatformTypeModel>(platformType);

            return mappedPlatformType;
        }

        public async Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types)
        {
            return await _platformTypeSqlServerRepository.GetIdsByTypesAsync(types);
        }

        public async Task<IEnumerable<PlatformTypeModel>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var platformTypes = await _platformTypeSqlServerRepository.GetAllAsync(includeDeleted, includeProperties);
            var mappedPlatformTypes = _mapper.Map<IEnumerable<PlatformTypeModel>>(platformTypes);

            return mappedPlatformTypes;
        }
    }
}