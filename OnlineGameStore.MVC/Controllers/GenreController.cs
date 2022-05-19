using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("genres")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService,
            IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet("new")]
        public ViewResult Create()
        {
            var editGenreViewModel = new EditGenreViewModel();

            ConfigureEditGenreViewModel(editGenreViewModel);

            return View(editGenreViewModel);
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] EditGenreViewModel genre)
        {
            VerifyGenre(genre);

            if (!ModelState.IsValid)
            {
                ConfigureEditGenreViewModel(genre);

                return View(genre);
            }

            var mappedGenre = _mapper.Map<Genre>(genre);

            _genreService.CreateGenre(mappedGenre);

            return RedirectToAction(nameof(GetGenres));
        }

        [HttpGet("update/{genreId:int}")]
        public IActionResult Update([FromRoute] int? genreId)
        {
            if (!genreId.HasValue)
            {
                return BadRequest("Need to pass genre id");
            }

            var genre = _genreService.GetGenreById(genreId.Value);

            if (genre == null)
            {
                return NotFound("Genre has not been found");
            }

            var editGenreViewModel = _mapper.Map<EditGenreViewModel>(genre);

            ConfigureEditGenreViewModel(editGenreViewModel);

            return View(editGenreViewModel);
        }

        [HttpPost("update/{genreId:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int genreId, [FromForm] EditGenreViewModel genre)
        {
            if (genreId != genre.Id)
            {
                return NotFound("Genre has not been found");
            }
            
            VerifyGenre(genre);

            if (!ModelState.IsValid)
            {
                ConfigureEditGenreViewModel(genre);

                return View(genre);
            }

            var mappedGenre = _mapper.Map<Genre>(genre);

            _genreService.EditGenre(mappedGenre);

            return RedirectToAction(nameof(GetGenres));
        }

        [HttpGet("{genreId}")]
        public IActionResult GetGenreById([FromRoute] int? genreId)
        {
            if (!genreId.HasValue)
            {
                return BadRequest("Need to pass genre id");
            }

            var genre = _genreService.GetGenreById(genreId.Value);

            if (genre == null)
            {
                return NotFound("Genre has not been found");
            }

            var genreViewModel = _mapper.Map<GenreViewModel>(genre);

            return View("Details", genreViewModel);
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            var genres = _genreService.GetAllParentGenres();

            var genresViewModel = _mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return View("Index", genresViewModel);
        }

        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public IActionResult Remove([FromForm] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Need to pass genre id");
            }

            _genreService.DeleteGenre(id.Value);

            return RedirectToAction(nameof(GetGenres));
        }

        private void ConfigureEditGenreViewModel(EditGenreViewModel model)
        {
            model.Genres = new SelectList(_genreService.GetAllWithoutGenre(model.Id),
                nameof(Genre.Id),
                nameof(Genre.Name));
        }

        private void VerifyGenre(EditGenreViewModel genre)
        {
            var checkResult = _genreService.CheckNameForUnique(genre.Id, genre.Name);

            if (checkResult)
            {
                ModelState.AddModelError("Name", "Genre with same name exist.");
            }
        }
    }
}