using System;
using System.Collections.Generic;

namespace OnlineGameStore.DAL.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }
        
        public bool IsQuoted { get; set; }

        public Guid GameId { get; set; }

        public GameEntity Game { get; set; }

        public Guid? ReplyToId { get; set; }

        public CommentEntity ReplyTo { get; set; }

        public ICollection<CommentEntity> Replies { get; set; }
    }
}