using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game> Games { get; }

        IGenericRepository<Comment> Comments { get; }

        IGenericRepository<Genre> Genres { get; }

        IGenericRepository<PlatformType> PlatformTypes { get; }

        IGenericRepository<Publisher> Publishers { get; }

        IGenericRepository<Order> Orders { get; }

        int Commit();
    }
}