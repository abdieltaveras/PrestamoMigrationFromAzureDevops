using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.SiteUtils
{
    public static class SiteImages
    {
        /// <summary>
        /// an image tha state that there is not image loaded
        /// </summary>
        public static string NoImage => SiteDirectory.SiteImages + "/noimagen.png";
        public static string MainLogo => SiteDirectory.SiteImages + "/mainlogo1.jpeg";
        public static string Demo => SiteDirectory.SiteImages + "/demo.png";
        public static string NegocioLogoConLetra => AuthInSession.GetNegocioLogo();
        public static string AppLogoConLetra => SiteDirectory.SiteImages  + "/pcpPrestamo.png";
        public static string AppLogoSinLetra => SiteDirectory.SiteImages + "/iconoPcpPrestamo-sin-letras.png";
        public static string PcProgLogo => SiteDirectory.SiteImages + "/pcpLogo250x211.jpg";
        public static string Carousel1 => SiteDirectory.SiteImages + "/carousel1.png";
        public static string Carousel2 => SiteDirectory.SiteImages + "/carousel2.jpg";
        public static string Carousel3 => SiteDirectory.SiteImages + "/carousel3.jpg";
    }

}