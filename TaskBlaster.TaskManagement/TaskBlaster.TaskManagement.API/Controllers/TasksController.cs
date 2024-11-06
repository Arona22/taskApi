using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;
using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ICommentService _commentService;

    public TasksController(ITaskService taskService, ICommentService commentService)
    {
        _taskService = taskService;
        _commentService = commentService;
    }

    // Gets paginated results for tasks filtered by a query
    [HttpGet("")]
    public async Task<ActionResult<Envelope<TaskDto>>> GetPaginatedTasksByCriteria([FromQuery] TaskCriteriaQueryParams query)
    {
        var tasks = await _taskService.GetPaginatedTasksByCriteriaAsync(query);
        return Ok(tasks);
    }

    // Gets a task by ID
    [HttpGet("{taskId}", Name = "GetTaskById")]
    public async Task<ActionResult<TaskDetailsDto?>> GetTaskById(int taskId)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);
        if (task == null) return NotFound();
        return Ok(task);
    }

    // Creates a new task
    [HttpPost("")]
    public async Task<ActionResult> CreateNewTask([FromBody] TaskInputModel task)
    {
        var createdTaskId = await _taskService.CreateNewTaskAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { taskId = createdTaskId });
    }
    
    // Archives a task by ID
    [HttpDelete("{taskId}")]
    public async Task<ActionResult> ArchiveTaskById(int taskId)
    {
        await _taskService.ArchiveTaskByIdAsync(taskId);
        return NoContent();
    }

    // Assigns a user to a task
    [HttpPatch("{taskId}/assign/{userId}")]
    public async Task<ActionResult> AssignUserToTask(int taskId, int userId)
    {
        await _taskService.AssignUserToTaskAsync(taskId, userId);
        return NoContent();
    }

    // Unassigns a user from a task
    [HttpPatch("{taskId}/unassign/{userId}")]
    public async Task<ActionResult> UnassignUserFromTask(int taskId, int userId)
    {
        await _taskService.UnassignUserFromTaskAsync(taskId, userId);
        return NoContent();
    }

    // Updates the status of a task
    [HttpPatch("{taskId}/status")]
    public async Task<ActionResult> UpdateTaskStatus(int taskId, [FromBody] StatusInputModel inputModel)
    {
        await _taskService.UpdateTaskStatusAsync(taskId, inputModel);
        return NoContent();
    }

    // Updates the priority of a task
    [HttpPatch("{taskId}/priority")]
    public async Task<ActionResult> UpdateTaskPriority(int taskId, [FromBody] PriorityInputModel inputModel)
    {
        await _taskService.UpdateTaskPriorityAsync(taskId, inputModel);
        return NoContent();
    }

    // Gets comments associated with a task
    [HttpGet("{taskId}/comments")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsAssociatedWithTask(int taskId)
    {
        var comments = await _commentService.GetCommentsAssociatedWithTaskAsync(taskId);
        return Ok(comments);
    }

    // Adds a comment to a task
    [HttpPost("{taskId}/comments")]
    public async Task<ActionResult> AddCommentToTask(int taskId, [FromBody] CommentInputModel inputModel)
    {
        await _commentService.AddCommentToTaskAsync(taskId, "DefaultUser", inputModel);
        return NoContent();
    }


    // Removes a comment from a task
    [HttpDelete("{taskId}/comments/{commentId}")]
    public async Task<ActionResult> RemoveCommentFromTask(int taskId, int commentId)
    {
        await _commentService.RemoveCommentFromTaskAsync(taskId, commentId);
        return NoContent();
    }
}
