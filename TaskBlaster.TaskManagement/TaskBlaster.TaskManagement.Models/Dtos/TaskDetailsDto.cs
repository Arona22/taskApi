namespace TaskBlaster.TaskManagement.Models.Dtos;

public class TaskDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string Status { get; set; } = "";
    public string Priority { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string CreatedBy { get; set; } = "";
    public string? AssignedToUser { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
}
