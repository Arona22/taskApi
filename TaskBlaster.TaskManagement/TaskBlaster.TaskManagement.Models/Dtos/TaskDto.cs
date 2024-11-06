namespace TaskBlaster.TaskManagement.Models.Dtos;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime? DueDate { get; set; }
    public string? AssignedToUser { get; set; }
}
