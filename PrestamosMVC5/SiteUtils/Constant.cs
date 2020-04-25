using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.SiteUtils
{
    /// <summary>
    /// define valores que se usaran como constantes para evitar errores de tipeados y otros
    /// </summary>

    public static class Constant
    {
        /// <summary>
        /// El valor para cuando se quiera establecer que no hay imagen 
        /// </summary>
        public static string NoImagen => PrestamoBLL.Utils.Constant.NoImagen;
    }
    
}