using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class PriorityService : IPriorityService
{
    private readonly IPriorityRepository _priorityRepository;

    public PriorityService(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
    {
        return await _priorityRepository.GetAllPrioritiesAsync();
    }
}
