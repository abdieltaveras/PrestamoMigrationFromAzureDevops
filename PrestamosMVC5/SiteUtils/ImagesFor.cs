using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.SiteUtils
{
    /// <summary>
    ///  esta clase sera usada para dar los parametros de las imagenes a operar en una vista
    ///  hay un ejemplo que puede consultar en el controlador test
    /// </summary>
    public class ImagesFor
    {
        public string PropName { get; } = string.Empty;

        public string FriendlyName { get; } = string.Empty;

        public int Qty { get; set; } = 5;

        public ImagesFor(string propName, string friendlyName)
        {
            this.PropName = propName;
            this.FriendlyName = friendlyName;
        }
    }
}