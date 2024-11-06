using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations
{
    public class TagRepository : ITagRepository
    {
        private readonly TaskBlasterDbContext _context;

        public TagRepository(TaskBlasterDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewTagAsync(TagInputModel inputModel)
        {
            var tag = new Entities.Tag
            {
                Name = inputModel.Name,
                Description = inputModel.Description
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
        {
            return await _context.Tags
                .Select(t => new TagDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                })
                .ToListAsync();
        }
    }
}
