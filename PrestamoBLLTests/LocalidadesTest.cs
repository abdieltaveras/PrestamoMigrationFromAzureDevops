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
    public class LocalidadesTest
    {
        [TestMethod()]
        public void GetLocalidadesTest()
        {
            var result = BLLPrestamo.Instance.GetLocalidades(new LocalidadGetParams()); 
            Assert.IsNotNull(result, "revisar porque no se retorno ningun valor");
        }
    }
}