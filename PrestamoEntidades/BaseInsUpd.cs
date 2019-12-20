using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace PrestamoEntidades
{

    /// <summary>
    /// para que los objetos especifiquen si generaran o no la secuencia automatica 
    /// </summary>
    public interface ISecuenciable
    {
        [Required]
        [NotMapped]
        [HiddenInput(DisplayValue = false)]
        bool GenerarSecuencia { get; set; }
    }

    public abstract class BaseUsuario
    {
        /// <summary>
        /// El loginName del usuario que se logueo o se registro y que esta trabajando en el sistema
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        [NotMapped]
        public string Usuario { get; set; } //= string.Empty;/// 
    }

    public abstract class BaseUsuarioEIdNegocio : BaseUsuario
    {
        /// <summary>
        /// El id del negocio o empresa o punto comercial que identifica la unidad 
        /// comercial que opera en el sistema
        /// </summary>
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int IdNegocio { get; set; } = 0; // dejarlo asi para que sea obligatorio inicializar el 
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
        public DateTime FechaInsertado { get; set; } = new DateTime(1900, 1, 1);
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string ModificadoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaModificado { get; set; } = new DateTime(1900, 1, 1);
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string AnuladoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaAnulado { get; set; } = new DateTime(1900, 1, 1);
        
        //[NotMapped]
        //[IgnorarEnParam]
        public bool Borrado() => string.IsNullOrEmpty(AnuladoPor);  

        //[NotMapped]
        //[IgnorarEnParam]
        public virtual bool Modificable() => false;
        //[NotMapped]
        //[IgnorarEnParam]
        public virtual bool Anulable()=> false;
        /// <summary>
        /// para obtener el valor del id primario del objeto
        /// </summary>
        /// <param name="_id"></param>
        public abstract int GetId();
    }
    /// <summary>
    /// clase basica para datos sobre Personas pero que no lleva el campo Codigo, suele usarse
    /// en aquellos catalogos que por compania no superaran los 100 registros, y es mas comodo para 
    /// el usuario tomarlo de una lista
    /// </summary>
    public abstract class BasePersonaInsUpd : BaseInsUpd
    {
        [Required]
        public bool Activo { get; set; } = true;
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Nombres { get; set; } = string.Empty;
        [Required(ErrorMessage = "el campo {0} es requerido")]
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
        [Required(ErrorMessage = "el campo {0} es requerido")]
        public string Codigo { get; set; } = string.Empty;
        [Required(ErrorMessage = "el campo {0} es requerido")]       
        public string Descripcion { get; set; } = string.Empty;
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


