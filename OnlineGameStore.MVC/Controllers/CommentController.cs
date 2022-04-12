using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;
using System.Collections.Generic;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("game")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost("{gameKey}/[action]")]
        public IActionResult NewComment([FromRoute] string gameKey, [FromBody] EditCommentViewModel comment)
        {
            if (string.IsNullOrWhiteSpace(gameKey) || !ModelState.IsValid)
            {
                return BadRequest("Something went wrong");
            }

            var mappedComment = _mapper.Map<Comment>(comment);

            var leavedComment = _commentService.LeaveCommentToGame(gameKey, mappedComment);

            var commentViewModel = _mapper.Map<CommentViewModel>(leavedComment);

            return Json(commentViewModel);
        }

        [HttpPost("{gameKey}/comments")]
        [ResponseCache(Duration = 60)]
        public IActionResult GetCommentsByGameKey([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest("Something wrong");
            }

            var comments = _commentService.GetAllCommentsByGameKey(gameKey);

            var commentsViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);

            return Json(commentsViewModel);
        }
    }
}
