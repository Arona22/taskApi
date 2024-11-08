namespace TaskBlaster.TaskManagement.Notifications.Models;

public class TemplateEmailInputModel
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public int TemplateId { get; set; }
        public Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
    }