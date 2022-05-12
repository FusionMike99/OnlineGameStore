using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("games")]
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

        [HttpPost("{gameKey}/[action]")]
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

        [HttpGet("{gameKey}/comments")]
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