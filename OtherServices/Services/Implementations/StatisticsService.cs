using Microsoft.AspNetCore.Mvc;
using OtherServices.Data;
using OtherServices.DTO;
using OtherServices.Services.Interfaces;

namespace OtherServices.Services.Implementations
{
    public class StatisticsService : IStatisticsService
    {
        private readonly OtherDbContext _context;

        public StatisticsService(OtherDbContext context)
        {
            _context = context;
        }

        public IActionResult GetSalesStatistics(int year)
        {
            var orders = _context.Donhangs
                                 .Where(s => s.NgayLap.HasValue && s.NgayLap.Value.Year == year)
                                 .ToList();

            var salesStatistics = new List<ThongKeDoanhThu>();

            for (int month = 1; month <= 12; month++)
            {
                long? monthlyTotal = orders
                    .Where(order => order.NgayLap.HasValue && order.NgayLap.Value.Month == month)
                    .Sum(order => order.TongTien) ?? 0;

                salesStatistics.Add(new ThongKeDoanhThu
                {
                    Thang = month,
                    DoanhThu = monthlyTotal
                });
            }

            return new OkObjectResult(new
            {
                status = true,
                data = salesStatistics
            });
        }
    }
}
