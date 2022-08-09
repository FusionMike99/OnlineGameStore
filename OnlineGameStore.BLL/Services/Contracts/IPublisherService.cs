using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPublisherService
    {
        Task<PublisherModel> CreatePublisher(PublisherModel publisher);

        Task<PublisherModel> EditPublisher(PublisherModel publisher);

        Task DeletePublisher(Guid publisherId);

        Task<PublisherModel> GetPublisherByCompanyName(string companyName);
        
        Task<IEnumerable<string>> GetSuppliersIdsByNames(IEnumerable<string> companiesNames);

        Task<IEnumerable<PublisherModel>> GetAllPublishers();

        Task<bool> CheckCompanyNameForUnique(Guid publisherId, string companyName);
    }
}