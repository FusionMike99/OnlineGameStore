using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services.Interfaces
{
    public interface IPublisherService
    {
        Task<PublisherModel> CreatePublisherAsync(PublisherModel publisher);

        Task<PublisherModel> EditPublisherAsync(PublisherModel publisher);

        Task DeletePublisherAsync(Guid publisherId);

        Task<PublisherModel> GetPublisherByCompanyNameAsync(string companyName);
        
        Task<PublisherModel> GetPublisherByIdAsync(Guid publisherId);
        
        Task<IEnumerable<string>> GetSuppliersIdsByNamesAsync(IEnumerable<string> companiesNames);

        Task<IEnumerable<PublisherModel>> GetAllPublishersAsync();

        Task<bool> CheckCompanyNameForUniqueAsync(Guid publisherId, string companyName);
    }
}