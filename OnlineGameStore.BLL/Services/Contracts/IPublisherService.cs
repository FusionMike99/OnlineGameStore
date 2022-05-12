using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPublisherService
    {
        Publisher CreatePublisher(Publisher publisher);

        Publisher EditPublisher(Publisher publisher);

        void DeletePublisher(int publisherId);

        Publisher GetPublisherByCompanyName(string companyName);

        IEnumerable<Publisher> GetAllPublishers();

        bool CheckCompanyNameForUnique(int publisherId, string companyName);
    }
}