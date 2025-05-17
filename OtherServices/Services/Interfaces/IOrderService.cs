using Microsoft.AspNetCore.Mvc;
using OtherServices.DTO;
using OtherServices.Models;

namespace OtherServices.Services.Interfaces
{
    public interface IOrderService
    {
        // Home functions
        Task<IActionResult> GetUserOrders(int userId, string typeMenu, int pageIndex, int pageSize);
        Task<IActionResult> CancelUserOrder(int orderId, int userId);
        Task<IActionResult> ConfirmOrderReceived(int orderId, int userId);
    }
}
