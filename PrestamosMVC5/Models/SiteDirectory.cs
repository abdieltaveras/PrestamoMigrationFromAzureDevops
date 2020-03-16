using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public static class SiteDirectory
    {
        /// <summary>
        /// place for custom icons files to be used at design time 
        /// </summary>
        public static string Icons => "~/Content/Site/icons";
        /// <summary>
        /// place for images files to be used on html pages at design times
        /// </summary>
        public static string Images => "~/content/build/images";
        public static string Gifs => "~/content/Site/Gifs";
        /// <summary>
        /// place for custom css designed to be used on html pages at design times
        /// </summary>
        public static string Css => "~/content/Site/css";
        /// <summary>
        /// Place for any kind of uploaded files by users that will be stored permanently
        /// that can be downloaded or needed at any time and can be deleted or updated the  user
        /// </summary>
        public static string Files => "~/content/Site/files";
    }


    /// <summary>
    /// to get paths for images directory for different info
    /// </summary>
    public static class ImagePath
    {
        /// <summary>
        /// receive a path and add at the beginning ~/ to return as a url path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string RemoveToUrl(this string path) => "~/" + path;
        public static string ForCliente => "~/content/ImagesFor/Clientes";
        public static string ForGarantia => "~/content/ImagesFor/Garantias";
    }
    
}