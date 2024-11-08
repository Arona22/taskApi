namespace TaskBlaster.TaskManagement.Notifications.Models
{
    public class BasicEmailInputModel
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public EmailContentType ContentType { get; set; } = EmailContentType.Text;
    }
}