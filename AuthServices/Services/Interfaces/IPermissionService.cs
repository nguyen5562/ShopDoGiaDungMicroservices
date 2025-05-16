using AuthServices.DTO;

namespace AuthServices.Services.Interfaces
{
    public interface IPermissionService
    {
        //Task<bool> HasPermission(int userId, string functionCode, string actionCode, int maDonVi);
        Task<List<PermissionDto>> GetUserPermissions(int userId);
    }
}
