using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtherServices.Data;
using OtherServices.DTO;
using OtherServices.Models;
using OtherServices.Services.Interfaces;

namespace OtherServices.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly OtherDbContext _context;

        public OrderService(OtherDbContext context)
        {
            _context = context;
        }

        // Home functions
        public async Task<IActionResult> GetUserOrders(int user, string typeMenu, int pageIndex, int pageSize)
        {
            int userId = _context.Taikhoans.FirstOrDefault(s => s.MaTaiKhoan == user)?.MaTaiKhoan ?? 0;

            

            var query = typeMenu switch
            {
                "tatca" => _context.Donhangs
                    .Where(a => a.MaTaiKhoan == userId)
                    .Include(item => item.Vanchuyen)
                    .Select(a => new DonhangDto
                    {
                        MaDonHang = a.MaDonHang,
                        TinhTrang = a.TinhTrang,
                        NgayDatHang = a.NgayLap,
                        TongTien = a.TongTien,
                        Vanchuyen = new VanchuyenDto
                        {
                            NguoiNhan = a.Vanchuyen.NguoiNhan,
                            DiaChi = a.Vanchuyen.DiaChi
                        }
                    }),
                "chuathanhtoan" => _context.Donhangs
                    .Where(a => a.MaTaiKhoan == userId && a.TinhTrang == 0)
                    .Include(item => item.Vanchuyen)
                    .Select(a => new DonhangDto
                    {
                        MaDonHang = a.MaDonHang,
                        TinhTrang = a.TinhTrang,
                        NgayDatHang = a.NgayLap,
                        TongTien = a.TongTien,
                        Vanchuyen = new VanchuyenDto
                        {
                            NguoiNhan = a.Vanchuyen.NguoiNhan,
                            DiaChi = a.Vanchuyen.DiaChi
                        }
                    }),
                "choxacnhan" => _context.Donhangs
                    .Where(a => a.MaTaiKhoan == userId && a.TinhTrang == 1)
                    .Include(item => item.Vanchuyen)
                    .Select(a => new DonhangDto
                    {
                        MaDonHang = a.MaDonHang,
                        TinhTrang = a.TinhTrang,
                        NgayDatHang = a.NgayLap,
                        TongTien = a.TongTien,
                        Vanchuyen = new VanchuyenDto
                        {
                            NguoiNhan = a.Vanchuyen.NguoiNhan,
                            DiaChi = a.Vanchuyen.DiaChi
                        }
                    }),
                "dangvanchuyen" => _context.Donhangs
                    .Where(a => a.MaTaiKhoan == userId && a.TinhTrang == 2)
                    .Include(item => item.Vanchuyen)
                    .Select(a => new DonhangDto
                    {
                        MaDonHang = a.MaDonHang,
                        TinhTrang = a.TinhTrang,
                        NgayDatHang = a.NgayLap,
                        TongTien = a.TongTien,
                        Vanchuyen = new VanchuyenDto
                        {
                            NguoiNhan = a.Vanchuyen.NguoiNhan,
                            DiaChi = a.Vanchuyen.DiaChi
                        }
                    }),
                "dahoanthanh" => _context.Donhangs
                    .Where(a => a.MaTaiKhoan == userId && a.TinhTrang == 3)
                    .Include(item => item.Vanchuyen)
                    .Select(a => new DonhangDto
                    {
                        MaDonHang = a.MaDonHang,
                        TinhTrang = a.TinhTrang,
                        NgayDatHang = a.NgayLap,
                        TongTien = a.TongTien,
                        Vanchuyen = new VanchuyenDto
                        {
                            NguoiNhan = a.Vanchuyen.NguoiNhan,
                            DiaChi = a.Vanchuyen.DiaChi
                        }
                    }),
                "dahuy" => _context.Donhangs
                    .Where(a => a.MaTaiKhoan == userId && a.TinhTrang == 4)
                    .Include(item => item.Vanchuyen)
                    .Select(a => new DonhangDto
                    {
                        MaDonHang = a.MaDonHang,
                        TinhTrang = a.TinhTrang,
                        NgayDatHang = a.NgayLap,
                        TongTien = a.TongTien,
                        Vanchuyen = new VanchuyenDto
                        {
                            NguoiNhan = a.Vanchuyen.NguoiNhan,
                            DiaChi = a.Vanchuyen.DiaChi
                        }
                    }),
                _ => _context.Donhangs
                    .Where(a => a.MaTaiKhoan == userId)
                    .Include(item => item.Vanchuyen)
                    .Select(a => new DonhangDto
                    {
                        MaDonHang = a.MaDonHang,
                        TinhTrang = a.TinhTrang,
                        NgayDatHang = a.NgayLap,
                        TongTien = a.TongTien,
                        Vanchuyen = new VanchuyenDto
                        {
                            NguoiNhan = a.Vanchuyen.NguoiNhan,
                            DiaChi = a.Vanchuyen.DiaChi
                        }
                    })
            };

            var count = await query.CountAsync();
            var dt = await query
                .OrderByDescending(item => item.MaDonHang)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new OkObjectResult(new
            {
                donhang = dt,
                typeMenu = typeMenu,
                totalCount = count
            });
        }

        public async Task<IActionResult> CancelUserOrder(int orderId, int userId)
        {
            var dh = await _context.Donhangs.FirstOrDefaultAsync(d => d.MaDonHang == orderId && d.MaTaiKhoan == userId);
            if (dh == null)
            {
                return new NotFoundResult();
            }

            dh.TinhTrang = 4;
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { status = true });
        }

        public async Task<IActionResult> ConfirmOrderReceived(int orderId, int userId)
        {
            var dh = await _context.Donhangs.FirstOrDefaultAsync(d => d.MaDonHang == orderId && d.MaTaiKhoan == userId);
            if (dh == null)
            {
                return new NotFoundResult();
            }

            dh.TinhTrang = 3;
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { status = true });
        }
    }
}
