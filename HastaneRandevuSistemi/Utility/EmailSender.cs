using Microsoft.AspNetCore.Identity.UI.Services;

namespace HastaneRandevuSistemi.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //buraya email gönderme işlemlerinizi ekleyebilirsiniz.
            return Task.CompletedTask;
        }
    }
}