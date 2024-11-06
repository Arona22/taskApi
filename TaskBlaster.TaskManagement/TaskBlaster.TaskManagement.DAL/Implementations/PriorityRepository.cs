using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.DAL.Implementations
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly TaskBlasterDbContext _context;

        public PriorityRepository(TaskBlasterDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
        {
            return await _context.Priorities
                .Select(p => new PriorityDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                })
                .ToListAsync();
        }
    }
}
