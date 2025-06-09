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
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ServicesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await dbContext.Services
                .Select(s => new ServiceDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Details = s.Details
                })
                .ToListAsync();

            return Ok(services);
        }
        [HttpPost("createService")]
        public async Task<IActionResult> CreateService([FromForm] CreateUpdateServiceDto dto)
        {
            string? fileName = null;

            if (dto.Image != null)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ServiceImages");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);
            }

            var service = new Service
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = fileName != null ? $"/ServiceImages/{fileName}" : null,
                Details = dto.Details,
            };

            dbContext.Services.Add(service);
            await dbContext.SaveChangesAsync();

            return Ok("Service created");
        }

        [HttpPut("{id}/updateService")]
        public async Task<IActionResult> UpdateService(int id, [FromForm] CreateUpdateServiceDto dto)
        {
            var service = await dbContext.Services.FindAsync(id);
            if (service == null) return NotFound("Service not found");

            if (!string.IsNullOrWhiteSpace(dto.Title))
                service.Title = dto.Title;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                service.Description = dto.Description;

            if (dto.Details != null && dto.Details.Any())
                service.Details = dto.Details;


            if (dto.Image != null)
            {
               
                if (!string.IsNullOrEmpty(service.ImageUrl))
                {
                    var oldPath = Path.Combine("wwwroot", service.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

               
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ServiceImages");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Image.FileName);
                var path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await dto.Image.CopyToAsync(stream);

                service.ImageUrl = $"/ServiceImages/{fileName}";
            }

            await dbContext.SaveChangesAsync();
            return Ok("Service updated");
        }


        [HttpDelete("{id}/deleteService")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await dbContext.Services.FindAsync(id);
            if (service == null) return NotFound("Service not found");

            
            if (!string.IsNullOrEmpty(service.ImageUrl))
            {
                var path = Path.Combine("wwwroot", service.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            dbContext.Services.Remove(service);
            await dbContext.SaveChangesAsync();

            return Ok("Service deleted");
        }




    }
}
