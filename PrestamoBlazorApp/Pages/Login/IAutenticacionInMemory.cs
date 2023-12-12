using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Login
{
    public interface IAutenticacionInMemory
    {

        Task<bool> LoginAsync(string usuario, string contrasena);
        Task<bool> OlvideContrasenaAsync(string usuario);
        Task<string> GetCompaniaAsync();
        Task<List<string>> GetLocalidadesAsync();
    }

    public class AutenticacionInMemory : IAutenticacionInMemory
    {
        private readonly Dictionary<string, string> usuarios = new Dictionary<string, string>
    {
        {"usuario1", "contrasena1"},
        {"usuario2", "contrasena2"}
        // Agrega más usuarios según sea necesario
    };

        public async Task<bool> LoginAsync(string usuario, string contrasena)
        {
            if (usuarios.TryGetValue(usuario, out var storedContrasena))
            {
                return contrasena == storedContrasena;
            }

            return false;
        }

        public async Task<bool> OlvideContrasenaAsync(string usuario)
        {
            // Implementa la lógica para restablecer la contraseña según tus necesidades
            // Puede ser un proceso de envío de correo electrónico, mensajes, etc.
            return true;
        }

        public async Task<string> GetCompaniaoAsync()
        {
            // Implementa la lógica para obtener información del negocio
            return "InformacionDelNegocio";
        }

        public async Task<List<string>> GetLocalidadesAsync()
        {
            // Implementa la lógica para obtener la lista de localidades
            return new List<string> { "Localidad1", "Localidad2", "Localidad3" };
        }
    }

}
