using System.Text.Json;
using TaskBlaster.TaskManagement.Notifications.Models;
using TaskBlaster.TaskManagement.Notifications.Services.Interfaces;

namespace TaskBlaster.TaskManagement.Notifications.Services.Implementations
{
    public class MailjetService : IMailService
    {
        private readonly HttpClient _httpClient;

        public MailjetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.mailjet.com/v3.1/");
        }

        public async Task SendBasicEmailAsync(string to, string subject, string content, EmailContentType contentType)
        {
            var payload = new
            {
                Messages = new[]
                {
                    new
                    {
                        From = new { Email = "sender@example.com", Name = "Sender Name" },
                        To = new[] { new { Email = to, Name = "Recipient Name" } },
                        Subject = subject,
                        TextPart = contentType == EmailContentType.Text ? content : null,
                        HTMLPart = contentType == EmailContentType.Html ? content : null
                    }
                }
            };

            var contentJson = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("send", contentJson);
            response.EnsureSuccessStatusCode();
        }

        public async Task SendTemplateEmailAsync(string to, string subject, int templateId, Dictionary<string, object> variables)
        {
            var payload = new
            {
                Messages = new[]
                {
                    new
                    {
                        From = new { Email = "sender@example.com", Name = "Sender Name" },
                        To = new[] { new { Email = to, Name = "Recipient Name" } },
                        Subject = subject,
                        TemplateID = templateId,
                        TemplateLanguage = true,
                        Variables = variables
                    }
                }
            };

            var contentJson = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("send", contentJson);
            response.EnsureSuccessStatusCode();
        }
    }
}
