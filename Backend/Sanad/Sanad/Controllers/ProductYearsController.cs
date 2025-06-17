using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanad.Data;
using Sanad.DTO;
using Sanad.Models;

namespace Sanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductYearsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductYearsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var years = await _context.ProductYears
                .Select(y => new
                {
                    y.Id,
                    y.YearValue,
                    ProductCount = y.Products != null ? y.Products.Count() : 0
                })
                .ToListAsync();

            return Ok(years);
        }



        [HttpPost("CreateYear")]
        public async Task<IActionResult> Create([FromBody] CreateProductYearDto dto)
        {
            if (dto.YearValue <= 0)
                return BadRequest("Invalid year value.");

            var year = new ProductYear
            {
                YearValue = dto.YearValue
            };

            _context.ProductYears.Add(year);
            await _context.SaveChangesAsync();

            return Ok(year);
        }


        [HttpDelete("{id}/DeleteYear")]
        public async Task<IActionResult> Delete(int id)
        {
            var y = await _context.ProductYears.Include(p => p.Products).FirstOrDefaultAsync(y => y.Id == id);
            if (y == null) return NotFound();

            if (y.Products.Any())
                return BadRequest("Cannot delete year with linked products.");

            _context.ProductYears.Remove(y);
            await _context.SaveChangesAsync();
            return Ok("Deleted");
        }
    }
}
