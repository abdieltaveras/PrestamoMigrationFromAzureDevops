using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public static class SiteImages
    {
        /// <summary>
        /// an image tha state that there is not image loaded
        /// </summary>
        public static string NoImage => SiteDirectory.Images + "/noimagen.png";
        public static string MainLogo => SiteDirectory.Images + "/mainlogo1.jpeg";
        public static string SecondaryLogo => SiteDirectory.Images + "/secondlogo.png";
        public static string PcpPrestamo => SiteDirectory.Images + "/pcpPrestamo.png";
        public static string Monica => SiteDirectory.Images + "/Monica.jpg";
        public static string PcpPrestamoYMonica => SiteDirectory.Images + "/Monica.jpg";

        public static string Carousel1 => SiteDirectory.Images + "/carousel1.png";
        public static string Carousel2 => SiteDirectory.Images + "/carousel2.jpg";
        public static string Carousel3 => SiteDirectory.Images + "/carousel3.jpg";
    }

}