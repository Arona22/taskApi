using System.Collections.Generic;
using TaskBlaster.TaskManagement.Models.Dtos;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly List<TaskWithNotificationDto> _tasks = new List<TaskWithNotificationDto>();

        public async Task<IEnumerable<TaskWithNotificationDto>> GetTasksForNotifications()
        {
            // Fetch tasks that require notifications
            return await Task.FromResult(_tasks);
        }

        public async Task UpdateTaskNotifications()
        {
            // Update the tasks to mark them as notified.
            foreach (var task in _tasks)
            {
                task.Notification.DueDateNotificationSent = true;
                task.Notification.DayAfterNotificationSent = true;
            }

            await Task.CompletedTask;
        }
    }
}
