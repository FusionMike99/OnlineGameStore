using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IPublisherRepository
    {
        Task CreateAsync(PublisherModel publisherModel);

        Task UpdateAsync(PublisherModel publisherModel);

        Task DeleteAsync(PublisherModel publisherModel);
        
        Task<PublisherModel> GetByNameAsync(string companyName, bool includeDeleted = false);
        
        Task<PublisherModel> GetByIdAsync(Guid id, bool includeDeleted = false);

        Task<IEnumerable<string>> GetSuppliersIdsByNamesAsync(IEnumerable<string> companiesNames);

        Task<IEnumerable<PublisherModel>> GetAllAsync();
    }
}