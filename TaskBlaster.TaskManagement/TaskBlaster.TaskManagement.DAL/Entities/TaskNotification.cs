namespace TaskBlaster.TaskManagement.DAL.Entities;

public class TaskNotification
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public bool DueDateNotificationSent { get; set; }
    public bool DayAfterNotificationSent { get; set; }
    public DateTime? LastNotificationDate { get; set; }

    public Task Task { get; set; } = null!;
}

