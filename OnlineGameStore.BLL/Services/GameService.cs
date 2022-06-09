using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork, ILogger<GameService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Game CreateGame(Game game)
        {
            var createdGame = _unitOfWork.Games.Create(game);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(CreateGame)}.
                    Creating game with id {createdGame.Id} successfully", createdGame);

            return createdGame;
        }

        public void DeleteGame(int gameId)
        {
            var game = _unitOfWork.Games.GetSingle(g => g.Id == gameId);

            if (game == null)
            {
                var exception = new InvalidOperationException("Game has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleting game with id {gameId} unsuccessfully", gameId);

                throw exception;
            }

            _unitOfWork.Games.Delete(game);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleting game with id {gameId} successfully", game);
        }

        public Game EditGame(Game game)
        {
            var editedGame = _unitOfWork.Games.Update(game);

            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(EditGame)}.
                    Editing game with id {editedGame.Id} successfully", editedGame);

            return editedGame;
        }
       
        public IEnumerable<Game> GetAllGames(SortFilterGameModel sortFilterModel = null, PageModel pageModel = null)
        {
            AddSubgenresToList(sortFilterModel?.SelectedGenres);
            
            var predicate = GetPredicate(sortFilterModel);

            var (skip, take) = CalculateSkipAndTake(pageModel);
            
            var games = _unitOfWork.Games.GetMany(predicate,
                    false,
                    SortGame(sortFilterModel?.GameSortState),
                    skip,
                    take,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}",
                    $"{nameof(Game.Comments)}",
                    $"{nameof(Game.Publisher)}");

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetAllGames)}.
                    Receiving games successfully", games);

            return games;
        }

        public int GetGamesNumber(SortFilterGameModel sortFilterModel = null)
        {
            AddSubgenresToList(sortFilterModel?.SelectedGenres);
            
            var predicate = GetPredicate(sortFilterModel);

            var gamesNumber = _unitOfWork.Games.CountEntities(predicate);
            
            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGamesNumber)}.
                    Counting games successfully", gamesNumber);

            return gamesNumber;
        }

        public Game GetGameByKey(string gameKey, bool increaseViews = false)
        {
            var game = _unitOfWork.Games.GetSingle(g => g.Key == gameKey,
                    false,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}",
                    $"{nameof(Game.Publisher)}");

            if (game != null && increaseViews)
            {
                game.ViewsNumber++;

                _unitOfWork.Commit();
            }

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGameByKey)}.
                    Receiving game with game key {gameKey} successfully", game);

            return game;
        }

        public IEnumerable<Game> GetGamesByGenre(int genreId)
        {
            var games = _unitOfWork.Games
                .GetMany(g => g.GameGenres.Any(genre => genre.GenreId == genreId),
                    false,
                    null,
                    null,
                    null,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}",
                    $"{nameof(Game.Publisher)}");

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGamesByGenre)}.
                    Receiving games with genre id {genreId} successfully", games);

            return games;
        }

        public IEnumerable<Game> GetGamesByPlatformType(int typeId)
        {
            var games = _unitOfWork.Games
                .GetMany(g => g.GamePlatformTypes.Any(platformType => platformType.PlatformId == typeId),
                    false,
                    null,
                    null,
                    null,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}",
                    $"{nameof(Game.Publisher)}");

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGamesByPlatformType)}.
                    Receiving games with platform type id {typeId} successfully", games);

            return games;
        }

        public bool CheckKeyForUnique(int gameId, string gameKey)
        {
            var game = _unitOfWork.Games.GetSingle(g => g.Key == gameKey, true);

            return game != null && game.Id != gameId;
        }

        private void AddSubgenresToList(List<int> genreIds)
        {
            if (genreIds == null)
            {
                return;
            }
            
            for (var i = 0; i < genreIds.Count; i++)
            {
                var index = i;
                
                var subgenreIds = _unitOfWork.Genres.GetSingle(g => g.Id == genreIds[index],
                        false,
                        $"{nameof(Genre.SubGenres)}")
                    .SubGenres.Select(g => g.Id)
                    .ToList();
                
                AddSubgenresToList(subgenreIds);
                
                genreIds.AddRange(subgenreIds);
            }
        }

        private static Expression<Func<Game, bool>> GetPredicate(SortFilterGameModel model)
        {
            var gameFilterPipeline = new GameFilterPipeline()
                .Register(new GamesByGenresFilter())
                .Register(new GamesByPlatformTypesFilter())
                .Register(new GamesByPublishersFilter())
                .Register(new GamesByPriceRangeFilter())
                .Register(new GamesByDatePublishedFilter())
                .Register(new GamesByNameFilter());

            var predicate = gameFilterPipeline.Process(model);

            return predicate;
        }
        
        private static Func<IQueryable<Game>, IOrderedQueryable<Game>> SortGame(GameSortState? sortState)
        {
            Func<IQueryable<Game>, IOrderedQueryable<Game>> orderBy = sortState switch
            {
                GameSortState.MostPopular => games => games.OrderByDescending(g => g.ViewsNumber),
                GameSortState.MostCommented => games => games.OrderByDescending(g => g.Comments.Count),
                GameSortState.PriceAsc => games => games.OrderBy(g => g.Price),
                GameSortState.PriceDesc => games => games.OrderByDescending(g => g.Price),
                GameSortState.New => games => games.OrderByDescending(g => g.DateAdded),
                _ => null
            };

            return orderBy;
        }

        private static (int? skip, int? take) CalculateSkipAndTake(PageModel pageModel)
        {
            int? skip = null, take = null;

            if (pageModel?.PageSize != PageSize.All)
            {
                skip = (pageModel?.CurrentPageNumber - 1) * (int?)pageModel?.PageSize;
                take = (int?)pageModel?.PageSize;
            }

            return (skip, take);
        }
    }
}