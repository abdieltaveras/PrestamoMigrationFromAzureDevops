using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class BLLPrestamoTests
    {
        [TestMethod()]
        public void GetTasaInteresTest()
        {
            var DatosObtenidos = BLLPrestamo.Instance.GetTasaInteres(new TasaInteresGetParams());

            Assert.IsTrue(DatosObtenidos.Count() > 0);
        }
    }
}