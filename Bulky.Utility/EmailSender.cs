using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    //public interface IEmailService
    //{
    //    Task SendEmailAsync(string toEmail, string subject, string content);
    //}


    public class EmailSender : IEmailSender
    {
        private readonly HttpClient _httpClient;

        public EmailSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.brevo.com/v3/smtp/email");
                request.Headers.Add("api-key", "123456");

                var payload = new
                {
                    sender = new { email = "azzahta7070@gmail.com" },
                    to = new[] { new { email = toEmail } },
                    subject = subject,
                    htmlContent = content
                };

                request.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                _httpClient.Timeout = TimeSpan.FromMinutes(30);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (TaskCanceledException ex)
            {

            }
            
        }
    }
}
