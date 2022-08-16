using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.SqlServer
{
    public interface IGenreSqlServerRepository : ISqlServerRepository<GenreEntity>
    {
        Task<GenreEntity> GetByNameAsync(string name, bool includeDeleted = false, params string[] includeProperties);

        Task<IEnumerable<GenreEntity>> GetParentGenresAsync(bool includeDeleted = false,
            params string[] includeProperties);
        
        Task<IEnumerable<GenreEntity>> GetWithoutGenreAsync(Guid id, bool includeDeleted = false,
            params string[] includeProperties);
        
        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> genresNames);
    }
}