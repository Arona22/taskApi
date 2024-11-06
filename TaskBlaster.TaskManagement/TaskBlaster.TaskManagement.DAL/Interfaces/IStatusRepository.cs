using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface IStatusRepository
{
    Task<IEnumerable<StatusDto>> GetAllStatusesAsync();
}