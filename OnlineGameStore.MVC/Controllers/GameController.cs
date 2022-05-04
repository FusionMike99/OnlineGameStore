﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Services.Contracts;
using OnlineGameStore.MVC.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("games")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IPublisherService publisherService,
            ICartService cartService,
            IMapper mapper)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _publisherService = publisherService;
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        public ViewResult Create()
        {
            var editGameViewModel = new EditGameViewModel();

            ConfigureEditGameViewModel(editGameViewModel);

            return View(editGameViewModel);
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] EditGameViewModel game)
        {
            VerifyGame(game);

            if (!ModelState.IsValid)
            {
                ConfigureEditGameViewModel(game);

                return View(game);
            }

            var mappedGame = _mapper.Map<Game>(game);

            _gameService.CreateGame(mappedGame);

            return RedirectToAction(nameof(GetGames));
        }

        [HttpGet("update/{gameKey}")]
        public IActionResult Update([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest("Need to pass game key");
            }

            var game = _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound("Game has not been found");
            }

            var editGameViewModel = _mapper.Map<EditGameViewModel>(game);

            ConfigureEditGameViewModel(editGameViewModel);

            return View(editGameViewModel);
        }

        [HttpPost, Route("update", Name = "gameupdate")]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] EditGameViewModel game)
        {
            VerifyGame(game);

            if (!ModelState.IsValid)
            {
                ConfigureEditGameViewModel(game);

                return View(game);
            }

            var mappedGame = _mapper.Map<Game>(game);

            _gameService.EditGame(mappedGame);

            return RedirectToAction(nameof(GetGames));
        }

        [HttpGet("{gameKey}")]
        public IActionResult GetGameByKey([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest("Need to pass game key");
            }

            var game = _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound("Game has not been found");
            }

            var gameViewModel = _mapper.Map<GameViewModel>(game);

            return View("Details", gameViewModel);
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            var games = _gameService.GetAllGames();

            var gamesViewModel = _mapper.Map<IEnumerable<GameViewModel>>(games);

            return View("Index", gamesViewModel);
        }

        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove([FromForm] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Need to pass game id");
            }

            _gameService.DeleteGame(id.Value);

            return RedirectToAction(nameof(GetGames));
        }

        [HttpGet("{gameKey}/download")]
        public IActionResult Download([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest("Need to pass game key");
            }

            var game = _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound("Game has not been found");
            }

            var gameViewModel = _mapper.Map<GameViewModel>(game);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var serilizedGame = Encoding.Default.GetBytes(JsonSerializer.Serialize(gameViewModel, options));

            return File(serilizedGame, "application/txt", $"{game.Name}.txt");
        }

        [HttpGet("{gameKey}/buy")]
        public IActionResult BuyGame([FromRoute] string gameKey)
        {
            var game = _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound("Game has not been found");
            }

            _cartService.AddItem(game, 1);

            return RedirectToAction(nameof(GetGameByKey), new { gameKey });
        }

        private void ConfigureEditGameViewModel(EditGameViewModel model)
        {
            model.Genres = new SelectList(_genreService.GetAllParentGenres(),
                    nameof(Genre.Id),
                    nameof(Genre.Name));

            model.PlatformTypes = new SelectList(_platformTypeService.GetAllPlatformTypes(),
                    nameof(PlatformType.Id),
                    nameof(PlatformType.Type));

            model.Publishers = new SelectList(_publisherService.GetAllPublishers(),
                    nameof(Publisher.Id),
                    nameof(Publisher.CompanyName));
        }

        private void VerifyGame(EditGameViewModel game)
        {
            if (_gameService.CheckKeyForUniqueness(game.Id, game.Key))
            {
                ModelState.AddModelError("Key", "Key with same value exist.");
            }
        }
    }
}
