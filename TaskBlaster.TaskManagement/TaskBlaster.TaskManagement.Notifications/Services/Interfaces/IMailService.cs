using TaskBlaster.TaskManagement.Notifications.Models;

namespace TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

public interface IMailService
{
    /// <summary>
    /// Sends a basic email with a mail service
    /// </summary>
    /// <param name="to">Recipient of the email</param>
    /// <param name="subject">The subject of the email</param>
    /// <param name="content">The content of the email</param>
    /// <param name="contentType">The content type of the email (HTML, Text)</param>
    Task SendBasicEmailAsync(string to, string subject, string content, EmailContentType contentType);
    
    /// <summary>
    /// Sends a templated email with a mail service (optional)
    /// </summary>
    /// <param name="to">Recipient of the email</param>
    /// <param name="subject">The subject of the email</param>
    /// <param name="templateId">The id of the template to use</param>
    /// <param name="variables">The variables which are injected into the template</param>
    Task SendTemplateEmailAsync(string to, string subject, int templateId, Dictionary<string, object> variables);
}