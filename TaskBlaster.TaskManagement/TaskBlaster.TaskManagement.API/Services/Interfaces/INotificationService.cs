namespace TaskBlaster.TaskManagement.API.Services.Interfaces;

public interface INotificationService
{
    /// <summary>
    /// Send an assigned notification to a specific user
    /// </summary>
    /// <param name="userId">The id of the user which has been assigned to the task</param>
    /// <param name="taskId">The id of the task</param>
    Task SendAssignedNotification(int userId, int taskId);
    
    /// <summary>
    /// Send an unassigned notification to a specific user
    /// </summary>
    /// <param name="userId">The id of the user which has been unassigned from the task</param>
    /// <param name="taskId">The id of the task</param>
    Task SendUnassignedNotification(int userId, int taskId);
}