using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineGameStore.BLL.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public Guid? PublisherId { get; set; }
        
        public PublisherEntity Publisher { get; set; }
    }
}