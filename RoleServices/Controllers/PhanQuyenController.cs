using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleServices.Data;

namespace RoleServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhanQuyenController : ControllerBase
    {
        private readonly RoleDbContext _context;

        public PhanQuyenController(RoleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _context.PhanQuyens
                .Include(pq => pq.MaChucNangNavigation)
                .Include(pq => pq.MaHanhDongNavigation)
                .Include(pq => pq.MaDonViNavigation)
                .ToListAsync();

            return Ok(permissions);
        }
    }

}
