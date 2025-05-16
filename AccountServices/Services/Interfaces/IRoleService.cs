using Microsoft.AspNetCore.Mvc;
using AccountServices.DTO;
using AccountServices.Models;


namespace AccountServices.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetUserRolesAsync(int userId);
        Task AssignRoleToUserAsync(int userId, int roleId);
        Task RemoveRoleFromUserAsync(int userId, int roleId);
    }

}
