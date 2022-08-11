using System.Threading.Tasks;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var totalGames = await _gameService.GetGamesNumber();

            return View(totalGames);
        }
    }
}