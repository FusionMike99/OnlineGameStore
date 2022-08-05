using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
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
        public IActionResult NewComment([FromRoute] string gameKey, [FromForm] EditCommentViewModel comment)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                ModelState.AddModelError("", ErrorMessages.NeedGameKey);
            }

            if (!ModelState.IsValid)
            {
                var aggregateCommentViewModel = CreateAggregateCommentViewModel(gameKey);

                aggregateCommentViewModel.EditComment = comment;

                return View("Index", aggregateCommentViewModel);
            }

            var mappedComment = _mapper.Map<Comment>(comment);

            _commentService.LeaveCommentToGame(gameKey, mappedComment);

            return RedirectToAction(nameof(GetCommentsByGameKey), new { gameKey });
        }

        [HttpGet("comments")]
        public IActionResult GetCommentsByGameKey([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }

            var aggregateCommentViewModel = CreateAggregateCommentViewModel(gameKey);

            aggregateCommentViewModel.EditComment = new EditCommentViewModel();

            return View("Index", aggregateCommentViewModel);
        }
        
        [HttpGet("updateComment/{commentId}")]
        public IActionResult UpdateComment([FromRoute] string commentId, [FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(commentId))
            {
                return BadRequest();
            }

            var comment = _commentService.GetCommentById(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            var editCommentViewModel = _mapper.Map<EditCommentViewModel>(comment);

            return PartialView("_Update", editCommentViewModel);
        }

        [HttpPost("updateComment/{commentId}")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateComment(string commentId, EditCommentViewModel comment, string gameKey)
        {
            if (commentId != comment.Id)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return PartialView("_Update", comment);
            }

            var mappedComment = _mapper.Map<Comment>(comment);

            _commentService.EditComment(mappedComment);

            return Json(new { url = Url.Action(nameof(GetCommentsByGameKey), new { gameKey }) });
        }
        
        [HttpGet("removeComment/{id}")]
        public IActionResult RemoveComment([FromRoute] string id, [FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var comment = _commentService.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            var commentViewModel = _mapper.Map<CommentViewModel>(comment);

            return PartialView("_Remove", commentViewModel);
        }
        
        [HttpPost("removeComment/{id}")]
        [ValidateAntiForgeryToken]
        [ActionName("Remove")]
        public IActionResult RemoveCommentConfirmed([FromRoute] string id, [FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            _commentService.DeleteComment(id);

            return RedirectToAction(nameof(GetCommentsByGameKey), new { gameKey });
        }

        private AggregateCommentViewModel CreateAggregateCommentViewModel(string gameKey)
        {
            var comments = _commentService.GetAllCommentsByGameKey(gameKey);

            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            var aggregateCommentViewModel = new AggregateCommentViewModel
            {
                Comments = commentsViewModel
            };

            return aggregateCommentViewModel;
        }
    }
}