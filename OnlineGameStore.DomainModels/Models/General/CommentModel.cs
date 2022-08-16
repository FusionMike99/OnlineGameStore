using System;
using System.Collections.Generic;

namespace OnlineGameStore.DomainModels.Models.General
{
    public class CommentModel : BaseModel
    {
        public string Name { get; set; }

        public string Body { get; set; }
        
        public bool IsQuoted { get; set; }

        public Guid GameId { get; set; }

        public GameModel Game { get; set; }

        public Guid? ReplyToId { get; set; }

        public CommentModel ReplyTo { get; set; }

        public ICollection<CommentModel> Replies { get; set; }
    }
}