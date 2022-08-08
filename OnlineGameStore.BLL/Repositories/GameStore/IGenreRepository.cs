using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<Genre> GetByName(string name,
            bool includeDeleted = false,
            params string[] includeProperties);

        Task<IEnumerable<Genre>> GetParentGenres(bool includeDeleted = false,
            params string[] includeProperties);
        
        Task<IEnumerable<Genre>> GetWithoutGenre(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties);
        
        Task<IEnumerable<string>> GetIdsByNames(IEnumerable<string> genresNames);
    }
}