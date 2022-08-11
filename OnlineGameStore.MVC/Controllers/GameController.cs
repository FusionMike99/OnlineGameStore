using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.ModelBuilders;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("games")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly IPlatformTypeService _platformTypeService;
        private readonly IPublisherService _publisherService;

        public GameController(IGameService gameService,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IPublisherService publisherService,
            IMapper mapper)
        {
            _gameService = gameService;
            _genreService = genreService;
            _platformTypeService = platformTypeService;
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        public async Task<IActionResult> Create()
        {
            var editGameViewModel = new EditGameViewModel();

            await ConfigureEditGameViewModel(editGameViewModel);

            return View(editGameViewModel);
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EditGameViewModel game)
        {
            await VerifyGame(game);

            if (!ModelState.IsValid)
            {
                await ConfigureEditGameViewModel(game);

                return View(game);
            }

            var mappedGame = _mapper.Map<GameModel>(game);

            await _gameService.CreateGame(mappedGame);

            return RedirectToAction(nameof(GetGames));
        }

        [HttpGet("update/{gameKey}")]
        public async Task<IActionResult> Update([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            var game = await _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound();
            }

            var editGameViewModel = _mapper.Map<EditGameViewModel>(game);

            await ConfigureEditGameViewModel(editGameViewModel);

            return View(editGameViewModel);
        }

        [HttpPost("update/{gameKey}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string gameKey, [FromForm] EditGameViewModel game)
        {
            await VerifyGame(game);

            if (!ModelState.IsValid)
            {
                await ConfigureEditGameViewModel(game);

                return View(game);
            }

            var mappedGame = _mapper.Map<GameModel>(game);

            await _gameService.EditGame(mappedGame);

            return RedirectToAction(nameof(GetGames));
        }

        [HttpGet("{gameKey}")]
        public async Task<IActionResult> GetGameByKey([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            var game = await _gameService.GetGameByKey(gameKey, increaseViews: true);

            if (game == null)
            {
                return NotFound();
            }

            var gameViewModel = _mapper.Map<GameViewModel>(game);

            return View("Details", gameViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetGames(SortFilterGameViewModel sortFilterGameViewModel,
            int pageNumber = 1, PageSize pageSize = PageSize.Ten)
        {
            SortFilterGameModel sortFilterGameModel = await SortFilterGameModelBuilder.Create(sortFilterGameViewModel,
                _genreService, _platformTypeService, _publisherService);

            var pageModel = new PageModel(pageNumber, pageSize);

            var (games, gamesNumber) = await _gameService.GetAllGames(sortFilterGameModel, pageModel);

            var gamesViewModel = _mapper.Map<IEnumerable<GameViewModel>>(games);

            var pageViewModel = new PageViewModelBuilder(pageModel, gamesNumber);

            var gameListViewModel = new GameListViewModel
            {
                PageViewModel = pageViewModel,
                Games = gamesViewModel,
                SortFilterGameViewModel = await SortFilterGameViewModelBuilder.Create(sortFilterGameModel,
                    _genreService, _platformTypeService, _publisherService)
            };

            return View("Index", gameListViewModel);
        }

        [HttpPost("remove/{gameKey}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            await _gameService.DeleteGame(gameKey);

            return RedirectToAction(nameof(GetGames));
        }

        [HttpGet("{gameKey}/download")]
        public async Task<IActionResult> Download([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            var game = await _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound();
            }

            var gameViewModel = _mapper.Map<GameViewModel>(game);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var serializedGame = Encoding.Default.GetBytes(JsonSerializer.Serialize(gameViewModel, options));

            return File(serializedGame, "application/txt", $"{game.Name}.txt");
        }

        private async Task ConfigureEditGameViewModel(EditGameViewModel model)
        {
            var genresTask = _genreService.GetAllGenres();
            var platformsTask = _platformTypeService.GetAllPlatformTypes();
            var publishersTask = _publisherService.GetAllPublishers();

            await Task.WhenAll(genresTask, platformsTask, publishersTask);
            
            model.Genres = new SelectList(await genresTask,
                nameof(Genre.Id),
                nameof(Genre.Name));

            model.PlatformTypes = new SelectList(await platformsTask,
                nameof(PlatformType.Id),
                nameof(PlatformType.Type));

            model.Publishers = new SelectList(await publishersTask,
                nameof(Publisher.CompanyName),
                nameof(Publisher.CompanyName));
        }

        private async Task VerifyGame(EditGameViewModel game)
        {
            var checkResult = await _gameService.CheckKeyForUnique(game.Id, game.Key);

            if (checkResult)
            {
                ModelState.AddModelError(nameof(GameViewModel.Key), ErrorMessages.GameKeyExist);
            }
        }
    }
}