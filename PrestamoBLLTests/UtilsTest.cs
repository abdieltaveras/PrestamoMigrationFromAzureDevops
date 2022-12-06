using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NullGuard;
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
    public class UtilsTest
    {
        [TestMethod()]
        public void GetSvrDateTest()
        {
            //var result = UtilsBLL.GetDateFromSqlServer();
            //Assert.IsNotNull(result, "no pudo obtener la fecha");
        }

        [TestMethod()]
        public void FailWhenNullParamsTest()
        {
            //var result = UtilsBLL.GetDateFromSqlServer();
            //Assert.IsNotNull(result, "no pudo obtener la fecha");
            DisallowNullParam(null);
            allowNullParam(null);
            Assert.Fail("Prueba Exitosa");
        }


        private void allowNullParam(Cliente? cliente) { }
        private void DisallowNullParam([NotNull] Cliente cliente) { }


    }
}