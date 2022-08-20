using System;

namespace OnlineGameStore.BLL.Models.General
{
    public class UserModel
    {
        public Guid Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string Role { get; set; }
        
        public Guid? PublisherId { get; set; }
    }
}