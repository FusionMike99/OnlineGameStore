using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("genres")]
    [AuthorizeByRoles(Permissions.ManagerPermission)]
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
        public async Task<IActionResult> Create()
        {
            var editGenreViewModel = new EditGenreViewModel();
            await ConfigureEditGenreViewModel(editGenreViewModel);

            return View(editGenreViewModel);
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EditGenreViewModel genre)
        {
            await VerifyGenre(genre);

            if (!ModelState.IsValid)
            {
                await ConfigureEditGenreViewModel(genre);

                return View(genre);
            }

            var mappedGenre = _mapper.Map<GenreModel>(genre);
            await _genreService.CreateGenreAsync(mappedGenre);

            return RedirectToAction(nameof(GetGenres));
        }

        [HttpGet("update/{genreId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid genreId)
        {
            var genre = await _genreService.GetGenreByIdAsync(genreId);

            if (genre == null)
            {
                return NotFound();
            }

            var editGenreViewModel = _mapper.Map<EditGenreViewModel>(genre);
            await ConfigureEditGenreViewModel(editGenreViewModel);

            return View(editGenreViewModel);
        }

        [HttpPost("update/{genreId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid genreId, [FromForm] EditGenreViewModel genre)
        {
            if (genreId != genre.Id)
            {
                return NotFound();
            }
            
            await VerifyGenre(genre);

            if (!ModelState.IsValid)
            {
                await ConfigureEditGenreViewModel(genre);
                return View(genre);
            }

            var mappedGenre = _mapper.Map<GenreModel>(genre);
            await _genreService.EditGenreAsync(mappedGenre);

            return RedirectToAction(nameof(GetGenres));
        }

        [HttpGet("{genreId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenreById([FromRoute] Guid genreId)
        {
            var genre = await _genreService.GetGenreByIdAsync(genreId);

            if (genre == null)
            {
                return NotFound();
            }

            var genreViewModel = _mapper.Map<GenreViewModel>(genre);

            return View("Details", genreViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetAllParentGenresAsync();
            var genresViewModel = _mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return View("Index", genresViewModel);
        }

        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromForm] Guid id)
        {
            await _genreService.DeleteGenreAsync(id);

            return RedirectToAction(nameof(GetGenres));
        }

        private async Task ConfigureEditGenreViewModel(EditGenreViewModel model)
        {
            var genres = await _genreService.GetAllWithoutGenreAsync(model.Id);
            model.Genres = new SelectList(genres, nameof(GenreEntity.Id), nameof(GenreEntity.Name));
        }

        private async Task VerifyGenre(EditGenreViewModel genre)
        {
            var checkResult = await _genreService.CheckNameForUniqueAsync(genre.Id, genre.Name);

            if (checkResult)
            {
                ModelState.AddModelError(nameof(GenreViewModel.Name), ErrorMessages.GenreNameExist);
            }
        }
    }
}