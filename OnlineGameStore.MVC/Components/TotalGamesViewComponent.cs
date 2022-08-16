using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Services.Interfaces;

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
            var totalGames = await _gameService.GetGamesNumberAsync();

            return View(totalGames);
        }
    }
}