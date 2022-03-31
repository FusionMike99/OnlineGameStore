using System.Collections.Generic;

namespace OnlineGameStore.MVC.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public int? ReplyToId { get; set; }

        public int GameId { get; set; }

        public IEnumerable<CommentViewModel> Replies { get; set; }
    }
}
