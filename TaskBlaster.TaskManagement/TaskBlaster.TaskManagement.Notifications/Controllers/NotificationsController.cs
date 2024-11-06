using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Notifications.Models;

namespace TaskBlaster.TaskManagement.Notifications.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    /// <summary>
    /// Sends a basic email
    /// </summary>
    /// <param name="inputModel">An input model used to populate the basic email</param>
    [HttpPost("emails/basic")]
    public Task<ActionResult> SendBasicEmail([FromBody] BasicEmailInputModel inputModel)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sends a templated email (optional)
    /// </summary>
    /// <param name="inputModel">An input model used to populate the templated email</param>
    [HttpPost("emails/template")]
    public Task<ActionResult> SendTemplatedEmail([FromBody] TemplateEmailInputModel inputModel)
    {
        throw new NotImplementedException();
    }
}