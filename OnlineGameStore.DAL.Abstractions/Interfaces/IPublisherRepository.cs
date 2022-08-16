using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Abstractions.Interfaces
{
    public interface IPublisherRepository
    {
        Task CreateAsync(PublisherModel publisherModel);

        Task UpdateAsync(PublisherModel publisherModel);

        Task DeleteAsync(PublisherModel publisherModel);
        
        Task<PublisherModel> GetByNameAsync(string companyName);
        
        Task<PublisherModel> GetByNameIncludeDeletedAsync(string companyName);
        
        Task<PublisherModel> GetByIdAsync(Guid id);

        Task<IEnumerable<string>> GetSuppliersIdsByNamesAsync(IEnumerable<string> companiesNames);

        Task<IEnumerable<PublisherModel>> GetAllAsync();
    }
}