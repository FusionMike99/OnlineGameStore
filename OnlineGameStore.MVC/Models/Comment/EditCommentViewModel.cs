using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class EditCommentViewModel
    {
        [UIHint("HiddenInput")]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Body { get; set; }
        
        [UIHint("HiddenInput")]
        public bool IsQuoted { get; set; }
        
        [UIHint("HiddenInput")]
        public Guid GameId { get; set; }

        [UIHint("HiddenInput")]
        public Guid? ReplyToId { get; set; }
    }
}