using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

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

        public async Task<IEnumerable<string>> GetPlatformTypesIdsByNames(IEnumerable<string> types)
        {
            var platformTypesIds = await _platformTypeRepository.GetIdsByTypesAsync(types);

            return platformTypesIds;
        }

        public async Task<bool> CheckTypeForUnique(string platformTypeId, string type)
        {
            var platformType = await _platformTypeRepository.GetByTypeAsync(type, includeDeleted: true);

            return platformType != null && platformType.Id != platformTypeId;
        }

        public async Task<PlatformTypeModel> CreatePlatformType(PlatformTypeModel platformType)
        {
           await _platformTypeRepository.CreateAsync(platformType);

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(CreatePlatformType)}.
                    Creating paltform type with id {platformType.Id} successfully", platformType);

            return platformType;
        }

        public async Task DeletePlatformType(string platformTypeId)
        {
            var platformType = await _platformTypeRepository.GetByIdAsync(platformTypeId);

            if (platformType == null)
            {
                var exception = new InvalidOperationException("Platform type has not been found");

                _logger.LogError(exception,
                    $@"Class: {nameof(PlatformTypeService)}; Method: {nameof(DeletePlatformType)}.
                    Deleting platform type with id {platformTypeId} unsuccessfully", platformTypeId);

                throw exception;
            }

            await _platformTypeRepository.DeleteAsync(platformType);

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(DeletePlatformType)}.
                    Deleting platform type with id {platformTypeId} successfully", platformType);
        }

        public async Task<PlatformTypeModel> EditPlatformType(PlatformTypeModel platformType)
        {
            await _platformTypeRepository.UpdateAsync(platformType);

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(EditPlatformType)}.
                    Editing platform type with id {platformType.Id} successfully", platformType);

            return platformType;
        }

        public async Task<IEnumerable<PlatformTypeModel>> GetAllPlatformTypes()
        {
            var platformTypes = await _platformTypeRepository.GetAllAsync();

            return platformTypes;
        }

        public async Task<PlatformTypeModel> GetPlatformTypeById(string platformTypeId)
        {
            var platformType = await _platformTypeRepository.GetByIdAsync(platformTypeId);

            return platformType;
        }
    }
}