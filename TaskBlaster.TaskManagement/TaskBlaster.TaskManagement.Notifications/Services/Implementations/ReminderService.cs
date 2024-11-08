using Hangfire;
using System;
using System.Threading.Tasks;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations
{
    public class ReminderService
    {
        private readonly ITaskService _taskService;
        private readonly IMailService _mailService;

        public ReminderService(ITaskService taskService, IMailService mailService)
        {
            _taskService = taskService;
            _mailService = mailService;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task SendDueDateReminder()
        {
            // Fetch tasks that are due or overdue and need a reminder
            var tasks = await _taskService.GetTasksForNotifications();

            foreach (var task in tasks)
            {
                if (task.DueDate == DateTime.UtcNow.Date || task.DueDate == DateTime.UtcNow.AddDays(1).Date)
                {
                    var emailContent = $"Reminder: Your task '{task.Title}' is due on {task.DueDate}.";

                    // Send email notification
                    await _mailService.SendBasicEmailAsync(task.AssignedToUser, "Task Due Reminder", emailContent, EmailContentType.Text);

                    // Update task notification status
                    await _taskService.UpdateTaskNotifications();
                }
            }
        }
    }
}
