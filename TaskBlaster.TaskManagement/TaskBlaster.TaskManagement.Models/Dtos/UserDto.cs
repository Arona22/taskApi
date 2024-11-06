namespace TaskBlaster.TaskManagement.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string? ProfileImageUrl { get; set; }
}
