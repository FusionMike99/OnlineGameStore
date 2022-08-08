using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGenreService
    {
        Task<GenreModel> CreateGenre(GenreModel genre);

        Task<GenreModel> EditGenre(GenreModel genre);

        Task DeleteGenre(string genreId);

        Task<GenreModel> GetGenreById(string genreId);

        Task<IEnumerable<GenreModel>> GetAllGenres();
        
        Task<IEnumerable<GenreModel>> GetAllParentGenres();

        Task<IEnumerable<GenreModel>> GetAllWithoutGenre(string genreId);
        
        Task<IEnumerable<string>> GetGenresIdsByNames(params string[] genresNames);
        
        Task<IEnumerable<string>> GetCategoriesIdsByNames(IEnumerable<string> categoriesNames);

        Task<bool> CheckNameForUnique(string genreId, string name);
    }
}