using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }
        
        public bool IsQuoted { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public Guid? ReplyToId { get; set; }

        public Comment ReplyTo { get; set; }

        public ICollection<Comment> Replies { get; set; }
    }
}