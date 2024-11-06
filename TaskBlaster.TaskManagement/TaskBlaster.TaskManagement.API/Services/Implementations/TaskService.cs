using TaskBlaster.TaskManagement.API.Services.Interfaces;
using TaskBlaster.TaskManagement.DAL.Interfaces;
using TaskBlaster.TaskManagement.Models;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Models.InputModels;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly INotificationService _notificationService;

    public TaskService(ITaskRepository taskRepository, INotificationService notificationService)
    {
        _taskRepository = taskRepository;
        _notificationService = notificationService;
    }

    public async Task<Envelope<TaskDto>> GetPaginatedTasksByCriteriaAsync(TaskCriteriaQueryParams query)
    {
        return await _taskRepository.GetPaginatedTasksByCriteriaAsync(query);
    }

    public async Task<TaskDetailsDto?> GetTaskByIdAsync(int taskId)
    {
        return await _taskRepository.GetTaskByIdAsync(taskId);
    }

    public async Task<int> CreateNewTaskAsync(TaskInputModel task)
    {
        return await _taskRepository.CreateNewTaskAsync(task);
    }

    public async Task ArchiveTaskByIdAsync(int taskId)
    {
        await _taskRepository.ArchiveTaskByIdAsync(taskId);
    }

    public async Task AssignUserToTaskAsync(int taskId, int userId)
    {
        await _taskRepository.AssignUserToTaskAsync(taskId, userId);
        await _notificationService.SendAssignedNotification(userId, taskId);
    }

    public async Task UnassignUserFromTaskAsync(int taskId, int userId)
    {
        await _taskRepository.UnassignUserFromTaskAsync(taskId, userId);
        await _notificationService.SendUnassignedNotification(userId, taskId);
    }

    public async Task UpdateTaskStatusAsync(int taskId, StatusInputModel inputModel)
    {
        await _taskRepository.UpdateTaskStatusAsync(taskId, inputModel);
    }

    public async Task UpdateTaskPriorityAsync(int taskId, PriorityInputModel inputModel)
    {
        await _taskRepository.UpdateTaskPriorityAsync(taskId, inputModel);
    }
}
