﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;
using System.Collections.Generic;
using System.IO;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("game")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpPost("new")]
        public IActionResult Create([FromBody] EditGameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedGame = _mapper.Map<Game>(game);

            mappedGame = _gameService.CreateGame(mappedGame);

            var gameViewModel = _mapper.Map<GameViewModel>(mappedGame);

            return Json(gameViewModel);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody] EditGameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedGame = _mapper.Map<Game>(game);

            mappedGame = _gameService.EditGame(mappedGame);

            var gameViewModel = _mapper.Map<GameViewModel>(mappedGame);

            return Json(gameViewModel);
        }

        [HttpGet("{gameKey}")]
        [ResponseCache(Duration = 60)]
        public IActionResult GetGameByKey([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            var game = _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound();
            }

            var gameViewModel = _mapper.Map<GameViewModel>(game);

            return Json(gameViewModel);
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult GetGames()
        {
            var games = _gameService.GetAllGames();

            var gamesViewModel = _mapper.Map<IEnumerable<GameViewModel>>(games);

            return Json(gamesViewModel);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            _gameService.DeleteGame(id.Value);

            return NoContent();
        }

        [HttpGet("{gameKey}/[action]")]
        public IActionResult Download([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            var filepath = Path.Combine("~/Files", "hello.txt");

            return File(filepath, "text/plain", "hello.txt");
        }
    }
}