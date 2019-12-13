using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades.Tests
{
    [TestClass()]
    public class DireccionTests
    {
        [TestMethod]
        public void TestEnumIntValuesWhenFirstIsSetToOne()
        {
            int resultadoEsperado = 1;
            int resultadoObtenido = (int)TiposCargosMora.Cargo_Fijo;
            Assert.IsTrue(resultadoEsperado == resultadoObtenido);
        }
    }
}