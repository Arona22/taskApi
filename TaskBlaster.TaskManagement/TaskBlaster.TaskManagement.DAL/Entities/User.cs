namespace TaskBlaster.TaskManagement.DAL.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string? ProfileImageUrl { get; set; }
}
