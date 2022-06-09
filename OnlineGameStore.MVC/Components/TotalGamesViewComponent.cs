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
            var totalGames = _gameService.GetGamesNumber();

            return View(totalGames);
        }
    }
}