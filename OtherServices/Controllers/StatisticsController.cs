using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OtherServices.Attributes;
using OtherServices.Services.Interfaces;

namespace OtherServices.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyAllowedOrigins")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [Permission("ThongKe", "Xem")]
        [HttpPost("statistics")]
        public IActionResult GetSalesStatistics([FromBody] int year)
        {
            return _statisticsService.GetSalesStatistics(year);
        }
    }
}
