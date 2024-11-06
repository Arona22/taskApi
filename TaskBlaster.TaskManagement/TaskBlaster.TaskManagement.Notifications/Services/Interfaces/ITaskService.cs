using TaskBlaster.TaskManagement.Models.Dtos;

namespace TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

public interface ITaskService
{
    /// <summary>
    /// Get all tasks which have not been notified and are due.
    /// </summary>
    Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications();

    /// <summary>
    /// Updates the status of the task notifications, after the emails have been sent
    /// </summary>
    Task UpdateTaskNotifications();
}