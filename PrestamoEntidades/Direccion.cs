using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{

    public class Localidad
    {
        public int IdLocalidad { get; set; } 
        public int IdLocalidadPadre { get; set; }
        public TipoLocalidad TipoLocalidad { get; set; } = TipoLocalidad.Continente;
        public string Nombre { get; set; } = string.Empty;
    }

    public class Direccion
    {
        public int IdDireccion {get;set;} 
        public int IdLocalidad { get; set; } 
        /// <summary>
        /// Nombre de la calle incluyendo el numero de la vivienda
        /// </summary>
        public string Calle { get; set; } = string.Empty;
        /// <summary>
        /// Para detallar la direccion esta proximo a la iglesia La Santidad
        /// Doblando por la banca GranPremio detras de la escuela Los Genios
        /// </summary>
        public string Detalles { get; set; } = string.Empty;
    }
}
