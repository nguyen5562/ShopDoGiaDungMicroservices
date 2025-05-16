using Microsoft.AspNetCore.Mvc;

namespace OtherServices.Services.Interfaces
{
    public interface IStatisticsService
    {
        IActionResult GetSalesStatistics(int year);
    }
}
