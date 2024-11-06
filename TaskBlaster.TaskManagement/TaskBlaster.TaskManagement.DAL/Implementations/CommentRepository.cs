using Microsoft.EntityFrameworkCore;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using Task = System.Threading.Tasks.Task;

namespace TaskBlaster.TaskManagement.DAL.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TaskBlasterDbContext _context;

        public CommentRepository(TaskBlasterDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentToTaskAsync(int taskId, string user, CommentInputModel comment)
        {
            var newComment = new Entities.Comment
            {
                TaskId = taskId,
                Author = user,
                ContentAsMarkdown = comment.ContentAsMarkdown,
                CreatedDate = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId)
        {
            return await _context.Comments
                .Where(c => c.TaskId == taskId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Author = c.Author,
                    ContentAsMarkdown = c.ContentAsMarkdown,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();
        }

        public async Task RemoveCommentFromTaskAsync(int taskId, int commentId)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.TaskId == taskId && c.Id == commentId);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
