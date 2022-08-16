using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("games/{gameKey}")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService,
            IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> NewComment([FromRoute] string gameKey, [FromForm] EditCommentViewModel comment)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                ModelState.AddModelError("", ErrorMessages.NeedGameKey);
            }

            if (!ModelState.IsValid)
            {
                var aggregateCommentViewModel = await CreateAggregateCommentViewModel(gameKey);

                aggregateCommentViewModel.EditComment = comment;

                return View("Index", aggregateCommentViewModel);
            }

            var mappedComment = _mapper.Map<CommentModel>(comment);

            await _commentService.LeaveCommentToGameAsync(gameKey, mappedComment);

            return RedirectToAction(nameof(GetCommentsByGameKey), new { gameKey });
        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetCommentsByGameKey([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            var aggregateCommentViewModel = await CreateAggregateCommentViewModel(gameKey);

            aggregateCommentViewModel.EditComment = new EditCommentViewModel();

            return View("Index", aggregateCommentViewModel);
        }
        
        [HttpGet("updateComment/{commentId:guid}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid commentId, [FromRoute] string gameKey)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var editCommentViewModel = _mapper.Map<EditCommentViewModel>(comment);

            return PartialView("_Update", editCommentViewModel);
        }

        [HttpPost("updateComment/{commentId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateComment(Guid commentId, EditCommentViewModel comment, string gameKey)
        {
            if (commentId != comment.Id)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return PartialView("_Update", comment);
            }

            var mappedComment = _mapper.Map<CommentModel>(comment);

            await _commentService.EditCommentAsync(mappedComment);

            return Json(new { url = Url.Action(nameof(GetCommentsByGameKey), new { gameKey }) });
        }
        
        [HttpGet("removeComment/{id:guid}")]
        public async Task<IActionResult> RemoveComment([FromRoute] Guid id, [FromRoute] string gameKey)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            var commentViewModel = _mapper.Map<CommentViewModel>(comment);

            return PartialView("_Remove", commentViewModel);
        }
        
        [HttpPost("removeComment/{id:guid}")]
        [ValidateAntiForgeryToken]
        [ActionName("Remove")]
        public async Task<IActionResult> RemoveCommentConfirmed([FromRoute] Guid id, [FromRoute] string gameKey)
        {
            await _commentService.DeleteCommentAsync(id);

            return RedirectToAction(nameof(GetCommentsByGameKey), new { gameKey });
        }

        private async Task<AggregateCommentViewModel> CreateAggregateCommentViewModel(string gameKey)
        {
            var comments = await _commentService.GetAllCommentsByGameKeyAsync(gameKey);

            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            var aggregateCommentViewModel = new AggregateCommentViewModel
            {
                Comments = commentsViewModel
            };

            return aggregateCommentViewModel;
        }
    }
}