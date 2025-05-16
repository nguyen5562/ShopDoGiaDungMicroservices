using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccountServices.Data;
using AccountServices.DTO;
using AccountServices.Models;
using AccountServices.Services.Interfaces;
using System.Security.Cryptography;
namespace AccountServices.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly AccountDbContext _context;

        public RoleService(AccountDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetUserRolesAsync(int userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }

        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            var userRole = new UserRole { UserId = userId, RoleId = roleId };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoleFromUserAsync(int userId, int roleId)
        {
            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }
    }

}
