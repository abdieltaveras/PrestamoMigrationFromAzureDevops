using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    /// <summary>
    /// Objeto para enviar las informaciones relacionadas con anular registros
    /// </summary>
    public class AnulacionDeRegistro
    {
        /// <summary>
        /// el nombre de la tabla sobre la cual se va a ejecutar la actualizacion
        /// </summary>
        public string NombreTabla { get; set; } = string.Empty;
        /// <summary>
        /// El valor del id del registro  a borrar
        /// </summary>

        public int IdRegistroValor { get; set; }
        /// <summary>
        /// El nombre que tiene en la tabla el campo primaryKey puede ser IdCliente, IdPrestamo, IdUsuario, etc.
        /// </summary>
        public string IdRegistroNombreColumna { get; set; }

        public string Usuario { get; set; }
    }
}
