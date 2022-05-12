using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.MVC.Components
{
    public class TotalGamesViewComponent : ViewComponent
    {
        private readonly IGameService _gameService;

        public TotalGamesViewComponent(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IViewComponentResult Invoke()
        {
            var games = _gameService.GetAllGames();

            var totalGames = games.Count();

            return View(totalGames);
        }
    }
}