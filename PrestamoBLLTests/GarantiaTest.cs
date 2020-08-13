using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class GarantiaTest
    {
        [TestMethod()]
        public void GarantiaSearchConPrestamosTest()
        {
            var mensaje = string.Empty;
            IEnumerable<GarantiaConMarcaYModeloYPrestamos> result = null;
            try
            {
                result = BLLPrestamo.Instance.GarantiaSearchConPrestamos(new BuscarGarantiaParams { Search = "2" });
            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            Assert.IsTrue(result.FirstOrDefault().IdPrestamos.Count() > 0, "la consulta no obtuvo prestamo para la garantia");
        }

        [TestMethod()]
        public void GarantiaSearchTest()
        {
            var mensaje = string.Empty;
            IEnumerable<GarantiaConMarcaYModelo> result = null;
            try
            {
                result = BLLPrestamo.Instance.GarantiaSearch(new BuscarGarantiaParams { Search = "27" });
            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            Assert.IsTrue(result.Count() > 0, "la consulta no obtuvo resultados");
        }

        [TestMethod()]
        public void TienePrestamosVigentesTest()
        {
            //var tpIdGarantias = new List<tpIdGarantia>();
            //tpIdGarantias.Add(new tpIdGarantia { IdGarantia = 4 });
            //tpIdGarantias.Add(new tpIdGarantia { IdGarantia = 3 });
            var IdGarantias = new List<int> { 4, 3 };
            var result =BLLPrestamo.Instance.IdGarantiasTienenPrestamosVigentes(IdGarantias);
            var result2 = BLLPrestamo.Instance.IdGarantiasConPrestamos(IdGarantias);
            Assert.Fail();
        }
    }
}