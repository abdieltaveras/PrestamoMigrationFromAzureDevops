using DevBox.Core.Classes.Utils;
using MudBlazor;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Login
{
    public interface IAutenticacionInMemory
    {

        Task<bool> LoginAsync(string usuario, string contrasena);
        Task<bool> LogoutAsync(string usuario, string contrasena);

        Task<bool> OlvideContrasenaAsync(string usuario);
        Task<string> GetCompaniaAsync();
        Task<List<string>> GetLocalidadesAsync();

    }

    public class AutenticacionInMemory : IAutenticacionInMemory
    {
        private readonly Dictionary<string, string> usuarios = new Dictionary<string, string>
    {
        {"CRISTAL", "1235"},
        {"Abdiel", "5678"}
        
    };

        public async Task<bool> LoginAsync(string usuario, string contrasena)
        {
            if (usuarios.TryGetValue(usuario, out var storedContrasena))
            {
                return contrasena == storedContrasena;
            }

            return false;
        }

        public async Task<bool> LogoutAsync(string usuario, string contrasena)
        {
            if (usuarios.TryGetValue(usuario, out var storedContrasena))
            {
                return contrasena == storedContrasena;
            }

            return false;
        }
      //  public async Task<string> OlvideContrasenaAsync(string usuario)
      //  {
           // try
           // {
              //  using (MailAddress mailMessage = new MailMessage())
               // {
                    //Destinatario
                 //   mailMessage.To.Add("cristaltaveras47@gmail.com");

                    //asunto
                 //   mailMessage.subject = "Recuperar contraseña";

                    //body
                 //   mailMessage.body = "<H1> se ha enviado correctamente.</h1>";
                  //  mailMessage.IsBodyHtml = true;

                    //remitente
                  //  mailMessage.GetFromDataRow = new MailAddress("randyjus25@hotmail.com", "Nueva Contraseña");

                 //   using (SmtpClient cliente = new SmtpClient())
                   // {
                        //contraseñas
                       // cliente.UseDefaultCredentials = false;
                       //     cliente.Credentials = new NetworkCredential("randyjus25@hotmail.com", "Codigo1234");
                       // cliente.Port = 587;
                       // cliente.EnableSsl = true;

                        //host

                      //  cliente.Host = "smtp.gmail.com";
                   //     cliente.Send(mailMessage);

                 //   }
//
             //   }
        //    }
          //  catch(Exception)
      //  }
          //  {
         //       throw;
         //   }
            

        public async Task<string> GetCompaniaAsync()
        {
            // Implementa la lógica para obtener información del negocio
            return "InformacionDelNegocio";
        }

        public async Task<List<string>> GetLocalidadesAsync()
        {
            // Implementa la lógica para obtener la lista de localidades
            return new List<string> { "Localidad1", "Localidad2", "Localidad3" };
        }

        public Task<bool> OlvideContrasenaAsync(string usuario)
        {
            throw new System.NotImplementedException();
        }
    }

}
