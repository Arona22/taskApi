using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMailService _mailService;

        public NotificationsController(IMailService mailService)
        {
            _mailService = mailService;
        }

        /// <summary>
        /// Sends a basic email
        /// </summary>
        /// <param name="inputModel">An input model used to populate the basic email</param>
        [HttpPost("emails/basic")]
        public async Task<ActionResult> SendBasicEmail([FromBody] BasicEmailInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _mailService.SendBasicEmailAsync(inputModel.To, inputModel.Subject, inputModel.Content, inputModel.ContentType);
                return Ok("Basic email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send basic email: {ex.Message}");
            }
        }

        /// <summary>
        /// Sends a templated email (optional)
        /// </summary>
        /// <param name="inputModel">An input model used to populate the templated email</param>
        [HttpPost("emails/template")]
        public async Task<ActionResult> SendTemplatedEmail([FromBody] TemplateEmailInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _mailService.SendTemplateEmailAsync(inputModel.To, inputModel.Subject, inputModel.TemplateId, inputModel.Variables);
                return Ok("Templated email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send templated email: {ex.Message}");
            }
        }
    }
}
