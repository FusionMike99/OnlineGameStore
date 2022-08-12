using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreGenreRepository : IGameStoreGenericRepository<GenreEntity>
    {
        Task<GenreEntity> GetByName(string name,
            bool includeDeleted = false,
            params string[] includeProperties);

        Task<IEnumerable<GenreEntity>> GetParentGenres(bool includeDeleted = false,
            params string[] includeProperties);
        
        Task<IEnumerable<GenreEntity>> GetWithoutGenre(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties);
        
        Task<IEnumerable<string>> GetIdsByNames(IEnumerable<string> genresNames);
    }
}