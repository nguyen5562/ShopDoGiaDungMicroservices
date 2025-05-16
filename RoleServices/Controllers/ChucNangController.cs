using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using RoleServices.Data;
using RoleServices.Models;

namespace RoleServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChucNangController : ControllerBase
    {
        private readonly RoleDbContext _context;

        public ChucNangController(RoleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChucNangs()
        {
            var chucNangs = await EntityFrameworkQueryableExtensions.ToListAsync(_context.ChucNangs
                .Select(cn => new ChucNang
                {
                    MaChucNang = cn.MaChucNang,
                    TenChucNang = cn.TenChucNang
                })
                );
            return Ok(chucNangs);
        }

    }

}
