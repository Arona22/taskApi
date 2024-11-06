namespace TaskBlaster.TaskManagement.Models;

public class Envelope<T> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int MaxCount { get; set; }
    public IEnumerable<T> Items { get; set; } = null!;
}