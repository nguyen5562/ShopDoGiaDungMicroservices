using OrderServices.DTO;

namespace OrderServices.Services.Interfaces
{
    public interface IPermissionService
    {
        //Task<bool> HasPermission(int userId, string functionCode, string actionCode, int maDonVi);
        Task<List<PermissionDto>> GetUserPermissions(int userId);
    }
}
