using System;
using System.ComponentModel.DataAnnotations;
namespace PrestamoEntidades
{
    public class Usuario: BasePersonaInsUpd
    {
        [Required]
        public int IdUsuario { get; set; } = 0;
        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; } = string.Empty;
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;
        [Required]
        public string Telefono1 { get; set; } = string.Empty;
        public string Telefono2 { get; set; } = string.Empty;
        public bool Bloqueado { get; set; } = false;
        [Required]
        public bool EsEmpleado { get; set; } = false;
        [Required]
        public bool DebeCambiarContraseña { get; set; } = true;
        public DateTime FechaExpiracionClave { get; set; }

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }

    public class UsuarioGetParams
    {
        public int IdUsuario { get; set; } = 0;
        public int IdNegocio { get; set; } = 0;
        public string LoginName { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public bool Bloqueado { get; set; } = false;
        public bool Activo { get; set; } = true;
        public bool DebeCambiarContraseña { get; set; } = true;
    }
}
