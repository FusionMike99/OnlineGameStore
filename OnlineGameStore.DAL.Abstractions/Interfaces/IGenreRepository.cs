using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Abstractions.Interfaces
{
    public interface IGenreRepository
    {
        Task CreateAsync(GenreModel genreModel);

        Task UpdateAsync(GenreModel genreModel);

        Task DeleteAsync(GenreModel genreModel);
        
        Task<GenreModel> GetByIdAsync(Guid id);
        
        Task<GenreModel> GetByNameAsync(string name);
        
        Task<GenreModel> GetByNameIncludeDeletedAsync(string name);

        Task<IEnumerable<string>> GetGenreIdsByNamesAsync(IEnumerable<string> names);
        
        Task<IEnumerable<string>> GetCategoryIdsByNamesAsync(IEnumerable<string> names);

        Task<IEnumerable<GenreModel>> GetAllAsync();
        
        Task<IEnumerable<GenreModel>> GetParentGenresAsync();
        
        Task<IEnumerable<GenreModel>> GetWithoutGenreAsync(Guid id);
    }
}