using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.DAL.Interfaces;

public interface ICommentRepository
{
    Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId);
    
    Task AddCommentToTaskAsync(int taskId, string user, CommentInputModel comment);
    
    Task RemoveCommentFromTaskAsync(int taskId, int commentId);
}