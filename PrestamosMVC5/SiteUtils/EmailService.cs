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
using System.IO;
using System.Data.Common;

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
    
        public static string CreateTemplatePrestamo(PrestamoBLL.Entidades.Cliente cl, PrestamoBLL.Entidades.Prestamo prestamo)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/MailTemplates/EmailTemplate.html")))
            {
                body = reader.ReadToEnd();

                //En esta parte se deben espeficicar los parametros que se enviaran al email.
                // ejemplo
                // Body = body.Replace({fname}, nombre)
                body = body.Replace("{Nombre}", cl.NombreCompleto);
                body = body.Replace("{Fecha}", DateTime.Now.ToString());
                body = body.Replace("{Prestamo}", prestamo.MontoCapital.ToString());
                body = body.Replace("{tasa}", prestamo.InteresGastoDeCierre.ToString());
                body = body.Replace("{cuotas}", prestamo.CantidadDePeriodos.ToString());

                return body;
            }
        }
    }
}