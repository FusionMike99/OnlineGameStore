﻿using System;
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
        private readonly IGeneralPublisherRepository _publisherRepository;

        public PublisherService(ILogger<PublisherService> logger,
            IGeneralPublisherRepository publisherRepository)
        {
            _logger = logger;
            _publisherRepository = publisherRepository;
        }

        public async Task<bool> CheckCompanyNameForUnique(string publisherId, string companyName)
        {
            var publisher = await _publisherRepository.GetByNameAsync(companyName, includeDeleted: true);

            return publisher != null && publisher.Id != publisherId;
        }

        public async Task<PublisherModel> CreatePublisher(PublisherModel publisher)
        {
            await _publisherRepository.CreateAsync(publisher);

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(CreatePublisher)}.
                    Creating publisher with id {publisher.Id} successfully", publisher);

            return publisher;
        }

        public async Task DeletePublisher(string publisherId)
        {
            var publisher = await _publisherRepository.GetByIdAsync(publisherId);

            if (publisher == null)
            {
                var exception = new InvalidOperationException("Publisher has not been found");

                _logger.LogError(exception, $@"Class: {nameof(PublisherService)}; Method: {nameof(DeletePublisher)}.
                    Deleting publisher with id {publisherId} unsuccessfully", publisherId);

                throw exception;
            }

            if (publisher.DatabaseEntity is DatabaseEntity.Northwind)
            {
                throw new InvalidOperationException("You cannot delete Northwind suppliers");
            }
            
            await _publisherRepository.DeleteAsync(publisher);

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(DeletePublisher)}.
                    Deleting publisher with id {publisherId} successfully", publisher);
        }

        public async Task<PublisherModel> EditPublisher(string companyName, PublisherModel publisher)
        {
            if (publisher.DatabaseEntity is DatabaseEntity.Northwind)
            {
                throw new InvalidOperationException("You cannot edit Northwind suppliers");
            }

            await _publisherRepository.UpdateAsync(publisher);

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(EditPublisher)}.
                    Editing publisher with id {publisher.Id} successfully", publisher);

            return publisher;
        }

        public async Task<IEnumerable<string>> GetSuppliersIdsByNames(IEnumerable<string> companiesNames)
        {
            return await _publisherRepository.GetSuppliersIdsByNamesAsync(companiesNames);
        }

        public async Task<IEnumerable<PublisherModel>> GetAllPublishers()
        {
            var publishers = await _publisherRepository.GetAllAsync();

            return publishers;
        }

        public async Task<PublisherModel> GetPublisherByCompanyName(string companyName)
        {
            var publisher = await _publisherRepository.GetByNameAsync(companyName);

            return publisher;
        }
    }
}