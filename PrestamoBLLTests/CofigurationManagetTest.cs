using System.Configuration;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class CofigurationManagetTest
    {
        [TestMethod()]

        public void GetConectionsStrings()
        {
            string path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;

            var connectionsSetting = ConfigurationManager.ConnectionStrings;
            BLLPrestamo.Instance.GetPeriodos(new PeriodoGetParams());

            Assert.IsTrue(connectionsSetting.Count > 1, "No esta funciondo el poder conseguir las cadenas de coneccion del app.config");
        }
    }
}
