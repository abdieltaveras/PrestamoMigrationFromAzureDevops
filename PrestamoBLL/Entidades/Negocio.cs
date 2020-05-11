using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using emtSoft.DAL;
namespace PrestamoBLL.Entidades
{
    public class Negocio : BaseInsUpd 
    {
        [Required] [MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;
        //ATTENTION NO PONER CAMPOS NULLABLE, lo que hace el database es que si esta nulo no pone ningun valor lo deja en su valor por defecto que crea el constructor
        [NotMapped]
        public int IdNegocioPadre { get; set; } = -1;
        [Required(ErrorMessage ="ingrese el nombre Juridico del negocio")] [MaxLength(100)] [Display(Name ="Nombre Juridico/Legal")]    
        public string NombreJuridico { get; set; } = string.Empty;
        [Required(ErrorMessage = "ingrese el nombre comercial ")][MaxLength(100)][Display(Name = "Nombre Comercial")]
        public string NombreComercial { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public bool Bloqueado { get; set; } = false;
        [EmailAddress(ErrorMessage = "correo electronico invalido")] [Display(Name = "Correo Electronico")]
        public string CorreoElectronico { get; set; } = string.Empty;
        [Required]  [MaxLength(20)] [Display(Name = "No Identificacion Impositiva ")]
        public string TaxIdNo { get; set; } = string.Empty;
        [Display(Name = "Otros Detalles")]
        /// <summary>
        /// Detalles adicionales InfoNegocio
        /// </summary>
        public string OtrosDetalles { get; set; } = string.Empty;
        [NotMapped]
        public string InfoAccion { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Logo { get; set; }
        public bool PermitirOperaciones { get; set; }
        [IgnorarEnParam]
        public int IdNegocioMatriz { get; set; } = 0 ;
    }

    public class NegociosGetParams : BaseGetParams
    {
        [MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;
        public int IdNegocioPadre { get; set; } = -1;
        
        public string NombreJuridico { get; set; } = string.Empty;
        
        public string NombreComercial { get; set; } = string.Empty;
        
        public string TaxIdNo { get; set; } = string.Empty;

        public int PermitirOperaciones { get; set; } = -1;

        [IgnorarEnParam]
        public int IdNegocioMatriz { get; internal set; } = 0;
    }

    /// <summary>
    /// otras informaciones relacionadas al negocio
    /// </summary>
    public class NegocioInfo
    {

        [MaxLength(100)]
        public string Direccion1 { get; set; }
        [MaxLength(100)]
        public string Direccion2 { get; set; }
        [MaxLength(20)]
        [Phone]
        public string Telefono1 { get; set; }
        [MaxLength(20)]
        [Phone]
        public string Telefono2 { get; set; }
        
        
    }
}
