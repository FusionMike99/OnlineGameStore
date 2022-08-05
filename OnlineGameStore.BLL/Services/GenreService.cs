using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly ILogger<GenreService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INorthwindUnitOfWork _northwindUnitOfWork;
        private readonly INorthwindLogService _logService;

        public GenreService(ILogger<GenreService> logger,
            IUnitOfWork unitOfWork,
            INorthwindUnitOfWork northwindUnitOfWork,
            INorthwindLogService logService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _northwindUnitOfWork = northwindUnitOfWork;
            _logService = logService;
        }

        public Genre CreateGenre(Genre genre)
        {
            var createdGenre = _unitOfWork.Genres.Create(genre);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(CreateGenre)}.
                    Creating genre with id {createdGenre.Id} successfully", createdGenre);
            
            _logService.LogCreating(createdGenre);

            return createdGenre;
        }

        public void DeleteGenre(string genreId)
        {
            var genreGuid = Guid.Parse(genreId);
            var genre = _unitOfWork.Genres.GetSingle(g => g.Id == genreGuid,
                includeDeleteEntities: false,
                $"{nameof(Genre.SubGenres)}");

            if (genre == null)
            {
                var exception = new InvalidOperationException("Genre has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} unsuccessfully", genreId);

                throw exception;
            }

            _unitOfWork.Genres.Delete(genre);

            genre.SubGenres.ToList()
                .ForEach(g => g.ParentId = g.Parent.ParentId);
            
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} successfully", genre);
            
            _logService.LogDeleting(genre);
        }

        public Genre EditGenre(Genre genre)
        {
            var oldGenre = GetGenreById(genre.Id.ToString());
            
            var editedGenre = _unitOfWork.Genres.Update(genre);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(EditGenre)}.
                    Editing genre with id {editedGenre.Id} successfully", editedGenre);
            
            _logService.LogUpdating(oldGenre, editedGenre);

            return editedGenre;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genres = _unitOfWork.Genres.GetMany(null,
                    false, null, null, null,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllGenres)}.
                    Receiving genres successfully", genres);

            return genres;
        }

        public IEnumerable<Genre> GetAllParentGenres()
        {
            var genres = _unitOfWork.Genres.GetMany(g => !g.ParentId.HasValue,
                    false, null, null, null,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllParentGenres)}.
                    Receiving parent genres successfully", genres);

            return genres;
        }

        public IEnumerable<Genre> GetAllWithoutGenre(string genreId)
        {
            var genreGuid = Guid.Parse(genreId);
            var genres = _unitOfWork.Genres.GetMany(g => g.Id != genreGuid && g.ParentId != genreGuid,
                    false, null, null, null,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllWithoutGenre)}.
                    Receiving genres without genre with id {genreId} successfully", genres);

            return genres;
        }

        public IEnumerable<string> GetGenresIdsByNames(params string[] genresNames)
        {
            var genresIds = _unitOfWork.Genres
                .GetMany(g => genresNames.Contains(g.Name))
                .Select(g => g.Id.ToString());

            return genresIds;
        }

        public IEnumerable<string> GetCategoriesIdsByNames(IEnumerable<string> genresNames)
        {
            var suppliersIds = _northwindUnitOfWork.Categories
                .GetMany(s => genresNames.Contains(s.Name))
                .Select(s => s.Id.ToString());

            return suppliersIds;
        }

        public Genre GetGenreById(string genreId)
        {
            var genreGuid = Guid.Parse(genreId);
            var genre = _unitOfWork.Genres.GetSingle(g => g.Id == genreGuid,
                    false,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetGenreById)}.
                    Receiving genre with id {genreId} successfully", genre);

            return genre;
        }

        public bool CheckNameForUnique(string genreId, string name)
        {
            var genreGuid = Guid.Parse(genreId);
            var genre = _unitOfWork.Genres.GetSingle(g => g.Name == name, true);

            return genre != null && genre.Id != genreGuid;
        }
    }
}