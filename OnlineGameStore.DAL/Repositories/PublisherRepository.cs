using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.DAL.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly IGameStorePublisherRepository _publisherRepository;
        private readonly INorthwindSupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public PublisherRepository(IGameStorePublisherRepository publisherRepository,
            INorthwindSupplierRepository supplierRepository,
            IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PublisherModel publisherModel)
        {
            var publisher = _mapper.Map<PublisherEntity>(publisherModel);

            var createdPublisher = await _publisherRepository.Create(publisher);

            publisherModel.Id = createdPublisher.Id;
        }

        public async Task UpdateAsync(PublisherModel publisherModel)
        {
            var publisher = _mapper.Map<PublisherEntity>(publisherModel);

            await _publisherRepository.Update(publisher);
        }

        public async Task DeleteAsync(PublisherModel publisherModel)
        {
            var publisher = _mapper.Map<PublisherEntity>(publisherModel);

            await _publisherRepository.Delete(publisher);
        }

        public async Task<PublisherModel> GetByNameAsync(string companyName, bool includeDeleted = false)
        {
            PublisherModel publisherModel;
            var publisher = await _publisherRepository.GetByName(companyName, includeDeleted);

            if (publisher != null)
            {
                publisherModel = _mapper.Map<PublisherModel>(publisher);
            }
            else
            {
                var supplier = await _supplierRepository.GetByName(companyName);

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
            var publisher = await _publisherRepository.GetById(id, includeDeleted);
            var publisherModel = _mapper.Map<PublisherModel>(publisher);

            return publisherModel;
        }

        public async Task<IEnumerable<string>> GetSuppliersIdsByNamesAsync(IEnumerable<string> companiesNames)
        {
            return await _supplierRepository.GetIdsByNames(companiesNames);
        }

        public async Task<IEnumerable<PublisherModel>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAll();

            var suppliers = await _supplierRepository.GetAll();

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