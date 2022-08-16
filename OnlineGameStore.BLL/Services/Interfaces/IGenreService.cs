using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services.Interfaces
{
    public interface IGenreService
    {
        Task<GenreModel> CreateGenreAsync(GenreModel genre);

        Task<GenreModel> EditGenreAsync(GenreModel genre);

        Task DeleteGenreAsync(Guid genreId);

        Task<GenreModel> GetGenreByIdAsync(Guid genreId);

        Task<IEnumerable<GenreModel>> GetAllGenresAsync();
        
        Task<IEnumerable<GenreModel>> GetAllParentGenresAsync();

        Task<IEnumerable<GenreModel>> GetAllWithoutGenreAsync(Guid genreId);
        
        Task<IEnumerable<string>> GetGenresIdsByNamesAsync(params string[] genresNames);
        
        Task<IEnumerable<string>> GetCategoriesIdsByNamesAsync(IEnumerable<string> categoriesNames);

        Task<bool> CheckNameForUniqueAsync(Guid genreId, string name);
    }
}