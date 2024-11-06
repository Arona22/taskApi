using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface IPriorityService
{
    /// <summary>
    /// Retrieves all priorities asynchronously
    /// </summary>
    /// <returns>A collection of all priorities</returns>
    Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync();
}