using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPublisherService
    {
        Publisher CreatePublisher(Publisher publisher);

        Publisher EditPublisher(string companyName, Publisher publisher);

        void DeletePublisher(string publisherId);

        Publisher GetPublisherByCompanyName(string companyName);
        
        IEnumerable<string> GetSuppliersIdsByNames(IEnumerable<string> companiesNames);

        IEnumerable<Publisher> GetAllPublishers();

        bool CheckCompanyNameForUnique(string publisherId, string companyName);
    }
}