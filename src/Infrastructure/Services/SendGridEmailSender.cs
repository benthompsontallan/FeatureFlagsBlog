using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly string _sendGridApiKey;

        public SendGridEmailSender(IConfiguration configuration)
        {
            _sendGridApiKey = configuration["SendGridApiKey"];
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var msg = CreateEmail(email, subject, message);

            await SendEmail(msg);
        }

        private SendGridMessage CreateEmail(string email, string subject, string message)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("noreply@tallan.com", "Tallan"));

            msg.AddTo(email);

            msg.SetSubject(subject);

            msg.AddContent(MimeType.Html, message);

            return msg;
        }

        private async Task SendEmail(SendGridMessage msg)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
