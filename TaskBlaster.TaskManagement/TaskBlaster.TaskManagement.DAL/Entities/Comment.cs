namespace TaskBlaster.TaskManagement.DAL.Entities;

public class Comment
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string Author { get; set; } = null!;
    public string ContentAsMarkdown { get; set; } = "";
    public DateTime CreatedDate { get; set; }

    public Task Task { get; set; } = null!;
    public User User { get; set; } = null!;
}
