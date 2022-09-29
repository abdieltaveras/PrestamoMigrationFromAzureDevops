using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    /// <summary>
    /// Clase del proyecto Test que tiene varianbles esenciales con informaciones para evitar tener que esribirlos 
    /// de esta manera siempre es congruente la informacion
    /// </summary>
    public class TestInfo
    {
        
        /// <summary>
        /// el nombre de usuario que envia este proyecto donde se necesite
        /// </summary>
        public static string Usuario => "PrestamoBllTestProject";
        public string _Usuario => Usuario;
        public string MensajeError { get; set; } = string.Empty;
    }
    
}
