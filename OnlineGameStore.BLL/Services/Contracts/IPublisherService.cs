using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPublisherService
    {
        Task<PublisherModel> CreatePublisherAsync(PublisherModel publisher);

        Task<PublisherModel> EditPublisherAsync(PublisherModel publisher);

        Task DeletePublisherAsync(Guid publisherId);

        Task<PublisherModel> GetPublisherByCompanyNameAsync(string companyName);
        
        Task<IEnumerable<string>> GetSuppliersIdsByNamesAsync(IEnumerable<string> companiesNames);

        Task<IEnumerable<PublisherModel>> GetAllPublishersAsync();

        Task<bool> CheckCompanyNameForUniqueAsync(Guid publisherId, string companyName);
    }
}