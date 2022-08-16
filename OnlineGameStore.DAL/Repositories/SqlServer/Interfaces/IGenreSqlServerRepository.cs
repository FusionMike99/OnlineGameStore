using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Interfaces
{
    public interface IGenreSqlServerRepository : ISqlServerRepository<GenreEntity>
    {
        Task<GenreEntity> GetByNameAsync(string name);
        
        Task<GenreEntity> GetByNameIncludeDeletedAsync(string name);

        Task<IEnumerable<GenreEntity>> GetParentGenresAsync();
        
        Task<IEnumerable<GenreEntity>> GetWithoutGenreAsync(Guid id);
        
        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> genresNames);
    }
}