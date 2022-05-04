using System.Collections.Generic;

namespace OnlineGameStore.MVC.Models
{
    public class AggregateCommentViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public EditCommentViewModel EditComment { get; set; }
    }
}
