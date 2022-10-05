using Microsoft.AspNetCore.Authorization;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Infrastructure
{
    public class AuthorizeByRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeByRolesAttribute(Roles roles)
        {
            Roles = roles.ToString();
        }
    }
}