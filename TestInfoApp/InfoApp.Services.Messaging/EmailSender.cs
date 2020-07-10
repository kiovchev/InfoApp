using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace InfoApp.Services.Messaging
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
