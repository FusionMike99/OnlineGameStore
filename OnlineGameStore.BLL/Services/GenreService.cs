using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;
using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GameService> _logger;

        public GenreService(IUnitOfWork unitOfWork, ILogger<GameService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Genre CreateGenre(Genre genre)
        {
            var createdGenre = _unitOfWork.Genres.Create(genre);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(CreateGenre)}.
                    Creating genre with id {createdGenre.Id} successfully", createdGenre);

            return createdGenre;
        }

        public void DeleteGenre(int genreId)
        {
            var genre = _unitOfWork.Genres
                .GetSingle(g => g.Id == genreId);

            if (genre == null)
            {
                var exception = new InvalidOperationException("Genre has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} unsuccessfully", genreId);

                throw exception;
            }

            _unitOfWork.Genres.Delete(genre);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} successfully", genre);
        }

        public Genre EditGenre(Genre genre)
        {
            var editedGenre = _unitOfWork.Genres.Update(genre);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(EditGenre)}.
                    Editing genre with id {editedGenre.Id} successfully", editedGenre);

            return editedGenre;
        }

        public IEnumerable<Genre> GetAllParentGenres()
        {
            var genres = _unitOfWork.Genres
                .GetMany(predicate: g => !g.ParentId.HasValue,
                    includeDeleteEntities: false,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllParentGenres)}.
                    Receiving genres successfully", genres);

            return genres;
        }

        public IEnumerable<Genre> GetAllWithoutGenre(int genreId)
        {
            var genres = _unitOfWork.Genres
                .GetMany(predicate: g => g.Id != genreId && g.ParentId != genreId,
                    includeDeleteEntities: false,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllWithoutGenre)}.
                    Receiving genres successfully", genres);

            return genres;
        }

        public Genre GetGenreById(int genreId)
        {
            var genre = _unitOfWork.Genres
                .GetSingle(predicate: g => g.Id == genreId,
                    includeDeleteEntities: false,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetGenreById)}.
                    Receiving genre with id {genreId} successfully", genre);

            return genre;
        }

        public bool CheckNameForUniqueness(int genreId, string name)
        {
            var genre = _unitOfWork.Genres.GetSingle(g => g.Name == name);

            return genre != null && genre.Id != genreId;
        }
    }
}
