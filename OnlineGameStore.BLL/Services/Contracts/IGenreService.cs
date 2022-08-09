using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGenreService
    {
        Task<GenreModel> CreateGenre(GenreModel genre);

        Task<GenreModel> EditGenre(GenreModel genre);

        Task DeleteGenre(Guid genreId);

        Task<GenreModel> GetGenreById(Guid genreId);

        Task<IEnumerable<GenreModel>> GetAllGenres();
        
        Task<IEnumerable<GenreModel>> GetAllParentGenres();

        Task<IEnumerable<GenreModel>> GetAllWithoutGenre(Guid genreId);
        
        Task<IEnumerable<string>> GetGenresIdsByNames(params string[] genresNames);
        
        Task<IEnumerable<string>> GetCategoriesIdsByNames(IEnumerable<string> categoriesNames);

        Task<bool> CheckNameForUnique(Guid genreId, string name);
    }
}