using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanad.Data;
using Sanad.DTO;
using Sanad.Models;
using System.Text.Json;

namespace Sanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment env;

        public ProductsController(ApplicationDbContext dbContext, IWebHostEnvironment env)
        {
            this.dbContext = dbContext;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll([FromQuery] int? year, [FromQuery] string? categoryName)
        {
            var query = dbContext.Products
                                 .Include(p => p.Category)
                                 .Include(p => p.ProductYear)
                                 .AsQueryable();

            if (year.HasValue)
                query = query.Where(p => p.ProductYear != null && p.ProductYear.YearValue == year.Value);

            if (!string.IsNullOrWhiteSpace(categoryName))
                query = query.Where(p => p.Category != null && p.Category.Name.ToLower() == categoryName.ToLower());

            var products = await query
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Description,
                    p.LongDescription,
                    p.ImageUrl,
                    p.Thumbnails,
                    p.Tags,
                    p.BuyLink,
                    p.DetailsLink,
                    p.DemoLink,
                    p.CategoryId,
                    p.ProductYearId,
                    CategoryName = p.Category != null ? p.Category.Name : null,
                    Year = p.ProductYear != null ? p.ProductYear.YearValue : (int?)null
                }).ToListAsync();

            return Ok(products);
        }



        [HttpGet("{id}/ProductDetails")]
        public async Task<ActionResult<object>> GetById(int id)
        {
            var product = await dbContext.Products
                                         .Include(p => p.Category)
                                         .Include(p => p.ProductYear)
                                         .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(new
            {
                product.Id,
                product.Title,
                product.Description,
                product.LongDescription,
                product.ImageUrl,
                product.Tags,
                product.BuyLink,
                product.DetailsLink,
                product.DemoLink,
                product.Thumbnails,
                CategoryName = product.Category.Name,
                Year = product.ProductYear.YearValue
            });
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.ImageUrl == null || dto.ImageUrl.Length == 0)
                return BadRequest("Image file is required.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageUrl.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageUrl.CopyToAsync(stream);
            }

            var thumbnailsUrls = new List<string>();
            if (dto.Thumbnails != null)
            {
                foreach (var thumb in dto.Thumbnails)
                {
                    var thumbName = Guid.NewGuid().ToString() + Path.GetExtension(thumb.FileName);
                    var thumbPath = Path.Combine(uploadsFolder, thumbName);

                    using (var stream = new FileStream(thumbPath, FileMode.Create))
                    {
                        await thumb.CopyToAsync(stream);
                    }

                    thumbnailsUrls.Add(thumbName);
                }
            }

            var product = new Product
            {
                Title = dto.Title,
                Description = dto.Description,
                LongDescription = dto.LongDescription,
                ImageUrl = fileName,
                Tags = dto.Tags,
                BuyLink = dto.BuyLink,
                DetailsLink = dto.DetailsLink,
                DemoLink = dto.DemoLink,
                CategoryId = dto.CategoryId,
                ProductYearId = dto.ProductYearId,
                Thumbnails = thumbnailsUrls.Any() ? JsonSerializer.Serialize(thumbnailsUrls) : null
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Product created successfully.", product.Id });
        }

        [HttpPut("{id}/updateproductinfo")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] CreateProductDto dto)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound("Product not found");

            if (!string.IsNullOrEmpty(dto.Title))
                product.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Description))
                product.Description = dto.Description;

            if (!string.IsNullOrEmpty(dto.LongDescription))
                product.LongDescription = dto.LongDescription;

            if (!string.IsNullOrEmpty(dto.Tags))
                product.Tags = dto.Tags;

            if (!string.IsNullOrEmpty(dto.BuyLink))
                product.BuyLink = dto.BuyLink;

            if (!string.IsNullOrEmpty(dto.DetailsLink))
                product.DetailsLink = dto.DetailsLink;

            if (!string.IsNullOrEmpty(dto.DemoLink))
                product.DemoLink = dto.DemoLink;

            if (dto.CategoryId > 0)
            {
                var categoryExists = await dbContext.Categories.AnyAsync(c => c.Id == dto.CategoryId);
                if (!categoryExists)
                    return BadRequest("Invalid CategoryId.");
                product.CategoryId = dto.CategoryId;
            }

            if (dto.ProductYearId > 0)
            {
                var yearExists = await dbContext.ProductYears.AnyAsync(y => y.Id == dto.ProductYearId);
                if (!yearExists)
                    return BadRequest("Invalid ProductYearId.");
                product.ProductYearId = dto.ProductYearId;
            }

            if (dto.Thumbnails != null && dto.Thumbnails.Any())
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages");
                var thumbnailsUrls = new List<string>();

                foreach (var thumb in dto.Thumbnails)
                {
                    var thumbName = Guid.NewGuid().ToString() + Path.GetExtension(thumb.FileName);
                    var thumbPath = Path.Combine(uploadsFolder, thumbName);

                    using (var stream = new FileStream(thumbPath, FileMode.Create))
                    {
                        await thumb.CopyToAsync(stream);
                    }

                    thumbnailsUrls.Add(thumbName);
                }

                product.Thumbnails = JsonSerializer.Serialize(thumbnailsUrls);
            }

            if (dto.ImageUrl != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages");
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldPath = Path.Combine(uploadsFolder, product.ImageUrl);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageUrl.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ImageUrl.CopyToAsync(stream);

                product.ImageUrl = fileName;
            }

            await dbContext.SaveChangesAsync();
            return Ok(new { message = "Product updated successfully" });
        }

        [HttpDelete("{id}/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound("Product not found");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages");

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(uploadsFolder, product.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            if (!string.IsNullOrEmpty(product.Thumbnails))
            {
                var thumbnails = JsonSerializer.Deserialize<List<string>>(product.Thumbnails);
                if (thumbnails != null)
                {
                    foreach (var thumb in thumbnails)
                    {
                        var thumbPath = Path.Combine(uploadsFolder, thumb);
                        if (System.IO.File.Exists(thumbPath))
                            System.IO.File.Delete(thumbPath);
                    }
                }
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully" });
        }
    }
}
