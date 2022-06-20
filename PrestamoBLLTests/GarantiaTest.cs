using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PrestamoBLL;
namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class GarantiaTest
    {
        PrestamoBLL.BLLPrestamo bLLPrestamo = new BLLPrestamo();
        PrestamoBLL.BLLPrestamo.BllAcciones bllAcciones = new BLLPrestamo.BllAcciones();

        [TestMethod()]
        public void GarantiaSearchConPrestamosTest()
        {
            var mensaje = string.Empty;
            IEnumerable<GarantiaConMarcaYModeloYPrestamos> result = null;
            try
            {
                result = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(new BuscarGarantiaParams { Search = "2" });
            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            Assert.IsTrue(result.FirstOrDefault().IdPrestamos.Count() > 0, "la consulta no obtuvo prestamo para la garantia");
        }

        [TestMethod()]
        public void GarantiaSearchsTest()
        {
            //var cn = ConfigurationManager.ConnectionStrings[""]
            GarantiaGetParams searchParam = new GarantiaGetParams();
            var mensaje = string.Empty;
            IEnumerable<GarantiaConMarcaYModeloYPrestamos> result = null;
            try
            {
                 result = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(new BuscarGarantiaParams { Search = "2" });
            }
            catch (Exception e)
            {
                mensaje = e.Message;
            }
            Assert.IsTrue(result.Count() > 0, "la consulta no obtuvo resultados");
        }

        [TestMethod()]
        public void GarantiaSearchTest()
        {
            var mensaje = string.Empty;
            IEnumerable<GarantiaConMarcaYModelo> result = null;
            try
            {
                result = BLLPrestamo.Instance.SearchGarantias("27");
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
            var result =BLLPrestamo.Instance.GarantiasTienenPrestamosVigentes(IdGarantias);
            var result2 = BLLPrestamo.Instance.GarantiasConPrestamos(IdGarantias);
            Assert.Fail();
        }
    }
}