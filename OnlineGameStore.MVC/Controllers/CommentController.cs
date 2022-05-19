using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
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
                ModelState.AddModelError("", "Need to pass game key");
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
                return BadRequest("Need to pass game key");
            }

            var aggregateCommentViewModel = CreateAggregateCommentViewModel(gameKey);

            aggregateCommentViewModel.EditComment = new EditCommentViewModel();

            return View("Index", aggregateCommentViewModel);
        }
        
        [HttpGet("updateComment/{commentId:int}")]
        public IActionResult UpdateComment([FromRoute] int? commentId, [FromRoute] string gameKey)
        {
            if (!commentId.HasValue)
            {
                return BadRequest("Need to pass comment id");
            }

            var comment = _commentService.GetCommentById(commentId.Value);

            if (comment == null)
            {
                return NotFound("Comment has not been found");
            }

            var editCommentViewModel = _mapper.Map<EditCommentViewModel>(comment);

            return PartialView("_Update", editCommentViewModel);
        }

        [HttpPost("updateComment/{commentId:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateComment(int commentId, EditCommentViewModel comment, string gameKey)
        {
            if (commentId != comment.Id)
            {
                return NotFound("Comment has not been found");
            }
            
            if (!ModelState.IsValid)
            {
                return PartialView("_Update", comment);
            }

            var mappedComment = _mapper.Map<Comment>(comment);

            _commentService.EditComment(mappedComment);

            return Json(new { url = Url.Action(nameof(GetCommentsByGameKey), new { gameKey }) });
        }
        
        [HttpGet("removeComment/{id:int?}")]
        public IActionResult RemoveComment([FromRoute] int? id, [FromRoute] string gameKey)
        {
            if (!id.HasValue)
            {
                return BadRequest("Need to pass comment id");
            }

            var comment = _commentService.GetCommentById(id.Value);

            if (comment == null)
            {
                return NotFound("Comment has not been found");
            }

            var commentViewModel = _mapper.Map<CommentViewModel>(comment);

            return PartialView("_Remove", commentViewModel);
        }
        
        [HttpPost("removeComment/{id:int}")]
        [ValidateAntiForgeryToken]
        [ActionName("Remove")]
        public IActionResult RemoveCommentConfirmed([FromRoute] int? id, [FromRoute] string gameKey)
        {
            if (!id.HasValue)
            {
                return BadRequest("Need to pass genre id");
            }

            _commentService.DeleteComment(id.Value);

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