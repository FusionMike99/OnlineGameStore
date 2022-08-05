using System;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Game, Guid> Games { get; }

        IGenericRepository<Comment, Guid> Comments { get; }

        IGenericRepository<Genre, Guid> Genres { get; }

        IGenericRepository<PlatformType, Guid> PlatformTypes { get; }

        IGenericRepository<Publisher, Guid> Publishers { get; }

        IGenericRepository<Order, Guid> Orders { get; }

        int Commit();
    }
}