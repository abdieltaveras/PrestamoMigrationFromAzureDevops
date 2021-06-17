using emtSoft.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PrestamoEntidades
{
    public interface IUsuario
    {
        string Usuario {get; set;}
    }
    /// <summary>
    /// esta clase solo contiene el campo Usuario 
    /// </summary>
    public abstract class BaseUsuario: IUsuario
    {
        /// <summary>
        /// El loginName del usuario que se logueo o se registro y que esta trabajando en el sistema
        /// </summary>
        public string Usuario { get; set; } = string.Empty;
    }
    /// <summary>
    /// Clase base que tiene el campo usuario y idNegocio
    /// </summary>
    public abstract class BaseUsuarioEIdNegocio : BaseUsuario, IIdNegocio
    {
        public int IdNegocio { get; set; } = -1;
        
        public int IdLocalidadNegocio { get; set; } = -1;
    }

    public interface IIdNegocio
    {
        int IdNegocio { get; set; }
    }
    public abstract class BaseIdNegocio : IIdNegocio
    {
        /// <summary>
        /// El id del negocio o empresa o punto comercial que identifica la unidad 
        /// comercial que opera en el sistema
        /// </summary>
        [Required]
        
        public int IdNegocio { get; set; } = -1;

        //todo add IdLocalidadNegocio to this base class to check with luis
    }

    
    /// <summary>
    /// Clase que tiene campos comunes que usaran la mayoria de las entidades que seran usadas
    /// para el registro de informacion como clientes, proveedores, direccion, etc.
    /// </summary>
    
    public abstract class BaseInsUpd : BaseUsuarioEIdNegocio
    {

        [IgnorarEnParam()]
        
        public string InsertadoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        
        public DateTime FechaInsertado { get;  set; } = InitValues._19000101;
        [IgnorarEnParam()]
        
        public string ModificadoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        
        public DateTime FechaModificado { get; set; } = InitValues._19000101;
        [IgnorarEnParam()]
        

        public string AnuladoPor { get;  set; } = string.Empty;
        [IgnorarEnParam()]
        
        public DateTime FechaAnulado { get;  set; } = InitValues._19000101;
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
        public int idSexo { get; set; } = (int)Sexo.Masculino;
    }

    public abstract class BaseCatalogo : BaseInsUpd
    {
        [Required]
        [Display(Name = "Estatus")]
        public bool Activo { get; set; } = true;
        [MaxLength(10)]
        public string Codigo { get; set; } 

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


