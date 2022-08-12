using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStorePublisherRepository : IGameStoreGenericRepository<PublisherEntity>
    {
        Task<PublisherEntity> GetByName(string companyName,
            bool includeDeleted = false,
            params string[] includeProperties);
    }
}