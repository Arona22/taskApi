namespace TaskBlaster.TaskManagement.Models.Dtos;

public class TaskNotificationDto
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public bool DueDateNotificationSent { get; set; }
    public bool DayAfterNotificationSent { get; set; }
    public DateTime? LastNotificationDate { get; set; }
}
