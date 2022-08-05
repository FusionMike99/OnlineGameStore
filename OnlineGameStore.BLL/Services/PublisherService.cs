using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly ILogger<PublisherService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INorthwindUnitOfWork _northwindUnitOfWork;
        private readonly INorthwindLogService _logService;
        private readonly IMapper _mapper;

        public PublisherService(ILogger<PublisherService> logger,
            IUnitOfWork unitOfWork,
            INorthwindUnitOfWork northwindUnitOfWork,
            INorthwindLogService logService,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _northwindUnitOfWork = northwindUnitOfWork;
            _logService = logService;
            _mapper = mapper;
        }

        public bool CheckCompanyNameForUnique(string publisherId, string companyName)
        {
            var publisherGuid = Guid.Parse(publisherId);
            var publisher = _unitOfWork.Publishers.GetSingle(g => g.CompanyName == companyName, true);

            return publisher != null && publisher.Id != publisherGuid;
        }

        public Publisher CreatePublisher(Publisher publisher)
        {
            var createdPublisher = _unitOfWork.Publishers.Create(publisher);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(CreatePublisher)}.
                    Creating publisher with id {createdPublisher.Id} successfully", createdPublisher);
            
            _logService.LogCreating(createdPublisher);

            return createdPublisher;
        }

        public void DeletePublisher(string publisherId)
        {
            var publisherGuid = Guid.Parse(publisherId);
            var publisher = _unitOfWork.Publishers.GetSingle(p => p.Id == publisherGuid);

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
            
            _unitOfWork.Publishers.Delete(publisher);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(DeletePublisher)}.
                    Deleting publisher with id {publisherId} successfully", publisher);
            
            _logService.LogDeleting(publisher);
        }

        public Publisher EditPublisher(string companyName, Publisher publisher)
        {
            if (publisher.DatabaseEntity is DatabaseEntity.Northwind)
            {
                throw new InvalidOperationException("You cannot edit Northwind suppliers");
            }

            var oldPublisher = GetPublisherByCompanyName(companyName);

            var editedPublisher = _unitOfWork.Publishers.Update(publisher);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(EditPublisher)}.
                    Editing publisher with id {editedPublisher.Id} successfully", editedPublisher);
            
            _logService.LogUpdating(oldPublisher, editedPublisher);

            return editedPublisher;
        }

        public IEnumerable<string> GetSuppliersIdsByNames(IEnumerable<string> companiesNames)
        {
            var suppliersIds = _northwindUnitOfWork.Suppliers
                .GetMany(s => companiesNames.Contains(s.CompanyName))
                .Select(s => s.Id.ToString());

            return suppliersIds;
        }

        public IEnumerable<Publisher> GetAllPublishers()
        {
            var publishers = _unitOfWork.Publishers.GetMany();

            var suppliers = _northwindUnitOfWork.Suppliers.GetMany();

            var unionPublishersSuppliers = UnionPublishersSuppliers(publishers, suppliers);

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(GetAllPublishers)}.
                    Receiving publishers successfully", unionPublishersSuppliers);

            return unionPublishersSuppliers;
        }

        public Publisher GetPublisherByCompanyName(string companyName)
        {
            var publisher = _unitOfWork.Publishers.GetSingle(p => p.CompanyName == companyName);
            
            if (publisher == null)
            {
                var supplier = _northwindUnitOfWork.Suppliers.GetFirst(p => p.CompanyName == companyName);

                if (supplier != null)
                {
                    publisher = _mapper.Map<Publisher>(supplier);
                    publisher.DatabaseEntity = DatabaseEntity.Northwind;
                }
            }

            _logger.LogDebug($@"Class: {nameof(PublisherService)}; Method: {nameof(GetPublisherByCompanyName)}.
                    Receiving publisher with company name {companyName} successfully", publisher);

            return publisher;
        }
        
        private IEnumerable<Publisher> UnionPublishersSuppliers(IEnumerable<Publisher> publishers,
            IEnumerable<NorthwindSupplier> suppliers)
        {
            var mappedProducts = _mapper.Map<List<Publisher>>(suppliers);
            
            mappedProducts.ForEach(p => p.DatabaseEntity = DatabaseEntity.Northwind);

            var result = publishers.Concat(mappedProducts).DistinctBy(g => g.CompanyName).ToList();

            return result;
        }
    }
}