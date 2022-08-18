using System.Linq;
using System.Security.Claims;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Infrastructure
{
    public static class ClaimPrincipalExtensions
    {
        public static bool IsInRoles(this ClaimsPrincipal principal, Roles roles)
        {
            var rolesArray = roles.ToString().Split(", ");
            return rolesArray.Any(principal.IsInRole);
        }
    }
}