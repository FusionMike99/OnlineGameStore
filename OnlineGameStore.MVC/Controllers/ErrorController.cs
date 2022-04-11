using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineGameStore.MVC.Controllers
{
    [Controller]
    public class ErrorController : Controller
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = ControllerContext.HttpContext.Features.Get<IExceptionHandlerFeature>();

            var problem = Problem(
                detail: context.Error.StackTrace ?? "No details",
                title: context.Error.Message ?? "No message");

            return problem;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
