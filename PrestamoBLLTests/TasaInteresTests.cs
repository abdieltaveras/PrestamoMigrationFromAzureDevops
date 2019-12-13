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
            var result = BLLPrestamo.Instance.GetTasasInteres(new TasaInteresGetParams { IdNegocio = 1 });
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod()]

        public void GetInsertadoPorInfoTest()
        {
            var insInfo_ac = string.Empty;
            var result = BLLPrestamo.Instance.GetTasasInteres(new TasaInteresGetParams { IdNegocio = 1 });
            if (result != null)
            {
                insInfo_ac = result.First().InsertadoPor;
            }

            Assert.IsTrue(insInfo_ac!=string.Empty);
        }
        /// <summary>
        /// insertar data mediante el BLL para el objeto TasaInteres
        /// </summary>
        [TestMethod()]
        public void insUpdTasaInteresTest()
        {
            var error = new Exception();
            var OperacionExitosa = true;
            var tasaInteres = new TasaInteres { Codigo = "B05", InteresMensual = 2.5M, Usuario = "Abdiel", IdNegocio = 1 };
            var searchData = new TasaInteresGetParams { Codigo = "B05", IdNegocio = -1 };
            var result = BLLPrestamo.Instance.GetTasasInteres(searchData);
            if (result.Count() != 0)
            {
                tasaInteres.idTasaInteres = result.First().idTasaInteres;
            }
            try
            {
                BLLPrestamo.Instance.insUpdTasaInteres(tasaInteres);
            }
            catch (Exception e)
            {
                error = e;
                OperacionExitosa = false;

            }
            Assert.IsTrue(OperacionExitosa, error.Message);
        }

        

        [TestMethod()]
        public void insUpdClienteTest()
        {
            
        }
    }
}