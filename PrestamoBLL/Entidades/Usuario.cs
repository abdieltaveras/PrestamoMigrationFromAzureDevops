using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PrestamoBLL.Entidades
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
        [RegularExpression(@"^\S*$", ErrorMessage = "No se permiten espacios en blanco")]
        public string LoginName { get; set; } = string.Empty;
        [GuardarEncriptado]
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
        [Display(Name = "Cuenta vigente Desde")]
        

        public DateTime VigenteDesde { get; set; } = InitValues._19000101;
        /// <summary>
        /// establece si hay una fecha limite de vigencia de la cuenta
        /// </summary>
        [Display(Name = "Cuenta vigente hasta")]

        

        public DateTime VigenteHasta { get; set; } = InitValues._19000101;
        /// <summary>
        /// Indica la fecha cuando inicia la vigencia de la contrasena
        /// para asi determinar si esta vencida segun los meses que se
        /// indica que la misma vencera
        /// </summary>

        public DateTime InicioVigenciaContraseña { get; set; } = InitValues._19000101;
        [Display(Name = "Contraseña Expira")]
        public int ContraseñaExpiraCadaXMes { get; set; } = -1;

        public string ImgFilePath { get; set; } = string.Empty;
        /// <summary>
        /// Muestra en texto cada que tiempo expira la contrasena
        /// </summary>
        /// <returns></returns>
        public string ContraseñaExpiraCadaXMesToText()
        {
            var result = (this.ContraseñaExpiraCadaXMes <= 0) ? "Nunca" : $"Cada {this.ContraseñaExpiraCadaXMes} mes(es)";
            return result;
        }
        public bool LaContrasenaExpira() => this.ContraseñaExpiraCadaXMes > 0;
        public bool LimitarVigenciaDeLaCuenta() => (this.VigenteHasta != InitValues._19000101);
        public int RazonBloqueo { get; set; } =-1;
        [IgnorarEnParam]
        /// <summary>
        ///  es el id del negocio raiz que define todas las ramificaciones
        /// </summary>
        public int IdNegocioMatriz { get; set; }

        [IgnorarEnParam]
        /// <summary>
        ///  esta propiedad unicamente se usa para el stored procedure retornar la validez de la contraseña
        /// </summary>
        public bool ContraseñaValida { get; internal set; } = false;
    }

    public enum RazonBloqueo {ContraseñaMalDigitada, PorAdministracionDeUsuario, PorOtroSistema }
    public class UsuarioGetParams: BaseGetParams
    {
        public int IdUsuario { get; set; } = -1;
        public string LoginName { get; set; } = string.Empty;
        public string NombreRealCompleto { get; set; } = string.Empty;
        
        public int Bloqueado { get; set; } = -1;
        public int Activo { get; set; } = -1;
        public int DebeCambiarContraseñaAlIniciarSesion { get; set; } = -1;
    }

    public class ChangePassword: BaseUsuario
    {
        public int IdUsuario { get; set; }
        [GuardarEncriptado]
        public string Contraseña { get; set; } = string.Empty;
    }
    public class UsuarioAnularParam : BaseAnularParams
    { }

    //public class UserRoleInsUpdParams
    //{
    //    public int IdUser { get; set; } = -1;
    //    public string Values { get; set; } = string.Empty;
    //}
    public class UsuarioRole
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }

        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string InsertadoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaInsertado { get; set; } = InitValues._19000101;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string ModificadoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaModificado { get; set; } = InitValues._19000101;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string AnuladoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaAnulado { get; set; } = InitValues._19000101;
        public bool Anulado() => string.IsNullOrEmpty(AnuladoPor);
    }
    public class UsuarioRoleIns
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
    }

    public class UsuarioOperacionesGetParams
    {
        public int IdUsuario { get; set; }
    }
}
