using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using emtSoft.DAL;
namespace PrestamoEntidades
{
    public class Negocio : BaseInsUpd 
    {
        [Required] [MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;
        [NotMapped]
        public bool GenerarSecuencia { get; set; } = true;
        public int? IdNegocioPadre { get; set; } = null;
        [Required] [MaxLength(100)] [Display(Name ="Nombre Juridico/Legal")]    
        public string NombreJuridico { get; set; } = string.Empty;
        [Required][MaxLength(100)][Display(Name = "Nombre Comercial")]
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
    }

    public class NegociosGetParams : BaseGetParams
    {
        [MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;
        public int IdNegocioPadre { get; set; } = -1;
        
        public string NombreJuridico { get; set; } = string.Empty;
        
        public string NombreComercial { get; set; } = string.Empty;
        
        public string TaxIdNo { get; set; } = string.Empty;
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
        [MaxLength(100)]
        public string Logo { get; set; }
        [EmailAddress]
        public string CorreoElectronico { get; set; }
    }
}
