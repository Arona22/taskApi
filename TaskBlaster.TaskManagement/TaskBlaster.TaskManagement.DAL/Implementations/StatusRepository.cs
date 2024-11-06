using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Implementations
{
    public class StatusRepository : IStatusRepository
    {
        private readonly TaskBlasterDbContext _context;

        public StatusRepository(TaskBlasterDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            return await _context.Statuses
                .Select(s => new StatusDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                })
                .ToListAsync();
        }
    }
}
