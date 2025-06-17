using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sanad.Data;
using Sanad.Models;

namespace Sanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PartnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPartners()
        {
            var partners = await _context.Partners.ToListAsync();
            return Ok(partners);
        }

        [HttpPost("AddPartner")]
        public async Task<IActionResult> CreatePartner([FromBody] Partner partner)
        {
            if (partner == null || string.IsNullOrWhiteSpace(partner.Name) || string.IsNullOrWhiteSpace(partner.LogoUrl))
                return BadRequest("Invalid partner data.");

            _context.Partners.Add(partner);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPartnerById), new { id = partner.Id }, partner);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartnerById(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null)
                return NotFound();

            return Ok(partner);
        }

        [HttpPut("{id}/UpdatePartner")]
        public async Task<IActionResult> UpdatePartner(int id, [FromBody] Partner updatedPartner)
        {
            var existing = await _context.Partners.FindAsync(id);
            if (existing == null)
                return NotFound();

            
            if (!string.IsNullOrWhiteSpace(updatedPartner.Name))
                existing.Name = updatedPartner.Name;

            if (!string.IsNullOrWhiteSpace(updatedPartner.LogoUrl))
                existing.LogoUrl = updatedPartner.LogoUrl;

            if (!string.IsNullOrWhiteSpace(updatedPartner.WebsiteUrl))
                existing.WebsiteUrl = updatedPartner.WebsiteUrl;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Partner updated successfully" });
        
    }

        [HttpDelete("{id}/DeletePartner")]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null)
                return NotFound();

            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Partner deleted successfully" });
        }
    }
}

    