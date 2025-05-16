using Microsoft.AspNetCore.Mvc;
using OrderServices.DTO;

namespace OrderServices.Services.Interfaces
{
    public interface ICartService
    {
        IActionResult GetCart(ISession session);
        JsonResult AddItemToCart(int productId, ISession session, bool checkOnly);
        ActionResult GetCartTotal(ISession session);
        JsonResult DeleteItemFromCart(long productId, ISession session);
        JsonResult UpdateCartItemQuantity(int productId, int quantity);
        JsonResult ClearCart(ISession session);
        Task<JsonResult> Checkout(ThongTinThanhToan thanhToan, int? userId);
    }
}
