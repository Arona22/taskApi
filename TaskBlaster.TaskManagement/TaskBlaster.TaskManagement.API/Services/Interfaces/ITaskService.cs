using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface ITaskService
{
    /// <summary>
    /// Returns all tasks by a provided criteria as a paginated result
    /// </summary>
    /// <param name="query">A query which is used to paginate and filter the result</param>
    /// <returns>A filtered and paginated list of tasks</returns>
    Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query);

    /// <summary>
    /// Returns a single task by id
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <returns>A single task or null</returns>
    Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId);

    /// <summary>
    /// Creates a new task
    /// </summary>
    /// <param name="task">Input model used to populate the new task</param>
    Task<int> CreateNewTaskAsync(TaskInputModel task);

    /// <summary>
    /// Archives a task by id
    /// </summary>
    /// <param name="taskId">The id of the task which should be archived</param>
    Task ArchiveTaskByIdAsync(int taskId);

    /// <summary>
    /// Assigns a user from a task, and notifies the user via email
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="userId">The id of the user</param>
    Task AssignUserToTaskAsync(int taskId, int userId);

    /// <summary>
    /// Unassigns a user from a task, and notifies the user via email
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="userId">The id of the user</param>
    Task UnassignUserFromTaskAsync(int taskId, int userId);

    /// <summary>
    /// Updates the status of a task, e.g. 'pending', 'completed'
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model associated with the status update</param>
    Task UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel);

    /// <summary>
    /// Updates the priority of a task, e.g. 'Critical', 'High'
    /// </summary>
    /// <param name="taskId">The id of the task</param>
    /// <param name="inputModel">The input model associated with the priority update</param>
    Task UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel);
}