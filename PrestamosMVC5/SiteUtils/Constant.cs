using PcpUtilidades;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

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
        public static string NoImagen => Utils.Constant.NoImagen;
        public static string Nuevo => "Nuevo";
        
        public static string prestamoDefaultInJson => getDefaultPrestamo().ToJson();

        public static Prestamo prestamoDefault => getDefaultPrestamo();

        private static Prestamo getDefaultPrestamo()
        {
            var prestamo = new Prestamo();
            return new Prestamo();
        }

        public static DateTime FechaHoy => DateTime.Now;
    }
    
}