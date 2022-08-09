using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IUnitOfWork
    {
        IGameStoreGenericRepository<Game> Games { get; }

        IGameStoreGenericRepository<Comment> Comments { get; }

        IGameStoreGenericRepository<Genre> Genres { get; }

        IGameStoreGenericRepository<PlatformType> PlatformTypes { get; }

        IGameStoreGenericRepository<Publisher> Publishers { get; }

        IGameStoreGenericRepository<Order> Orders { get; }

        int Commit();
    }
}