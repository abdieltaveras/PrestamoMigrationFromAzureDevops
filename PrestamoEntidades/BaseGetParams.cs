using DevBox.Core.DAL.SQLServer;

namespace PrestamoEntidades
{
    public abstract class BaseGetParams : BaseUsuarioEIdNegocio
    {
        /// <summary>
        /// para indicar si desea o no registros anulado
        /// -1 todos, 0 los normales que no estan marcados como anulado y 1 los marcados como anulados
        /// </summary>
        public int Anulado { get; set; } = -1;
    }

    public class BaseCatalogoGetParams : BaseGetParams
    {
        /// <summary>
        /// Then name of the column id example IdColor, IdClasificacion
        /// </summary>
        public string IdTabla { get; set; } = string.Empty;
        /// <summary>
        /// the Name of the table
        /// </summary>
        public string NombreTabla { get; set; } = string.Empty;

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
}

