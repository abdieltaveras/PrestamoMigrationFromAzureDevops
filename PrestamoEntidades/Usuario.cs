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
        [Display(Name = "Nombre de Usuario para iniciar sesion")]
        public string LoginName { get; set; } = string.Empty;
        [GuardarEncriptado]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; } = string.Empty;
        [EmailAddress]
        [Display(Name = "Correo Electronico")]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Telefono No 1")]
        public string Telefono1 { get; set; } = string.Empty;
        [Display(Name = "Telefono No 2")]
        public string Telefono2 { get; set; } = string.Empty;
        [Required]
        public bool Bloqueado { get; set; } = false;
        [Required]
        public bool EsEmpleado { get; set; } = false;
        public int IdPersonal { get; set; } = 0;
        [Required]
        [Display(Name = "Cambiar Contraseña al iniciar sesion")]
        public bool DebeCambiarContraseñaAlIniciarSesion { get; set; } = true;
        [Display(Name = "Cuenta valida hasta la siguiente Fecha")]
        //todo: When this property is nullable the getProcedures returns null even when is not null

        public DateTime UsuarioValidoHasta { get; set; } = InitValues._19000101;

        public int ContrasenaExpiraCadaXMes { get; set; } = -1;

        public string ImgFilePath { get; set; } = string.Empty;
        /// <summary>
        /// Muestra en texto cada que tiempo expira la contrasena
        /// </summary>
        /// <returns></returns>
        public string ContrasenaExpiraCadaXMesToText()
        {
            var result = (this.ContrasenaExpiraCadaXMes <= 0) ? "Nunca" : $"Cada {this.ContrasenaExpiraCadaXMes} mes(es)";
            return result;
        }
        public bool LaContrasenaExpira() => this.ContrasenaExpiraCadaXMes > 0;
        
    }

    
    public class UsuarioGetParams: BaseGetParams
    {
        public int IdUsuario { get; set; } = -1;
        public string LoginName { get; set; } = string.Empty;
        public string NombreRealCompleto { get; set; } = string.Empty;
        
        public int Bloqueado { get; set; } = -1;
        public int Activo { get; set; } = -1;
        public int DebeCambiarContraseña { get; set; } = -1;
    }

    public class UsuarioAnularParam : BaseAnularParams
    { }
}
