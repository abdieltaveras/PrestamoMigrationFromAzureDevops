using DevBox.Core.DAL.SQLServer;
using System;

namespace PrestamoEntidades
{
    public class NombreTablasForCatalogos
    {
        public static string Ocupaciones => "tblOcupaciones";

        public static string VerificadorDireccion => "tblVerificadorDirecciones";

        public static string Telefonos => "tblTipoTelefonos";

        public static string TipoSexo => "tblTipoSexos";

        public static string Tasadores => "tblTasadores";
        public static string Localizadores => "tblLocalizadores";

        public static string EstadosCiviles => "tblEstadosCiviles";
        public static string Clasificaciones => "tblClasificaciones";
    }
    public abstract class BaseGetParams : BaseUsuarioEIdNegocio
    {
        /// <summary>
        /// para indicar si desea o no registros anulado
        /// -1 todos, 0 los normales que no estan marcados como anulado y 1 los marcados como anulados
        /// </summary>
        public int? Anulado { get; set; } = -1;
    }

    //public abstract class BaseCatalogoGetParams : BaseGetParams
    //{
    //    [IgnoreOnParams]
    //    public int reportType { get; set; } = -1;
    //    /// <summary>
    //    /// Then name of the column id example IdColor, IdClasificacion
    //    /// </summary>
    //    public string IdTabla { get; set; } = string.Empty;
    //    /// <summary>
    //    /// the Name of the table
    //    /// </summary>
    //    public string NombreTabla { get; set; } = string.Empty;

    //}

    public  class BaseCatalogoGetParams : BaseUsuarioEIdNegocio
    {
        public int IdRegistro { get; set; } = -1;
    }

    /// Informaciones basicas de un objeto que tiene datos relaciona a una persona pero sin Codigo
    /// se decidio crearle una concreta PersonaInfoBasicaSinCodigoGetParams
    /// para poder instanciarla en varios escenarios que compartiran todas las cosas que aqui tenemos 
    /// </summary>
    public abstract class BasePersonaGetParams
    {
        /// <summary>
        /// indica si esta o no activo, por defecto pone que sea true
        /// </summary>
        public int Activo { get; set; } = -1;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
    }

    public class BaseReporteParams
    {
        public int Opcion { get; set; } = 1;
        public string OrdenarPor { get; set; } = "Nombres";
        public string ODesde { get; set; } = "A";
        public string OHasta { get; set; } = "Z";
        public string Rango { get; set; } = "Nombres";
        public string RDesde { get; set; } = "A";
        public string RHasta { get; set; } = "Z";
        public int reportType { get; set; } = 1;
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}

