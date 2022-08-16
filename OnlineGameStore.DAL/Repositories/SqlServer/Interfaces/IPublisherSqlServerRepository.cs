using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Interfaces
{
    public interface IPublisherSqlServerRepository : ISqlServerRepository<PublisherEntity>
    {
        Task<PublisherEntity> GetByNameAsync(string companyName);
        
        Task<PublisherEntity> GetByNameIncludeDeletedAsync(string companyName);
    }
}