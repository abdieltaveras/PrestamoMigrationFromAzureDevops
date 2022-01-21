using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoEntidades;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class ClasificacionTests
    {
        [TestMethod()]
        public void ClasificacionQueRequierenGarantias_SuccesfullExecution()
        {
            bool succesResult = true;
            try
            {
                var result = BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(1);
            }
            catch (Exception)
            {

                succesResult = false;
            }
            
            Assert.IsTrue(succesResult,"el procedimiento no pudo ser ejecutado");
        }

        [TestMethod()]
        public void GetClasificaciones()
        {
            bool succesResult = true;
            var searchParam = new ClasificacionesGetParams();
            try
            {
                
                var result = BLLPrestamo.Instance.GetClasificaciones(searchParam);
            }
            catch (Exception)
            {
                succesResult = false;
            }

            Assert.IsTrue(succesResult, "No se pudo ejecutar el procedimiento de busqueda");
        }

    }
}