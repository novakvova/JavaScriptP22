using LohikaBackend.Abastract;
using LohikaBackend.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace LohikaBackend.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.Content;
            bodyBuilder.TextBody = "This is some plain text";
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = bodyBuilder.ToMessageBody(); //new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            //emailMessage.HtmlBody = message.Content;

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                    client.Disconnect(true);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
