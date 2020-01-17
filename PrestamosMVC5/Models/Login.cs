using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }

    public class CustomSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> RoleName { get; set; }

    }

    public class UserModel
    {
        public Usuario Usuario { get; set; } = new Usuario();
        [Required]
        public Guid ActivationCode { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Valor requerido no puede estar vacio")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("Contraseña", ErrorMessage = "Error : Las confirmaciones de contraseñas no coinciden o estan vacias")]
        public string ConfirmarContraseña { get; set; }
        [Display(Name = "La Contraseña Expira ?")]
        public bool LaContraseñaExpira { get; set; }
        [Display(Name ="Limitar vigencia de esta cuenta")]
        public bool LimitarVigenciaDeCuenta { get; set; }
        [Display(Name = "La Contraseña Expira Cada")]
        [Range(maximum:12, minimum:1,ErrorMessage ="Solo se aceptan valores entre 1  y 12")]
        [Required]
        public int ContraseñaExpiraCadaXMes { get; set; } = 1;
    }
    
}