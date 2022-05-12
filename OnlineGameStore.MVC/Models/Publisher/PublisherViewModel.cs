using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class PublisherViewModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Home page")]
        public string HomePage { get; set; }
    }
}