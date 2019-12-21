namespace PrestamoEntidades
{
    public abstract class BaseGetParams : BaseUsuarioEIdNegocio
    {
        /// <summary>
        /// para indicar si desea o no registros borrados
        /// -1 todos, 0 los normales que no estan marcados como borrados y 1 los marcados como borrados
        /// </summary>
        public int Borrado { get; set; } = 0;
        
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

        public override string ToString()
        {
            return this.Nombres + " " + this.Apellidos;
        }

    }

}

