using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface IPriorityRepository
{
    Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync();
}