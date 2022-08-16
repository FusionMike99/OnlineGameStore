using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.Identity.Services.Interfaces
{
    public interface IRoleService
    {
        Task AttachRoleToUserAsync(string userName, string roleName);

        Task<IEnumerable<RoleModel>> GetAllRolesAsync();
    }
}