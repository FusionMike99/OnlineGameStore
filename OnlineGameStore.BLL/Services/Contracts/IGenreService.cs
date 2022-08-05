using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGenreService
    {
        Genre CreateGenre(Genre genre);

        Genre EditGenre(Genre genre);

        void DeleteGenre(string genreId);

        Genre GetGenreById(string genreId);

        IEnumerable<Genre> GetAllGenres();
        
        IEnumerable<Genre> GetAllParentGenres();

        IEnumerable<Genre> GetAllWithoutGenre(string genreId);
        
        IEnumerable<string> GetGenresIdsByNames(params string[] genresNames);
        
        IEnumerable<string> GetCategoriesIdsByNames(IEnumerable<string> genresNames);

        bool CheckNameForUnique(string genreId, string name);
    }
}