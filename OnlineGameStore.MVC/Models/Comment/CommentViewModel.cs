using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Message")]
        public string Body { get; set; }
        
        public bool IsQuoted { get; set; }

        public string ReplyToId { get; set; }

        public CommentViewModel ReplyTo { get; set; }

        public IEnumerable<CommentViewModel> Replies { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}