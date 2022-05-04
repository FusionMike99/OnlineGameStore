using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Models;
using System.Diagnostics;

namespace OnlineGameStore.MVC.Controllers
{
    [Controller]
    public class ErrorController : Controller
    {
        [Route("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(errorViewModel);
        }
    }
}
