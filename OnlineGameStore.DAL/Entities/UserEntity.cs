using System;
using Microsoft.AspNetCore.Identity;

namespace OnlineGameStore.DAL.Entities
{
    public class UserEntity : IdentityUser<Guid>
    {
        public Guid? PublisherId { get; set; }
        
        public PublisherEntity Publisher { get; set; }
    }
}