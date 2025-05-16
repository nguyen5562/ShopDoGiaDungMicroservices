using Microsoft.AspNetCore.Mvc;
using OtherServices.DTO;
using OtherServices.Models;

namespace OtherServices.Services.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> UpdateUserProfile(TaiKhoanDto userDto);
        Task<IEnumerable<Taikhoan>> GetAllUsersAsync();
    }
}
