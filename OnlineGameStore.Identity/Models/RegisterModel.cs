using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.Identity.Models
{
    public class RegisterModel : UserModel
    {
        public string Password { get; set; }
    }
}