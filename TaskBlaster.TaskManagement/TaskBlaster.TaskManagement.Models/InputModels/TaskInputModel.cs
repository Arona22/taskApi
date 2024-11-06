namespace TaskBlaster.TaskManagement.Models.InputModels;

public class TaskInputModel
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public int StatusId { get; set; }
    public int PriorityId { get; set; }
    public DateTime? DueDate { get; set; }
    public string? AssignedToUser { get; set; }
}
