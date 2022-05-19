using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Comment : IBaseEntity<int>
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Body { get; set; }
        
        public bool IsQuoted { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int? ReplyToId { get; set; }

        public Comment ReplyTo { get; set; }

        public ICollection<Comment> Replies { get; set; }
        
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}