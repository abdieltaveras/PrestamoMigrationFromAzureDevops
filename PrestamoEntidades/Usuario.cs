using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
namespace PrestamoEntidades
{
    public class Usuario: BaseInsUpd
    {
        [Required]
        public int IdUsuario { get; set; } = 0;
        public bool Activo { get; set; } = true;
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [Display(Name ="Nombre Real")]
        public string NombreRealCompleto { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; } = string.Empty;
        [GuardarEncriptado]
        [MaxLength(50)]
        public string Contraseña { get; set; } = string.Empty;
        [EmailAddress]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]
        public string Telefono1 { get; set; } = string.Empty;
        public string Telefono2 { get; set; } = string.Empty;
        [Required]
        public bool Bloqueado { get; set; } = false;
        [Required]
        public bool EsEmpleado { get; set; } = false;
        public int IdPersonal { get; set; } = 0;
        [Required]
        public bool DebeCambiarContraseña { get; set; } = true;
        public DateTime? FechaExpiracionContraseña { get; set; }
    }

    public class UsuarioGetParams: BaseGetParams
    {
        public int IdUsuario { get; set; } = 0;
        public string LoginName { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public int Bloqueado { get; set; } = 0;
        public int Activo { get; set; } = 1;
        public int DebeCambiarContraseña { get; set; } = 0;
    }

    public class UsuarioAnularParam : BaseAnularParams
    { }
}
