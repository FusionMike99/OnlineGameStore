using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters.Games;
using OnlineGameStore.BLL.Pipelines.Filters.Products;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INorthwindUnitOfWork _northwindUnitOfWork;
        private readonly IMapper _mapper;

        public GameService(ILogger<GameService> logger,
            IUnitOfWork unitOfWork,
            INorthwindUnitOfWork northwindUnitOfWork,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _northwindUnitOfWork = northwindUnitOfWork;
            _mapper = mapper;
        }

        public Game CreateGame(Game game)
        {
            var createdGame = _unitOfWork.Games.Create(game);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(CreateGame)}.
                    Creating game with id {createdGame.Id} successfully", createdGame);

            return createdGame;
        }

        public void DeleteGame(string gameKey)
        {
            var game = GetGameByKey(gameKey);

            if (game == null)
            {
                var exception = new InvalidOperationException("Game has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleting game with id {gameKey} unsuccessfully", gameKey);

                throw exception;
            }

            if (game.DatabaseEntity is DatabaseEntity.GameStore)
            {
                _unitOfWork.Games.Delete(game);
                _unitOfWork.Commit();
            }
            else
            {
                game.Id = Guid.NewGuid();
                game.IsDeleted = true;
                game.DeletedAt = DateTime.UtcNow;

                CreateGame(game);
            }

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleting game with id {gameKey} successfully", game);
        }

        public Game EditGame(string gameKey, Game game)
        {
            Game editedGame;
            var oldGame = game.DeepClone();
            
            if (game.DatabaseEntity is DatabaseEntity.GameStore)
            {
                editedGame = _unitOfWork.Games.Update(game);

                _unitOfWork.Commit();
            }
            else
            {
                game.Id = Guid.NewGuid();
                editedGame = CreateGame(game);
            }

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(EditGame)}.
                    Editing game with id {editedGame.Id} successfully", editedGame);

            return editedGame;
        }

        public void UpdateGameQuantity(string gameKey, short quantity, Func<short, short, short> operation)
        {
            var game = GetGameByKey(gameKey);
            var oldGame = game.DeepClone();

            game.UnitsInStock = operation(game.UnitsInStock, quantity);

            if (game.DatabaseEntity is DatabaseEntity.GameStore)
            {
                _unitOfWork.Games.Update(game);
            }
            else
            {
                var product = _mapper.Map<NorthwindProduct>(game);
                _northwindUnitOfWork.Products.Update(p => p.Key == product.Key, product);
            }
        }

        public IEnumerable<Game> GetAllGames(SortFilterGameModel sortFilterModel = null, PageModel pageModel = null)
        {
            var games = GetGamesProducts(sortFilterModel);
            var pagingGames = PagingGames(games, pageModel);

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetAllGames)}.
                    Receiving games successfully", pagingGames);

            return pagingGames;
        }

        public IEnumerable<Game> GetAllGames(out int gamesNumber, SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null)
        {
            var games = GetGamesProducts(sortFilterModel).ToList();
            gamesNumber = games.Count;
            
            var pagingGames = PagingGames(games, pageModel);

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetAllGames)}.
                    Receiving games successfully", pagingGames, gamesNumber);

            return pagingGames;
        }

        public int GetGamesNumber(SortFilterGameModel sortFilterModel = null)
        {
            var games = GetGamesProducts(sortFilterModel).ToList();

            var totalNumbers = games.Count;
            
            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGamesNumber)}.
                    Counting games successfully", totalNumbers);

            return totalNumbers;
        }

        public Game GetGameByKey(string gameKey, bool increaseViews = false)
        {
            var game = _unitOfWork.Games.GetSingle(g => g.Key == gameKey,
                    false,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            if (game == null)
            {
                var product = _northwindUnitOfWork.Products.GetFirst(p => p.Key == gameKey);

                if (product != null)
                {
                    product.Supplier = _northwindUnitOfWork.Suppliers
                        .GetFirst(s => s.SupplierId == product.SupplierId);
                    product.Category = _northwindUnitOfWork.Categories
                        .GetFirst(c => c.CategoryId == product.CategoryId);
                    
                    game = _mapper.Map<Game>(product);
                    game.DatabaseEntity = DatabaseEntity.Northwind;
                    game.DateAdded = Constants.AddedAtDefault;

                    if (product.Category != null)
                    {
                        var genre = _unitOfWork.Genres.GetSingle(g => g.Name == product.Category.Name);

                        game.GameGenres = new List<GameGenre>
                        {
                            new GameGenre
                            {
                                GenreId = genre.Id,
                                Genre = genre
                            }
                        };
                    }
                }
            }

            if (game != null && increaseViews)
            {
                IncreaseViewsNumber(game);
            }

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGameByKey)}.
                    Receiving game with game key {gameKey} successfully", game);

            return game;
        }

        public bool CheckKeyForUnique(string gameId, string gameKey)
        {
            var gameGuid = Guid.Parse(gameId);
            var game = _unitOfWork.Games.GetSingle(g => g.Key == gameKey, true);

            return game != null && game.Id != gameGuid;
        }

        private void AddSubgenresToList(List<string> genreIds)
        {
            if (genreIds == null)
            {
                return;
            }
            
            for (var i = 0; i < genreIds.Count; i++)
            {
                var genreGuid = Guid.Parse(genreIds[i]);
                var subgenreIds = _unitOfWork.Genres
                    .GetSingle(g => g.Id == genreGuid, false, $"{nameof(Genre.SubGenres)}")
                    .SubGenres.Select(g => g.Id.ToString()).ToList();

                AddSubgenresToList(subgenreIds);
                genreIds.AddRange(subgenreIds);
            }
        }
        
        private (Expression<Func<Game, bool>>, Expression<Func<NorthwindProduct,bool>>) 
            GetPredicates(SortFilterGameModel model)
        {
            var gamePredicate = GetGameStorePredicate(model);

            var productPredicate = GetNorthwindPredicate(model);

            return (gamePredicate, productPredicate);
        }

        private static Expression<Func<Game, bool>> GetGameStorePredicate(SortFilterGameModel model)
        {
            var gamesFilterPipeline = new GamesFilterPipeline()
                .Register(new GamesByGenresFilter())
                .Register(new GamesByPlatformTypesFilter())
                .Register(new GamesByPublishersFilter())
                .Register(new GamesByPriceRangeFilter())
                .Register(new GamesByDatePublishedFilter())
                .Register(new GamesByNameFilter());

            var predicate = gamesFilterPipeline.Process(model);

            return predicate;
        }
        
        private Expression<Func<NorthwindProduct, bool>> GetNorthwindPredicate(SortFilterGameModel model)
        {
            var productsFilterPipeline = new ProductsFilterPipeline()
                .Register(new ProductsByCategoriesFilter())
                .Register(new ProductsBySuppliersFilter())
                .Register(new ProductsByPriceRangeFilter())
                .Register(new ProductsByNameFilter());

            SetSuppliersIds(model);

            var predicate = productsFilterPipeline.Process(model);

            return predicate;
        }
        
        private static IEnumerable<Game> SortGame(IEnumerable<Game> games, GameSortState? sortState)
        {
            var sortedGames = sortState switch
            {
                GameSortState.MostPopular => games.OrderByDescending(g => g.ViewsNumber),
                GameSortState.MostCommented => games.OrderByDescending(g => g.Comments?.Count),
                GameSortState.PriceAsc => games.OrderBy(g => g.Price),
                GameSortState.PriceDesc => games.OrderByDescending(g => g.Price),
                GameSortState.New => games.OrderByDescending(g => g.DateAdded),
                _ => games
            };

            return sortedGames;
        }

        private IEnumerable<NorthwindProduct> SetGameKeyAndDateAdded(List<NorthwindProduct> products)
        {
            products.ForEach(product =>
            {
                product.Key ??= product.Name.ToKebabCase();
                
                _northwindUnitOfWork.Products.Update(pr => pr.Id == product.Id, product);

                product.DateAdded ??= Constants.AddedAtDefault;
            });

            return products;
        }

        private IEnumerable<Game> UnionGamesProducts(IEnumerable<Game> games,
            IEnumerable<NorthwindProduct> products)
        {
            var mappedProducts = _mapper.Map<IEnumerable<Game>>(products);

            var result = games.Concat(mappedProducts).DistinctBy(g => g.Key).ToList();

            return result;
        }

        private IEnumerable<Game> GetGamesProducts(SortFilterGameModel sortFilterModel = null)
        {
            AddSubgenresToList(sortFilterModel?.SelectedGenres);
            
            var (predicateGameStore, predicateNorthwind) = GetPredicates(sortFilterModel);
            
            var games = _unitOfWork.Games.GetMany(predicateGameStore,
                true, null, null, null,
                $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}",
                $"{nameof(Game.Comments)}");

            var products = _northwindUnitOfWork.Products.GetMany(predicateNorthwind);

            products = SetGameKeyAndDateAdded(products.ToList());

            var unionGamesProducts = UnionGamesProducts(games, products);

            var sortedGames = SortGame(unionGamesProducts, sortFilterModel?.GameSortState);

            var notDeletedGames = sortedGames.Where(g => !g.IsDeleted);

            return notDeletedGames;
        }

        private static IEnumerable<Game> PagingGames(IEnumerable<Game> games, PageModel pageModel)
        {
            var (skip, take) = CalculateSkipAndTake(pageModel);
            
            if (skip.HasValue)
            {
                games = games.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                games = games.Take(take.Value);
            }

            return games;
        }

        private static (int? skip, int? take) CalculateSkipAndTake(PageModel pageModel)
        {
            if (pageModel?.PageSize == PageSize.All)
            {
                return (null, null);
            }
            
            var skip = (pageModel?.CurrentPageNumber - 1) * (int?)pageModel?.PageSize;
            var take = (int?)pageModel?.PageSize;

            return (skip, take);
        }

        private void IncreaseViewsNumber(Game game)
        {
            var oldGame = game.DeepClone();
            
            game.ViewsNumber++;
            
            if (game.DatabaseEntity is DatabaseEntity.GameStore)
            {
                _unitOfWork.Games.Update(game);
                _unitOfWork.Commit();
            }
            else
            {
                var product = _mapper.Map<NorthwindProduct>(game);
                _northwindUnitOfWork.Products.Update(p => p.Key == product.Key, product);
            }
        }

        private void SetSuppliersIds(SortFilterGameModel model)
        {
            if (model != null && model.SelectedPublishers?.Any() == false)
            {
                model.SelectedSuppliers = _northwindUnitOfWork.Suppliers
                    .GetMany(s => model.SelectedPublishers.Contains(s.CompanyName))
                    .Select(s => s.Id.ToString()).ToList();
            }
        }
    }
}