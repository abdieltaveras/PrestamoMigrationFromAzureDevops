using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Microsoft.Ajax.Utilities;
using System.Net;
using System.Threading;
using System.Web.Configuration;
using System.Configuration;
using System.Security.Cryptography;

namespace PrestamosMVC5.SiteUtils
{
    public class EmailService
    {
        public static bool SendMail(string to, string subject, string body, bool isHTML = false)
        {
            try
            {
                var from = ConfigurationManager.AppSettings["FromMail"];

                MailMessage mail = new MailMessage(from, to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = isHTML;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = WebConfigurationManager.AppSettings["Host"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["Port"]);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["Username"], WebConfigurationManager.AppSettings["Password"]);
                smtp.Send(mail);
                Thread.Sleep(3000);
                return true;

            } catch(Exception e)
            {
                return false;
            }
            
        }
  
    }
}