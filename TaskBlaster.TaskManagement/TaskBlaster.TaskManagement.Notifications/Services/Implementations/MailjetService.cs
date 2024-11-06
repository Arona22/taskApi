using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations;

public class MailjetService : IMailService
{
    public Task SendBasicEmailAsync(string to, string subject, string content, EmailContentType contentType)
    {
        throw new NotImplementedException();
    }

    public Task SendTemplateEmailAsync(string to, string subject, int templateId, Dictionary<string, object> variables)
    {
        throw new NotImplementedException();
    }
}