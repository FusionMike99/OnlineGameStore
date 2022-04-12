﻿using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineGameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GameService> _logger;

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
                    Created game with id {createdGame.Id} successfully", createdGame);

            return createdGame;
        }

        public void DeleteGame(int gameId)
        {
            var game = _unitOfWork.Games
                .GetSingle(g => g.Id == gameId);

            if (game == null)
            {
                var exception = new InvalidOperationException("Game has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleted game with id {gameId} unsuccessfully", gameId);

                throw exception;
            }

            _unitOfWork.Games.Delete(game);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleted game with id {gameId} successfully", game);
        }

        public Game EditGame(Game game)
        {
            var editedGame = _unitOfWork.Games.Update(game);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(EditGame)}.
                    Edited game with id {editedGame.Id} successfully", editedGame);

            return editedGame;
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _unitOfWork.Games
                .GetMany(
                    null,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetAllGames)}.
                    Received games successfully", games);

            return games;
        }

        public Game GetGameByKey(string gameKey)
        {
            var game = _unitOfWork.Games
                .GetSingle(
                    g => g.Key == gameKey,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGameByKey)}.
                    Received game with game key {gameKey} successfully", game);

            return game;
        }

        public IEnumerable<Game> GetGamesByGenre(int genreId)
        {
            var games = _unitOfWork.Games
                .GetMany(
                    g => g.GameGenres.Any(genre => genre.GenreId == genreId),
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGamesByGenre)}.
                    Received games with genre id {genreId} successfully", games);

            return games;
        }

        public IEnumerable<Game> GetGamesByPlatformType(int typeId)
        {
            var games = _unitOfWork.Games
                .GetMany(
                    g => g.GamePlatformTypes.Any(platformType => platformType.PlatformId == typeId),
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(GetGamesByPlatformType)}.
                    Received games with platform type id {typeId} successfully", games);

            return games;
        }
    }
}
