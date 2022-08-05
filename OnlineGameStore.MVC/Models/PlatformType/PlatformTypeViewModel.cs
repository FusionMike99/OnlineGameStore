using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class PlatformTypeViewModel
    {
        [UIHint("HiddenInput")]
        public string Id { get; set; }

        public string Type { get; set; }
    }
}