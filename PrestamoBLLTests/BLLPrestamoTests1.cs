using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class BLLPrestamoTests1
    {
        [TestMethod()]
        public void GetOperacionesTest()
        {
            var response = BLLPrestamo.Instance.GetOperaciones(new PrestamoEntidades.UsuarioOperacionesGetParams() { IdUsuario = 2 });
            Assert.IsTrue(response.Count() > 0, "No retorno registros");

        }
    }
}