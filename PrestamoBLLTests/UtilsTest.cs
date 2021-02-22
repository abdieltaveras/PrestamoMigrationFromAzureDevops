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
    public class UtilsTest
    {
        [TestMethod()]
        public void GetSvrDateTest()
        {
            var result = Utils.GetDateFromSqlServer();
            Assert.IsNotNull(result, "no pudo obtener la fecha");
        }
    }
}