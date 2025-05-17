using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtherServices.Data;
using OtherServices.DTO;
using OtherServices.Models;
using OtherServices.Services.Interfaces;
using System.Linq;

namespace OtherServices.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly OtherDbContext _context;
        private readonly IMinioService _minioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(OtherDbContext context, IMinioService minioService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _minioService = minioService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Implement all methods from IProductService

        // Admin functions
        public async Task<SanPhamct> GetProductDetailAsync(int productId)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            // Truy vấn sản phẩm dựa trên productId
            var sanpham = await (from sp in _context.Sanphams
                                 join h in _context.Hangsanxuats on sp.MaHang equals h.MaHang
                                 join dm in _context.Danhmucsanphams on sp.MaDanhMuc equals dm.MaDanhMuc
                                 where sp.MaSp == productId
                                 select new SanPhamct
                                 {
                                     MaSp = sp.MaSp,
                                     TenSp = sp.TenSp,
                                     MoTa = sp.MoTa,
                                     Anh1 = sp.Anh1,
                                     Anh2 = sp.Anh2,
                                     Anh3 = sp.Anh3,
                                     Anh4 = sp.Anh4,
                                     Anh5 = sp.Anh5,
                                     Anh6 = sp.Anh6,
                                     SoLuongDaBan = sp.SoLuongDaBan,
                                     SoLuongTrongKho = sp.SoLuongTrongKho,
                                     GiaTien = sp.GiaTien,
                                     Hang = h.TenHang,
                                     DanhMuc = dm.TenDanhMuc,
                                     MaHang = h.MaHang,
                                     MaDanhMuc = dm.MaDanhMuc
                                 }).FirstOrDefaultAsync();

            if (sanpham == null)
            {
                return null;
            }

            // Tạo Pre-signed URL cho mỗi ảnh
            sanpham.Anh1 = await _minioService.GetPreSignedUrlAsync(sanpham.Anh1,httpContext);
            sanpham.Anh2 = await _minioService.GetPreSignedUrlAsync(sanpham.Anh2,httpContext);
            sanpham.Anh3 = await _minioService.GetPreSignedUrlAsync(sanpham.Anh3,httpContext);
            sanpham.Anh4 = await _minioService.GetPreSignedUrlAsync(sanpham.Anh4,httpContext);
            sanpham.Anh5 = await _minioService.GetPreSignedUrlAsync(sanpham.Anh5,httpContext);
            sanpham.Anh6 = await _minioService.GetPreSignedUrlAsync(sanpham.Anh6,httpContext);

            return sanpham;
        }
        
        //public async Task<(bool IsSuccess, string Message)> UpdateCartItemQuantityAsync(int productId, int quantity)
        //{
        //    if (quantity <= 0)
        //    {
        //        return (false, "Số lượng phải lớn hơn 0.");
        //    }

        //    // Truy xuất sản phẩm từ cơ sở dữ liệu
        //    var product = await _context.Sanphams.FirstOrDefaultAsync(p => p.MaSp == productId);

        //    if (product == null)
        //    {
        //        return (false, "Sản phẩm không tồn tại.");
        //    }

        //    // Kiểm tra tồn kho
        //    if (product.SoLuongTrongKho < quantity)
        //    {
        //        return (false, $"Số lượng yêu cầu vượt quá tồn kho. Tồn kho hiện tại: {product.SoLuongTrongKho}.");
        //    }

        //    // Nếu cần, bạn có thể cập nhật số lượng trong cơ sở dữ liệu tại đây
        //    // Ví dụ: Giảm tồn kho nếu muốn cập nhật tồn kho theo giỏ hàng
        //    // product.Stock -= quantity;
        //    // _context.Products.Update(product);
        //    // await _context.SaveChangesAsync();

        //    // Trả về phản hồi thành công
        //    return (true, "Số lượng sản phẩm còn đủ.");
        //}

        // Home functions
        public async Task<IActionResult> GetTopSellingProducts()
        {
            var sanpham = await _context.Sanphams.OrderByDescending(a => a.SoLuongDaBan).Take(6).ToListAsync();
            var danhmucsp = await _context.Danhmucsanphams.ToListAsync();
            var hang = await _context.Hangsanxuats.ToListAsync();
            var httpContext = _httpContextAccessor.HttpContext;
            // Tạo Pre-signed URL cho ảnh sản phẩm
            foreach (var sp in sanpham)
            {
                sp.Anh1 = await _minioService.GetPreSignedUrlAsync(sp.Anh1,httpContext);
            }

            return new OkObjectResult(new
            {
                sanpham = sanpham,
                danhmucsp = danhmucsp,
                hang = hang
            });
        }

        public async Task<IActionResult> GetProductsByBrand(int brandId, string brandName, int pageIndex, int pageSize, int maxPrice, int minPrice, string orderPrice)
        {
            IQueryable<Sanpham> query = _context.Sanphams.Where(s => s.MaHang == brandId);

            if (maxPrice != 0)
            {
                query = query.Where(item => item.GiaTien < maxPrice && item.GiaTien > minPrice);
            }

            query = orderPrice == "tang" ? query.OrderBy(item => item.GiaTien) : query.OrderByDescending(item => item.GiaTien);

            var count = await query.CountAsync();
            var model = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var httpContext = _httpContextAccessor.HttpContext;
            // Tạo Pre-signed URL cho ảnh sản phẩm
            foreach (var sp in model)
            {
                sp.Anh1 = await _minioService.GetPreSignedUrlAsync(sp.Anh1,httpContext);
            }

            return new OkObjectResult(new
            {
                sanpham = model,
                maxPrice = maxPrice,
                minPrice = minPrice,
                orderPrice = orderPrice,
                tenhang = brandName,
                idHang = brandId,
                totalCount = count
            });
        }

        public async Task<IActionResult> GetProductsByCategory(int categoryId, string categoryName, int pageIndex, int pageSize, int maxPrice, int minPrice, string orderPrice)
        {
            IQueryable<Sanpham> query = _context.Sanphams.Where(s => s.MaDanhMuc == categoryId);

            if (maxPrice != 0)
            {
                query = query.Where(item => item.GiaTien < maxPrice && item.GiaTien > minPrice);
            }
            var httpContext = _httpContextAccessor.HttpContext;
            query = orderPrice == "tang" ? query.OrderBy(item => item.GiaTien) : query.OrderByDescending(item => item.GiaTien);

            var count = await query.CountAsync();
            var model = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            // Tạo Pre-signed URL cho ảnh sản phẩm
            foreach (var sp in model)
            {
                sp.Anh1 = await _minioService.GetPreSignedUrlAsync(sp.Anh1,httpContext);
            }

            return new OkObjectResult(new
            {
                sanpham = model,
                maxPrice = maxPrice,
                minPrice = minPrice,
                orderPrice = orderPrice,
                tendanhmuc = categoryName,
                idCategory = categoryId,
                totalCount = count
            });
        }

        public async Task<IActionResult> GetProductDetail(int productId)
        {
            var danhgia = await (from a in _context.Taikhoans
                                 join b in _context.Danhgiasanphams on a.MaTaiKhoan equals b.MaTaiKhoan
                                 join c in _context.Sanphams on b.MaSp equals c.MaSp
                                 where c.MaSp == productId
                                 orderby b.NgayDanhGia descending
                                 select new CommentView()
                                 {
                                     TenTaiKhoan = a.Ten,
                                     DanhGia = b.DanhGia,
                                     NoiDung = b.NoiDungBinhLuan,
                                     ThoiGian = b.NgayDanhGia
                                 }).ToListAsync();

            int? sum = danhgia.Sum(item => item.DanhGia);
            double sao = danhgia.Count() > 0 ? Math.Round((double)sum / danhgia.Count(), 1) : 0;
            var httpContext = _httpContextAccessor.HttpContext;
            var sp = await _context.Sanphams.FindAsync(productId);
            if(sp == null)
            {
                return new OkObjectResult(new { status = false });
            }
            // Tạo Pre-signed URL cho ảnh sản phẩm
            sp.Anh1 = await _minioService.GetPreSignedUrlAsync(sp.Anh1, httpContext);
            sp.Anh2 = await _minioService.GetPreSignedUrlAsync(sp.Anh2, httpContext);
            sp.Anh3 = await _minioService.GetPreSignedUrlAsync(sp.Anh3, httpContext);
            sp.Anh4 = await _minioService.GetPreSignedUrlAsync(sp.Anh4, httpContext);
            sp.Anh5 = await _minioService.GetPreSignedUrlAsync(sp.Anh5, httpContext);
            sp.Anh6 = await _minioService.GetPreSignedUrlAsync(sp.Anh6, httpContext);

            return new OkObjectResult(new
            {
                sanpham = sp,
                danhgia = danhgia,
                sao = sao
            });
        }

        public async Task<IActionResult> GetAllProducts(int pageIndex, int pageSize, int maxPrice, int minPrice, string orderPrice)
        {
            IQueryable<Sanpham> model = _context.Sanphams;

           
            if (maxPrice != 0)
            {
                model = model.Where(item => item.GiaTien < maxPrice && item.GiaTien > minPrice);
            }

            model = orderPrice == "tang" ? model.OrderBy(item => item.GiaTien) : model.OrderByDescending(item => item.GiaTien);

            var count = await model.CountAsync();
            var dt = await model.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var httpContext = _httpContextAccessor.HttpContext;
            // Tạo Pre-signed URL cho ảnh sản phẩm
            foreach (var sp in dt)
            {
                sp.Anh1 = await _minioService.GetPreSignedUrlAsync(sp.Anh1,httpContext);
            }

            return new OkObjectResult(new
            {
                sanpham = dt,
                maxPrice = maxPrice,
                minPrice = minPrice,
                orderPrice = orderPrice,
                totalCount = count
            });
        }

        public async Task<IActionResult> SearchProducts(
            string? search,
            string? idCategories,
            string? idHangs,
            int pageIndex,
            int pageSize,
            string? maxPrice,
            string? minPrice,
            string orderPrice)
        {
            // Khởi tạo query sản phẩm
            IQueryable<Sanpham> model = _context.Sanphams;

            // Áp dụng bộ lọc theo danh mục nếu có
            if (!string.IsNullOrEmpty(idCategories))
            {
                int[] arrayCategory = Array.ConvertAll(idCategories.Split(","), int.Parse);
                model = model.Where(item => arrayCategory.Contains(item.MaDanhMuc??0));
            }

            // Áp dụng bộ lọc theo hãng nếu có
            if (!string.IsNullOrEmpty(idHangs))
            {
                int[] arrayIdHang = Array.ConvertAll(idHangs.Split(","), int.Parse);
                model = model.Where(item => arrayIdHang.Contains(item.MaHang ?? 0));
            }

            // Áp dụng điều kiện tìm kiếm theo tên sản phẩm nếu có
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(s => s.TenSp.Contains(search));
            }

            // Áp dụng bộ lọc theo giá tối đa nếu có
            if (!string.IsNullOrEmpty(maxPrice) && int.TryParse(maxPrice, out int priceMax))
            {
                model = model.Where(item => item.GiaTien <= priceMax);
            }

            // Áp dụng bộ lọc theo giá tối thiểu nếu có
            if (!string.IsNullOrEmpty(minPrice) && int.TryParse(minPrice, out int priceMin))
            {
                model = model.Where(item => item.GiaTien >= priceMin);
            }

            // Sắp xếp theo giá
            model = orderPrice == "tang" ? model.OrderBy(item => item.GiaTien) : model.OrderByDescending(item => item.GiaTien);

            // Đếm tổng số sản phẩm phù hợp với bộ lọc
            var count = await model.CountAsync();

            // Lấy dữ liệu trang
            var dt = await model.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var httpContext = _httpContextAccessor.HttpContext;
            // Tạo URL tạm thời cho ảnh sản phẩm
            foreach (var sp in dt)
            {
                sp.Anh1 = await _minioService.GetPreSignedUrlAsync(sp.Anh1,httpContext);
            }

            // Trả về kết quả
            return new OkObjectResult(new
            {
                sanpham = dt,
                maxPrice = maxPrice,
                minPrice = minPrice,
                orderPrice = orderPrice,
                totalCount = count
            });
        }


    }
}
