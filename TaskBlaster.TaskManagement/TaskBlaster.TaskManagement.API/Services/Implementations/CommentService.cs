using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsAssociatedWithTaskAsync(int taskId)
    {
        return await _commentRepository.GetCommentsAssociatedWithTaskAsync(taskId);
    }

    public async Task AddCommentToTaskAsync(int taskId, string user, CommentInputModel comment)
    {
        await _commentRepository.AddCommentToTaskAsync(taskId, user, comment);
    }

    public async Task RemoveCommentFromTaskAsync(int taskId, int commentId)
    {
        await _commentRepository.RemoveCommentFromTaskAsync(taskId, commentId);
    }
}
