using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PrestamoWS.Services
{
    public interface IMailService
    {
        string PasswordResetUrl { get; }
        Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody);
    }
    public class MailSettings
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string FromEmail { get; set; }
        public string Host { get; set; }
        public string PasswordResetUrl { get; set; }
    }

    public class SmtpSettings
    {
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
    }
    public class MailService : IMailService
    {
        private readonly MailSettings _mailConfig;
        private readonly SmtpSettings _smtpConfig;
        public MailService(MailSettings mailConfig, SmtpSettings smtpConfig)
        {
            _mailConfig = mailConfig;
            _smtpConfig = smtpConfig;

        }
        public string PasswordResetUrl => _mailConfig.PasswordResetUrl;
        public async Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_mailConfig.FromEmail);
                mail.To.Add(ToEmail);
                mail.Subject = Subject;
                mail.Body = HTMLBody;
                mail.IsBodyHtml = true;

                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  

                using (SmtpClient smtp = new SmtpClient(_mailConfig.Host, _mailConfig.Port))
                {
                    smtp.UseDefaultCredentials = _smtpConfig.UseDefaultCredentials;
                    smtp.EnableSsl = _smtpConfig.EnableSsl;
                    smtp.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
                    smtp.Send(mail);
                }
            }
            /*using (MailMessage message = new MailMessage())
            {
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_mailConfig.FromEmail);
                message.To.Add(new MailAddress(ToEmail));
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = HTMLBody;
                smtp.Port = _mailConfig.Port;
                smtp.Host = _mailConfig.Host;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
            }*/
        }
    }
}
