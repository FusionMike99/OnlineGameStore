using System.Linq;
using System.Security.Claims;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Infrastructure
{
    public static class ClaimPrincipalExtensions
    {
        public static bool IsInRole(this ClaimsPrincipal principal, Roles role)
        {
            var roleString = role.ToString();
            return principal.IsInRole(roleString);
        }
        
        public static bool IsInRoles(this ClaimsPrincipal principal, Roles roles)
        {
            var rolesArray = roles.ToString().Split(", ");
            return rolesArray.Any(principal.IsInRole);
        }
        
        public static string GetPublisherName(this ClaimsPrincipal principal)
        {
            var publisherClaim = principal.FindFirst(Claims.Publisher);
            
            return publisherClaim == null ? string.Empty : publisherClaim.Value;
        }
    }
}