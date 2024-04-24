using System.ComponentModel.DataAnnotations;
using TaskMgmt.Services.CustomExceptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using TaskMgmt.Services.ConfigurationClass;
using TaskMgmt.Services.Interfaces;

namespace TaskMgmt.Services
{

    public class EmailNotificationService : INotificationService, IDisposable
    {
        private readonly SmtpClient _client;
        private readonly EmailConfiguration _emailConfig;

        public EmailNotificationService(IOptions<EmailConfiguration> config)
        {
            _emailConfig = config.Value;
            _client = GetSmtpClient();
        }

        public async Task BulkNotifyAsync(IEnumerable<string> recipientIds, string subject, string message)
        {
            var text = CreateEmailMessage(recipientIds, subject, message);
            await SendEmailAsync(text);
        }

        public async Task NotifyAsync(string recipientId, string subject, string message)
        {
            var text = CreateEmailMessage(new List<string> { recipientId }, subject, message);
            await SendEmailAsync(text);
        }

        private MimeMessage CreateEmailMessage(IEnumerable<string> recipientIds, string subject, string message)
        {
            var text = new MimeMessage();
            text.From.Add(new MailboxAddress(_emailConfig.SenderName, _emailConfig.SenderEmail));
            var emailValidator = new EmailAddressAttribute();
            foreach (var recipientId in recipientIds)
            {
                if (!emailValidator.IsValid(recipientId))
                {
                    throw new InvalidEmailException("Provided email is not valid" + recipientId);
                }
                text.To.Add(new MailboxAddress("", recipientId));
            }
            text.Subject = subject;
            text.Body = new TextPart("html")
            {
                Text = message
            };
            return text;
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            try
            {
                await _client.SendAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private SmtpClient GetSmtpClient()
        {
            var client = new SmtpClient();
            client.Connect(_emailConfig.SmtpServer, _emailConfig.SmtpPort, _emailConfig.UseSsl);
            client.Authenticate(_emailConfig.SenderEmail, _emailConfig.SenderPassword);
            return client;
        }

        public void Dispose()
        {
            _client?.Disconnect(true);
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
