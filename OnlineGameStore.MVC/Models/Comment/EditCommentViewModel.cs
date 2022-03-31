using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class EditCommentViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        public int? ReplyToId { get; set; }
    }
}
