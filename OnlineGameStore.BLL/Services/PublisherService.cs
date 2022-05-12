using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly ILogger<PublisherService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork, ILogger<PublisherService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public bool CheckCompanyNameForUnique(int publisherId, string companyName)
        {
            var publisher = _unitOfWork.Publishers.GetSingle(g => g.CompanyName == companyName);

            return publisher != null && publisher.Id != publisherId;
        }

        public Publisher CreatePublisher(Publisher publisher)
        {
            var createdPublisher = _unitOfWork.Publishers.Create(publisher);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(CreatePublisher)}.
                    Creating publisher with id {createdPublisher.Id} successfully", createdPublisher);

            return createdPublisher;
        }

        public void DeletePublisher(int publisherId)
        {
            var publisher = _unitOfWork.Publishers.GetSingle(p => p.Id == publisherId);

            if (publisher == null)
            {
                var exception = new InvalidOperationException("Publisher has not been found");

                _logger.LogError(exception, $@"Class: {nameof(PublisherService)}; Method: {nameof(DeletePublisher)}.
                    Deleting publisher with id {publisherId} unsuccessfully", publisherId);

                throw exception;
            }

            _unitOfWork.Publishers.Delete(publisher);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(DeletePublisher)}.
                    Deleting publisher with id {publisherId} successfully", publisher);
        }

        public Publisher EditPublisher(Publisher publisher)
        {
            var editedPublisher = _unitOfWork.Publishers.Update(publisher);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(EditPublisher)}.
                    Editing publisher with id {editedPublisher.Id} successfully", editedPublisher);

            return editedPublisher;
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {
            var publishers = _unitOfWork.Publishers.GetMany(null,
                    false,
                    $"{nameof(Publisher.Games)}");

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(GetAllPublishers)}.
                    Receiving publishers successfully", publishers);

            return publishers;
        }

        public Publisher GetPublisherByCompanyName(string companyName)
        {
            var publisher = _unitOfWork.Publishers.GetSingle(p => p.CompanyName == companyName,
                    false,
                    $"{nameof(Publisher.Games)}");

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(GetPublisherByCompanyName)}.
                    Receiving publisher with company name {companyName} successfully", publisher);

            return publisher;
        }
    }
}