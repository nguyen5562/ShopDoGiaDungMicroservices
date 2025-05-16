using Microsoft.AspNetCore.Mvc;
using AuthServices.DTO;

namespace AuthServices.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IActionResult> Login(LoginInfo loginInfo);
        Task<AuthResult> LoginAsync(LoginInfo loginInfo);
        Task<IActionResult> Register(RegisterInfo registerInfo);
        UserDto GetUserById(string userId);
        Task<IActionResult> CreateUserAsync(CreateUserRequest request);
        //Task<List<PermissionDto>> GetPermissionsByUserId(int userId);
    }
}
