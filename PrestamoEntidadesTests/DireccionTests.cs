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
            int resultadoEsperado = 4;
            int resultadoObtenido= (int)TipoLocalidad.Municipio;
            Assert.IsTrue(resultadoEsperado == resultadoObtenido);
        }
    }
}