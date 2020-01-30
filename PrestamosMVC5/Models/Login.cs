using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class LoginModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int IdNegocio { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string LoginName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;
    
    }

    
    public class ChangePasswordModel
    {
        public string LoginName { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]

        public string Contraseña { get; set; } = string.Empty;
        [Required(ErrorMessage = "Valor requerido no puede estar vacio")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("Contraseña", ErrorMessage = "Error : Las confirmaciones de contraseñas no coinciden o estan vacias")]
        public string ConfirmarContraseña { get; set; } = string.Empty;
    }

    public class UserModel
    {
        public Usuario Usuario { get; set; }= new Usuario();
        [Required]
        public Guid ActivationCode { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]

        public string Contraseña { get; set; } = string.Empty;

        [Required(ErrorMessage = "Valor requerido no puede estar vacio")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("Contraseña", ErrorMessage = "Error : Las confirmaciones de contraseñas no coinciden o estan vacias")]
        public string ConfirmarContraseña { get; set; } = string.Empty;
        [Display(Name = "La Contraseña Expira ?")]
        public bool LaContraseñaExpira { get; set; } = false;
        [Display(Name = "Limitar vigencia de esta cuenta")]
        public bool LimitarVigenciaDeCuenta { get; set; } = false;
        [Display(Name = "La Contraseña Expira Cada")]
        //[Range(maximum:12, minimum:1,ErrorMessage ="Solo se aceptan valores entre 1  y 12")]
        //[Required]
        public int ContraseñaExpiraCadaXMes { get; set; } = 1;

        public bool ForActivo { get; set; } 
        public bool ForBloqueado { get; set; } 
        public bool ForCambiarContraseñaAlIniciarSesion { get; set; } 

        public bool ShowAdvancedOptions { get; set; } = false;
        public UserModel()
        {
            var usuario = new Usuario();
            this.ForActivo = usuario.Activo;
            this.ForBloqueado = usuario.Bloqueado;
            this.ForCambiarContraseñaAlIniciarSesion = usuario.DebeCambiarContraseñaAlIniciarSesion;
        }
    }
    
}