using OnlineGameStore.BLL.Entities;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPublisherService
    {
        Publisher CreatePublisher(Publisher publisher);

        Publisher EditPublisher(Publisher publisher);

        void DeletePublisher(int publisherId);

        Publisher GetPublisherByCompanyName(string companyName);

        IEnumerable<Publisher> GetAllPublishers();

        bool CheckCompanyNameForUniqueness(int publisherId, string companyName);
    }
}
