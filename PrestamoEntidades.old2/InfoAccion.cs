using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    /// <summary>
    /// Para recaudar informacion de quien realiza una accion
    /// </summary>
    public class InfoAccion
    {
        /// <summary>
        ///  el Id del negocio que esta realizacion la accion
        /// </summary>
        public int IdLocalidadNegocio { get; set; }
        /// <summary>
        /// La Fecha en que se realiza la accion
        /// </summary>
        [IgnorarEnParam]
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// El id del Usuario
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// El id que identifica al dispositivo/equipo
        /// </summary>
        public int IdDispositivo { get; set; }
        /// <summary>
        /// El id De la aplicacion
        /// </summary>
        public int IdAplicacion { get; set; }
        /// <summary>
        /// El id De la localidad
        /// </summary>
        public int IdLocalidad { get; set; }
    }
}
