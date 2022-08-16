using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly ILogger<PlatformTypeService> _logger;
        private readonly IPlatformTypeRepository _platformTypeRepository;

        public PlatformTypeService(ILogger<PlatformTypeService> logger,
            IPlatformTypeRepository platformTypeRepository)
        {
            _logger = logger;
            _platformTypeRepository = platformTypeRepository;
        }

        public async Task<IEnumerable<string>> GetPlatformTypesIdsByNamesAsync(IEnumerable<string> types)
        {
            var platformTypesIds = await _platformTypeRepository.GetIdsByTypesAsync(types);

            return platformTypesIds;
        }

        public async Task<bool> CheckTypeForUniqueAsync(Guid platformTypeId, string type)
        {
            var platformType = await _platformTypeRepository.GetByTypeIncludeDeletedAsync(type);

            return platformType != null && platformType.Id != platformTypeId;
        }

        public async Task<PlatformTypeModel> CreatePlatformTypeAsync(PlatformTypeModel platformType)
        {
           await _platformTypeRepository.CreateAsync(platformType);

            return platformType;
        }

        public async Task DeletePlatformTypeAsync(Guid platformTypeId)
        {
            var platformType = await _platformTypeRepository.GetByIdAsync(platformTypeId);

            if (platformType == null)
            {
                _logger.LogError("Deleting platform type with id {PlatformTypeId} unsuccessfully", platformTypeId);
                throw new InvalidOperationException("Platform type has not been found");
            }

            await _platformTypeRepository.DeleteAsync(platformType);
        }

        public async Task<PlatformTypeModel> EditPlatformTypeAsync(PlatformTypeModel platformType)
        {
            await _platformTypeRepository.UpdateAsync(platformType);

            return platformType;
        }

        public async Task<IEnumerable<PlatformTypeModel>> GetAllPlatformTypesAsync()
        {
            var platformTypes = await _platformTypeRepository.GetAllAsync();

            return platformTypes;
        }

        public async Task<PlatformTypeModel> GetPlatformTypeByIdAsync(Guid platformTypeId)
        {
            var platformType = await _platformTypeRepository.GetByIdAsync(platformTypeId);

            return platformType;
        }
    }
}