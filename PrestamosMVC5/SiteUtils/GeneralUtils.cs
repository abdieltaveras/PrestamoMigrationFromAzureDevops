
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.SiteUtils
{
    public static class GeneralUtils
     
    {
        /// <summary>
        /// Receives a fileName and if is it empty returns the savedFile name that could be empty
        /// </summary>
        /// <param name="imagen1ClienteFileName"></param>
        /// <param name="image1PreviewValue"></param>
        /// <param name="savedFileName"></param>
        /// <returns></returns>
        public static string  GetNameForFile(string imagen1ClienteFileName, string image1PreviewValue, string savedFileName)
        {
            string result = string.Empty;
            if (image1PreviewValue != Constant.NoImagen)
            {
                result = string.IsNullOrEmpty(imagen1ClienteFileName) ? savedFileName : imagen1ClienteFileName;
                
            }
            return string.IsNullOrEmpty(result) ? string.Empty: result;
        }
    }
}