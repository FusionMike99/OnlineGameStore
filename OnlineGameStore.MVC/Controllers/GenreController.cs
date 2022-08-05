using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Infrastructure;
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
        public IActionResult Create()
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

        [HttpGet("update/{genreId}")]
        public IActionResult Update([FromRoute] string genreId)
        {
            if (string.IsNullOrWhiteSpace(genreId))
            {
                return BadRequest();
            }

            var genre = _genreService.GetGenreById(genreId);

            if (genre == null)
            {
                return NotFound();
            }

            var editGenreViewModel = _mapper.Map<EditGenreViewModel>(genre);

            ConfigureEditGenreViewModel(editGenreViewModel);

            return View(editGenreViewModel);
        }

        [HttpPost("update/{genreId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(string genreId, [FromForm] EditGenreViewModel genre)
        {
            if (genreId != genre.Id)
            {
                return NotFound();
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
        public IActionResult GetGenreById([FromRoute] string genreId)
        {
            if (string.IsNullOrWhiteSpace(genreId))
            {
                return BadRequest();
            }

            var genre = _genreService.GetGenreById(genreId);

            if (genre == null)
            {
                return NotFound();
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
        public IActionResult Remove([FromForm] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            _genreService.DeleteGenre(id);

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
                ModelState.AddModelError(nameof(GenreViewModel.Name), ErrorMessages.GenreNameExist);
            }
        }
    }
}