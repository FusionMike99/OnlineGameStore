using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game, int> Games { get; }

        IGenericRepository<Comment, int> Comments { get; }

        IGenericRepository<Genre, int> Genres { get; }

        IGenericRepository<PlatformType, int> PlatformTypes { get; }

        IGenericRepository<Publisher, int> Publishers { get; }

        int Commit();
    }
}
