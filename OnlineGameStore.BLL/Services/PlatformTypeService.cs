using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly ILogger<PlatformTypeService> _logger;
        private readonly INorthwindLogService _logService;
        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork,
            INorthwindLogService logService,
            ILogger<PlatformTypeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logService = logService;
            _logger = logger;
        }

        public IEnumerable<int> GetPlatformTypesIdsByNames(IEnumerable<string> types)
        {
            var platformTypesIds = _unitOfWork.PlatformTypes
                .GetMany(s => types.Contains(s.Type))
                .Select(s => s.Id);

            return platformTypesIds;
        }

        public bool CheckTypeForUnique(int platformTypeId, string type)
        {
            var platformType = _unitOfWork.PlatformTypes.GetSingle(pt => pt.Type == type, true);

            return platformType != null && platformType.Id != platformTypeId;
        }

        public PlatformType CreatePlatformType(PlatformType platformType)
        {
            var createdPlatformType = _unitOfWork.PlatformTypes.Create(platformType);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(CreatePlatformType)}.
                    Creating paltform type with id {createdPlatformType.Id} successfully", createdPlatformType);
            
            _logService.LogCreating(createdPlatformType);

            return createdPlatformType;
        }

        public void DeletePlatformType(int platformTypeId)
        {
            var platformType = _unitOfWork.PlatformTypes.GetSingle(pt => pt.Id == platformTypeId);

            if (platformType == null)
            {
                var exception = new InvalidOperationException("Platform type has not been found");

                _logger.LogError(exception,
                    $@"Class: {nameof(PlatformTypeService)}; Method: {nameof(DeletePlatformType)}.
                    Deleting platform type with id {platformTypeId} unsuccessfully", platformTypeId);

                throw exception;
            }

            _unitOfWork.PlatformTypes.Delete(platformType);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(DeletePlatformType)}.
                    Deleting platform type with id {platformTypeId} successfully", platformType);
            
            _logService.LogDeleting(platformType);
        }

        public PlatformType EditPlatformType(PlatformType platformType)
        {
            var oldPlatformType = GetPlatformTypeById(platformType.Id);
            
            var editedPlatformType = _unitOfWork.PlatformTypes.Update(platformType);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(EditPlatformType)}.
                    Editing platform type with id {editedPlatformType.Id} successfully", editedPlatformType);
            
            _logService.LogUpdating(oldPlatformType, editedPlatformType);

            return editedPlatformType;
        }

        public IEnumerable<PlatformType> GetAllPlatformTypes()
        {
            var platformTypes = _unitOfWork.PlatformTypes.GetMany(null,
                    false,
                    null,
                    null,
                    null,
                    $"{nameof(PlatformType.GamePlatformTypes)}.{nameof(GamePlatformType.Game)}");

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(GetAllPlatformTypes)}.
                    Receiving platform types successfully", platformTypes);

            return platformTypes;
        }

        public PlatformType GetPlatformTypeById(int platformTypeId)
        {
            var platformType = _unitOfWork.PlatformTypes.GetSingle(pt => pt.Id == platformTypeId,
                    false,
                    $"{nameof(PlatformType.GamePlatformTypes)}.{nameof(GamePlatformType.Game)}");

            _logger.LogDebug($@"Class: {nameof(PlatformTypeService)}; Method: {nameof(GetPlatformTypeById)}.
                    Receiving platform type with id {platformTypeId} successfully", platformType);

            return platformType;
        }
    }
}