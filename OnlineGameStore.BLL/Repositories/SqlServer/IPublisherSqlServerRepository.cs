using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.SqlServer
{
    public interface IPublisherSqlServerRepository : ISqlServerRepository<PublisherEntity>
    {
        Task<PublisherEntity> GetByNameAsync(string companyName, bool includeDeleted = false,
            params string[] includeProperties);
    }
}