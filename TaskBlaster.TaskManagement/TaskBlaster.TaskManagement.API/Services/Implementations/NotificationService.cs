using TaskBlaster.TaskManagement.API.Services.Interfaces;

namespace TaskBlaster.TaskManagement.API.Services.Implementations;

public class NotificationService : INotificationService
{
    public async Task SendAssignedNotification(int userId, int taskId)
    {
        // Implementation to send an assigned notification (e.g., email or push)
        await Task.CompletedTask;
    }

    public async Task SendUnassignedNotification(int userId, int taskId)
    {
        // Implementation to send an unassigned notification
        await Task.CompletedTask;
    }
}
