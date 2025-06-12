using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sanad.Models;
using Sanad.Settings;

namespace Sanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly EmailSettings _emailSettings;

        public ContactController(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] Email form)
        {
            try
            {
                _emailSettings.SendEmail(form);
                return Ok(new { message = "Email sent successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to send email", error = ex.Message });
            }
        }
    }
}
