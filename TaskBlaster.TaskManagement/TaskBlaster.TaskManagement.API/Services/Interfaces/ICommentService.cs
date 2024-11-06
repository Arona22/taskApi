using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface ICommentService
{
    /// <summary>
    /// Gets all comments associated with a tag
    /// </summary>
    /// <param name="taskId">The task id</param>
    /// <returns>A list of comments</returns>
    Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId);
    
    /// <summary>
    /// Adds a comment to a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="user">The author of the comment (authenticated user)</param>
    /// <param name="comment">The input model containing the comment</param>
    Task AddCommentToTaskAsync(int taskId, string user, CommentInputModel comment);
    
    /// <summary>
    /// Removes a comment from a task
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="commentId">The id of the comment</param>
    Task RemoveCommentFromTaskAsync(int taskId, int commentId);
}