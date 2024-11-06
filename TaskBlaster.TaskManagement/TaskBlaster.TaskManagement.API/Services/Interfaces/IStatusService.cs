using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface IStatusService
{
    /// <summary>
    /// Retrieves all statuses asynchronously
    /// </summary>
    /// <returns>A collection of all statuses</returns>
    Task<IEnumerable<StatusDto>> GetAllStatusesAsync();
}