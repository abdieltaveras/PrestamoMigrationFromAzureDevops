using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSPrestamo.Utilidades
{
    public static class DirectorioDeImagenes
    {
        public static string ParaClientes => HttpContext.Current.Server.MapPath("~/Content/ImagesFor/Clientes/");

        public static string ParaGarantias => HttpContext.Current.Server.MapPath("~/Content/ImagesFor/Garantias/");
    }
}