using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class BLLPrestamoTests
    {
        /// <summary>
        /// Consulta mediante el Bll el objeto que almacena las tasas de interes
        /// </summary>
        [TestMethod()]
        
        public void GetTasaInteresTest()
        {
            var DatosObtenidos = BLLPrestamo.Instance.GetTasasInteres(new TasaInteresGetParams { IdNegocio=1});
            Assert.IsTrue(DatosObtenidos.Count() > 0);
        }
        /// <summary>
        /// insertar data mediante el BLL para el objeto TasaInteres
        /// </summary>
        [TestMethod()]
        public void insUpdTasaInteresTest()
        {

            var tasaInteres = new TasaInteres { Codigo = "B05", InteresMensual = 2.5f, Usuario="Abdiel", IdNegocio=1 };
            Exception error = null;
            try
            {
                BLLPrestamo.Instance.insUpdTasaInteres(tasaInteres);
            }
            catch (Exception e)
            {
                error = e;
            }
            Assert.IsTrue(error == null, error.Message);
        }
    }
}