﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IGameStoreGameRepository _gameRepository;
        private readonly INorthwindProductRepository _productRepository;
        private readonly INorthwindCategoryRepository _categoryRepository;
        private readonly INorthwindSupplierRepository _supplierRepository;
        private readonly IGameStoreGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GameRepository(IGameStoreGameRepository gameRepository,
            INorthwindProductRepository productRepository,
            INorthwindCategoryRepository categoryRepository,
            INorthwindSupplierRepository supplierRepository,
            IGameStoreGenreRepository genreRepository,
            IMapper mapper)
        {
            _gameRepository = gameRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }


        public async Task CreateAsync(GameModel gameModel)
        {
            var game = _mapper.Map<GameEntity>(gameModel);
            var createdGame = await _gameRepository.CreateAsync(game);
            gameModel.Id = createdGame.Id;
        }

        public async Task UpdateAsync(GameModel gameModel)
        {
            var game = _mapper.Map<GameEntity>(gameModel);
            
            if (gameModel.DatabaseEntity is DatabaseEntity.GameStore)
            {
                await _gameRepository.UpdateAsync(game);
            }
            else
            {
                await _gameRepository.CreateAsync(game);
            }
        }

        public async Task IncreaseGameQuantityAsync(string gameKey, short quantity)
        {
            short Operation(short a, short b) => (short)(a + b);
            await UpdateGameQuantity(gameKey, quantity, Operation);
        }

        public async Task DecreaseGameQuantityAsync(string gameKey, short quantity)
        {
            short Operation(short a, short b) => (short)(a - b);
            await UpdateGameQuantity(gameKey, quantity, Operation);
        }

        public async Task DeleteAsync(GameModel gameModel)
        {
            var game = _mapper.Map<GameEntity>(gameModel);
            
            if (gameModel.DatabaseEntity is DatabaseEntity.GameStore)
            {
                await _gameRepository.DeleteAsync(game);
            }
            else
            {
                game.IsDeleted = true;
                game.DeletedAt = DateTime.UtcNow;

                await _gameRepository.CreateAsync(game);
            }
        }

        public async Task<GameModel> GetByKeyAsync(string gameKey,
            bool increaseViews = false,
            bool includeDeleted = false)
        {
            GameModel gameModel = null;
            var gameTask = _gameRepository.GetByKeyAsync(gameKey, includeDeleted: includeDeleted,
                $"{nameof(GameEntity.GameGenres)}.{nameof(GameGenreEntity.Genre)}",
                $"{nameof(GameEntity.GamePlatformTypes)}.{nameof(GamePlatformTypeEntity.PlatformType)}");
            var productTask = _productRepository.GetByKeyAsync(gameKey);
            await Task.WhenAll(gameTask, productTask);

            var game = await gameTask;
            
            if (game != null)
            {
                gameModel = _mapper.Map<GameModel>(game);
            }
            else
            {
                var product = await productTask;

                if (product != null)
                {
                    product.Category = await _categoryRepository.GetByCategoryIdAsync(product.CategoryId);
                    product.Supplier = await _supplierRepository.GetBySupplierIdAsync(product.SupplierId);
                    gameModel = _mapper.Map<GameModel>(product);

                    if (product.Category != null)
                    {
                        var genre = await _genreRepository.GetByNameAsync(product.Category.Name);
                        var genreModel = _mapper.Map<GenreModel>(genre);
                        gameModel.GameGenres = new List<GameGenreModel>
                        {
                            new GameGenreModel
                            {
                                GenreId = genre.Id,
                                Genre = genreModel
                            }
                        };
                    }
                }
            }

            if (gameModel != null && increaseViews)
            {
                IncreaseViewsNumber(gameModel);
            }

            return gameModel;
        }

        public async Task<(IEnumerable<GameModel>, int)> GetAllAsync(SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null)
        {
            var games = await GetGameModels(sortFilterModel);
            var gameList = games.ToList();
            var gamesNumber = gameList.Count;
            var pagingGames = PagingGames(gameList, pageModel);

            return (pagingGames, gamesNumber);
        }

        public async Task<int> GetGamesNumberAsync(SortFilterGameModel sortFilterModel = null)
        {
            var games = await GetGameModels(sortFilterModel);
            var totalNumbers = games.Count();

            return totalNumbers;
        }

        private async Task UpdateGameQuantity(string gameKey, short quantity, Func<short, short, short> operation)
        {
            var gameModel = await GetByKeyAsync(gameKey);
            gameModel.UnitsInStock = operation(gameModel.UnitsInStock, quantity);

            if (gameModel.DatabaseEntity is DatabaseEntity.GameStore)
            {
                var game = _mapper.Map<GameEntity>(gameModel);
                await _gameRepository.UpdateAsync(game);
            }
            else
            {
                var product = _mapper.Map<NorthwindProduct>(gameModel);
                await _productRepository.UpdateAsync(product);
            }
        }

        private void IncreaseViewsNumber(GameModel gameModel)
        {
            gameModel.ViewsNumber++;
            
            if (gameModel.DatabaseEntity is DatabaseEntity.GameStore)
            {
                var game = _mapper.Map<GameEntity>(gameModel);
                _gameRepository.UpdateAsync(game);
            }
            else
            {
                var product = _mapper.Map<NorthwindProduct>(gameModel);
                _productRepository.UpdateAsync(product);
            }
        }
        
        private async Task AddSubgenresToList(List<string> genreIds)
        {
            if (genreIds == null)
            {
                return;
            }
            
            for (var i = 0; i < genreIds.Count; i++)
            {
                var genreGuid = Guid.Parse(genreIds[i]);
                var genre = await _genreRepository.GetByIdAsync(genreGuid, includeDeleted: false,
                        includeProperties: $"{nameof(GenreEntity.SubGenres)}");
                var subgenreIds = genre.SubGenres.Select(g => g.Id.ToString()).ToList();
                await AddSubgenresToList(subgenreIds);
                genreIds.AddRange(subgenreIds);
            }
        }

        private async Task<IEnumerable<NorthwindProduct>> GetNorthwindProducts(
            SortFilterGameModel sortFilterModel = null)
        {
            var products = await _productRepository.GetAllByFilterAsync(sortFilterModel);
            products = await _productRepository.SetGameKeyAndDateAddedAsync(products.ToList());

            return products;
        }

        private IEnumerable<GameModel> UnionGamesProducts(IEnumerable<GameEntity> games,
            IEnumerable<NorthwindProduct> products)
        {
            var mappedGames = _mapper.Map<IEnumerable<GameModel>>(games);
            var mappedProducts = _mapper.Map<IEnumerable<GameModel>>(products);
            var result = mappedGames.Concat(mappedProducts).DistinctBy(g => g.Key);

            return result;
        }
        
        private static IEnumerable<GameModel> SortGame(IEnumerable<GameModel> games, GameSortState? sortState)
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

        private async Task<IEnumerable<GameModel>> GetGameModels(SortFilterGameModel sortFilterModel = null)
        {
            await AddSubgenresToList(sortFilterModel?.SelectedGenres);
            var gamesTask = _gameRepository.GetAllByFilterAsync(sortFilterModel);
            var productsTask = GetNorthwindProducts(sortFilterModel);
            await Task.WhenAll(gamesTask, productsTask);

            var games = await gamesTask;
            var products = await productsTask;
            var gameModels = UnionGamesProducts(games, products);
            var sortedGames = SortGame(gameModels, sortFilterModel?.GameSortState);
            var notDeletedGames = sortedGames.Where(g => !g.IsDeleted);

            return notDeletedGames;
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

        private static IEnumerable<GameModel> PagingGames(IEnumerable<GameModel> games, PageModel pageModel)
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
    }
}