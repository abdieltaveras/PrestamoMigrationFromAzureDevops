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
    public class TestUtils
    {
        
        /// <summary>
        /// el nombre de usuario que envia este proyecto donde se necesite
        /// </summary>
        internal static string Usuario => "PrestamoBllTestProject";
        internal static int GetIdLocalidadNegocio()
        {
            return 1;
        }

        internal static int GetIdNegocio()
        {
            return 1;
        }
        internal string MensajeError { get; set; } = string.Empty;
        internal Exception ExceptionOccured { get; set; } = null;

        internal static void TryCatch(Action action, out TestUtils testInfo)
        {
            testInfo = new TestUtils();
            try
            {
                action();
            }
            catch (Exception e)
            {
                testInfo.MensajeError = e.Message;
                testInfo.ExceptionOccured = e;
            }
        }
        public static bool ActionMustFail(Action action)
        {
            return (ActionMustSucceed(action) == false);
        }

        public static bool ActionMustSucceed(Action action)
        {
            bool successFullOperation = true;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                successFullOperation = false;
            }
            return successFullOperation == true;
        }
    }
    
}
