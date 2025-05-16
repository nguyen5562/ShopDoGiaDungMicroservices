using Microsoft.AspNetCore.Mvc;
using AccountServices.DTO;
using AccountServices.Models;

namespace AccountServices.Services.Interfaces
{
    public interface IAccountService
    {
        IActionResult GetAccounts( int page, int pageSize);
        IActionResult UpdateAccountRole(int matk, int macv);
        IActionResult DeleteAccount(int matk);
        Task<Taikhoan> GetAccountByIdAsync(int maTaiKhoan);
    }
}
