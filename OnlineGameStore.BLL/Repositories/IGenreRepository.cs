using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IGenreRepository
    {
        Task CreateAsync(GenreModel genreModel);

        Task UpdateAsync(GenreModel genreModel);

        Task DeleteAsync(GenreModel genreModel);
        
        Task<GenreModel> GetByIdAsync(Guid id, bool includeDeleted = false, params string[] includeProperties);
        
        Task<GenreModel> GetByNameAsync(string name, bool includeDeleted = false, params string[] includeProperties);

        Task<IEnumerable<string>> GetGenreIdsByNamesAsync(IEnumerable<string> names);
        
        Task<IEnumerable<string>> GetCategoryIdsByNamesAsync(IEnumerable<string> names);

        Task<IEnumerable<GenreModel>> GetAllAsync(bool includeDeleted = false, params string[] includeProperties);
        
        Task<IEnumerable<GenreModel>> GetParentGenresAsync(bool includeDeleted = false, params string[] includeProperties);
        
        Task<IEnumerable<GenreModel>> GetWithoutGenreAsync(Guid id, bool includeDeleted = false,
            params string[] includeProperties);
    }
}