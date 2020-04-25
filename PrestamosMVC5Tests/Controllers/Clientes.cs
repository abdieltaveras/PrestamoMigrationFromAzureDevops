using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamosMVC5.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.Utils;

namespace PrestamosMVC5.ControllersTests
{
    [TestClass()]
    public class Clientes
    {
        [TestMethod()]
        public void GetNameForFile_noImagen()
        {
            var controller = new ClientesController();
            var result = controller.GetNameForFile("1", Constant.NoImagen, "3");
            Assert.IsTrue(string.IsNullOrEmpty(result), $"se esperaba un string vacio y se obtuvo {result}");

        }
    }
}