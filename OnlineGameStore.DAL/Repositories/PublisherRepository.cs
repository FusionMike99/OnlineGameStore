using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.MongoDb;
using OnlineGameStore.BLL.Repositories.SqlServer;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.DAL.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly IPublisherSqlServerRepository _publisherSqlServerRepository;
        private readonly ISupplierMongoDbRepository _supplierMongoDbRepository;
        private readonly IMapper _mapper;

        public PublisherRepository(IPublisherSqlServerRepository publisherSqlServerRepository,
            ISupplierMongoDbRepository supplierMongoDbRepository,
            IMapper mapper)
        {
            _publisherSqlServerRepository = publisherSqlServerRepository;
            _supplierMongoDbRepository = supplierMongoDbRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PublisherModel publisherModel)
        {
            var publisher = _mapper.Map<PublisherEntity>(publisherModel);
            var createdPublisher = await _publisherSqlServerRepository.CreateAsync(publisher);

            publisherModel.Id = createdPublisher.Id;
        }

        public async Task UpdateAsync(PublisherModel publisherModel)
        {
            var publisher = _mapper.Map<PublisherEntity>(publisherModel);
            await _publisherSqlServerRepository.UpdateAsync(publisher);
        }

        public async Task DeleteAsync(PublisherModel publisherModel)
        {
            var publisher = _mapper.Map<PublisherEntity>(publisherModel);
            await _publisherSqlServerRepository.DeleteAsync(publisher);
        }

        public async Task<PublisherModel> GetByNameAsync(string companyName, bool includeDeleted = false)
        {
            PublisherModel publisherModel;
            
            var publisherTask = _publisherSqlServerRepository.GetByNameAsync(companyName, includeDeleted);
            var supplierTask = _supplierMongoDbRepository.GetByNameAsync(companyName);
            await Task.WhenAll(publisherTask, supplierTask);

            var publisher = await publisherTask;
            
            if (publisher != null)
            {
                publisherModel = _mapper.Map<PublisherModel>(publisher);
            }
            else
            {
                var supplier = await supplierTask;

                if (supplier == null)
                {
                    return null;
                }
                
                publisherModel = _mapper.Map<PublisherModel>(supplier);
            }

            return publisherModel;
        }

        public async Task<PublisherModel> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var publisher = await _publisherSqlServerRepository.GetByIdAsync(id, includeDeleted);
            var publisherModel = _mapper.Map<PublisherModel>(publisher);

            return publisherModel;
        }

        public async Task<IEnumerable<string>> GetSuppliersIdsByNamesAsync(IEnumerable<string> companiesNames)
        {
            return await _supplierMongoDbRepository.GetIdsByNamesAsync(companiesNames);
        }

        public async Task<IEnumerable<PublisherModel>> GetAllAsync()
        {
            var publishersTask = _publisherSqlServerRepository.GetAllAsync();
            var suppliersTask = _supplierMongoDbRepository.GetAllAsync();
            await Task.WhenAll(publishersTask, suppliersTask);

            var publishers = await publishersTask;
            var suppliers = await suppliersTask;
            var unionPublishersSuppliers = UnionPublishersSuppliers(publishers, suppliers);

            return unionPublishersSuppliers;
        }
        
        private IEnumerable<PublisherModel> UnionPublishersSuppliers(IEnumerable<PublisherEntity> publishers,
            IEnumerable<NorthwindSupplier> suppliers)
        {
            var mappedPublishers = _mapper.Map<IEnumerable<PublisherModel>>(publishers);
            var mappedProducts = _mapper.Map<IEnumerable<PublisherModel>>(suppliers);
            var result = mappedPublishers.Concat(mappedProducts).DistinctBy(g => g.CompanyName);

            return result;
        }
    }
}