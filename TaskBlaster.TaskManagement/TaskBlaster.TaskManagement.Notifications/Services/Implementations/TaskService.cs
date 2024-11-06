using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations;

public class TaskService : ITaskService
{
    public Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskNotifications()
    {
        throw new NotImplementedException();
    }
}