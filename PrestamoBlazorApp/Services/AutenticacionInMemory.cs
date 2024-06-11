using Blazored.LocalStorage;
using DevBox.Core.Classes.Utils;
using Microsoft.Extensions.Configuration;
using MudBlazor;
using PrestamoBlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class AutenticacionInMemory : ServiceBase
    {
        public AutenticacionInMemory(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration, localStorageService)
        {
        }


        private readonly Dictionary<string, string> usuarios = new Dictionary<string, string>
        {
            
            {"CRISTAL", "1235"},
            {"Abdiel", "5678"}

        };

        public async Task<bool> LoginAsync(string usuario, string contrasena)
        {
            if (usuarios.TryGetValue(usuario, out var storedContrasena))
            {
                return await Task.Run(() => contrasena == storedContrasena);
            }

            return false;
        }

        public async Task<bool> LogoutAsync(string usuario, string contrasena)
        {
            if (usuarios.TryGetValue(usuario, out var storedContrasena))
            {
                return await Task.Run(() => contrasena == storedContrasena);
            }

            return false;
        }
        async Task<string> GetCompaniaAsync()
        {
            // Implementa la lógica para obtener información del negocio
            return await Task.Run(() => "InformacionDelNegocio");
        }
        async Task<List<string>> GetLocalidadesAsync()
        {
            // Implementa la lógica para obtener la lista de localidades
            return await Task.Run(() => new List<string> { "Localidad1", "Localidad2", "Localidad3" });
        }


        //async Task<bool> OlvideContrasenaAsync(string usuario)
        //{
        //    throw new System.NotImplementedException();
        //}

        public async Task<string> OlvideContrasenaAsync(string usuario)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    // Set the recipient email address
                    mailMessage.To.Add("cristaltaveras47@gmail.com");

                    // Set the email subject
                    mailMessage.Subject = "Recuperar contraseña";

                    // Set the email body
                    //mailMessage.Body = await NotifyMessageBySnackBar("Se ha enviado correctamente", Severity.Info);
                    //mailMessage.Body =
                    //mailMessage.IsBodyHtml = true;

                    // Set the sender email address and display name
                    mailMessage.From = new MailAddress("randyjus25@hotmail.com", "Nueva Contraseña");

                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        // Set the SMTP client credentials
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential("randyjus25@hotmail.com", "Codigo1234");

                        // Set the SMTP client properties
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.Host = "smtp.gmail.com";

                        // Send the email
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }

                // Return a success message
                return await Task.Run(() => "Email sent successfully!");
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further
            }
        }
    }
}

