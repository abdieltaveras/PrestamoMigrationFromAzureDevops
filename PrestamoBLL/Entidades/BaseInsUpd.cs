using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PrestamoBLL.Entidades
{
    /// <summary>
    /// esta clase solo contiene el campo Usuario 
    /// </summary>
    public abstract class BaseUsuario
    {
        /// <summary>
        /// El loginName del usuario que se logueo o se registro y que esta trabajando en el sistema
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        [NotMapped]
        public string Usuario { get; set; } = string.Empty;
    }
    /// <summary>
    /// Clase base que tiene el campo usuario y idNegocio
    /// </summary>
    public abstract class BaseUsuarioEIdNegocio : BaseUsuario
    {
        public int IdNegocio { get; set; } = -1; 
    }

    public abstract class BaseIdNegocio
    {
        /// <summary>
        /// El id del negocio o empresa o punto comercial que identifica la unidad 
        /// comercial que opera en el sistema
        /// </summary>
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int IdNegocio { get; set; } = -1;
    }

    public static class InitValues
    {
        public static DateTime _19000101 => new DateTime(1900, 01, 01);
    }
    /// <summary>
    /// Clase que tiene campos comunes que usaran la mayoria de las entidades que seran usadas
    /// para el registro de informacion como clientes, proveedores, direccion, etc.
    /// </summary>
    
    public abstract class BaseInsUpd : BaseUsuarioEIdNegocio
    {

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
        public bool Anulado() => !string.IsNullOrEmpty(AnuladoPor);  
        public virtual bool Modificable() => false;
        public virtual bool Anulable()=> false;
        /// <summary>
        /// para obtener el valor del id primario del objeto
        /// </summary>
        /// <param name="_id"></param>
        
    }
    /// <summary>
    /// clase basica para generales de una persona o entidad afin
    /// </summary>
    public abstract class BasePersonaInsUpd : BaseInsUpd
    {
        [Required]
        public bool Activo { get; set; } = true;
        [Required(ErrorMessage = "ingrese informacion en {0} ")]
        public string Nombres { get; set; } = string.Empty;
        [Required(ErrorMessage = "ingrese informacion en  {0} ")]
        public string Apellidos { get; set; } = string.Empty;
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Apodo { get; set; } = string.Empty;
        public int Sexo { get; set; } = 1;
    }

    public abstract class BaseCatalogo : BaseInsUpd
    {
        [Required]
        [Display(Name = "Estatus")]
        public bool Activo { get; set; } = true;
        [MaxLength(10)]
        [Required(ErrorMessage = "ingrese el codigo ")]
        public string Codigo { get; set; } = string.Empty;
        [Required(ErrorMessage = "ingrese el nombre")]
        public string Nombre { get; set; } = string.Empty;
        public abstract int GetId();
        //public string Descripcion { get; set; } = string.Empty;
    }
    public abstract class BaseDireccion : BaseInsUpd
    {
        public string Pais { get; set; } = string.Empty;
        public string Estado_o_Provincia { get; set; } = string.Empty;
        public string Ciudad_Municipio { get; set; } = string.Empty;
        public string SectorPrincipal { get; set; } = string.Empty;
        public string SectorSecundario { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
    }
}


