using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    /// <summary>
    /// Returns a list of all statuses
    /// </summary>
    /// <returns>A list of all statuses</returns>
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<StatusDto>>> GetAllStatuses()
    {
        var statuses = await _statusService.GetAllStatusesAsync();
        return Ok(statuses);
    }
}
