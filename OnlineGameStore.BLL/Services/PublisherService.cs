using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly ILogger<PublisherService> _logger;
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(ILogger<PublisherService> logger,
            IPublisherRepository publisherRepository)
        {
            _logger = logger;
            _publisherRepository = publisherRepository;
        }

        public async Task<bool> CheckCompanyNameForUniqueAsync(Guid publisherId, string companyName)
        {
            var publisher = await _publisherRepository.GetByNameAsync(companyName, includeDeleted: true);

            return publisher != null && publisher.Id != publisherId;
        }

        public async Task<PublisherModel> CreatePublisherAsync(PublisherModel publisher)
        {
            await _publisherRepository.CreateAsync(publisher);

            return publisher;
        }

        public async Task DeletePublisherAsync(Guid publisherId)
        {
            var publisher = await _publisherRepository.GetByIdAsync(publisherId);

            if (publisher == null)
            {
                var exception = new InvalidOperationException("Publisher has not been found");

                _logger.LogError(exception, @"Service: {Service}; Method: {Method}.
                    Deleting publisher with id {PublisherId} unsuccessfully", nameof(PublisherService),
                    nameof(DeletePublisherAsync), publisherId);

                throw exception;
            }

            if (publisher.DatabaseEntity is DatabaseEntity.Northwind)
            {
                throw new InvalidOperationException("You cannot delete Northwind suppliers");
            }
            
            await _publisherRepository.DeleteAsync(publisher);
        }

        public async Task<PublisherModel> EditPublisherAsync(PublisherModel publisher)
        {
            if (publisher.DatabaseEntity is DatabaseEntity.Northwind)
            {
                throw new InvalidOperationException("You cannot edit Northwind suppliers");
            }

            await _publisherRepository.UpdateAsync(publisher);

            return publisher;
        }

        public async Task<IEnumerable<string>> GetSuppliersIdsByNamesAsync(IEnumerable<string> companiesNames)
        {
            return await _publisherRepository.GetSuppliersIdsByNamesAsync(companiesNames);
        }

        public async Task<IEnumerable<PublisherModel>> GetAllPublishersAsync()
        {
            var publishers = await _publisherRepository.GetAllAsync();

            return publishers;
        }

        public async Task<PublisherModel> GetPublisherByCompanyNameAsync(string companyName)
        {
            var publisher = await _publisherRepository.GetByNameAsync(companyName);

            return publisher;
        }
    }
}