using Microsoft.AspNetCore.Mvc;
using OtherServices.DTO;
using OtherServices.Models;

namespace OtherServices.Services.Interfaces
{
    public interface IProductService
    {
        // Home functions
        Task<IActionResult> GetTopSellingProducts();
        Task<IActionResult> GetProductsByBrand(int brandId, string brandName, int pageIndex, int pageSize, int maxPrice, int minPrice, string orderPrice);
        Task<IActionResult> GetProductsByCategory(int categoryId, string categoryName, int pageIndex, int pageSize, int maxPrice, int minPrice, string orderPrice);
        Task<IActionResult> GetProductDetail(int productId);
        Task<IActionResult> GetAllProducts(int pageIndex, int pageSize, int maxPrice, int minPrice, string orderPrice);
        //Task<(bool IsSuccess, string Message)> UpdateCartItemQuantityAsync(int productId, int quantity);
        Task<SanPhamct> GetProductDetailAsync(int productId);
        Task<IActionResult> SearchProducts(
         string? search,
         string? idCategories,
         string? idHangs,
         int pageIndex,
         int pageSize,
         string? maxPrice,
         string? minPrice,
         string orderPrice);
    }
}
