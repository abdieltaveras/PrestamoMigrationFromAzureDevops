using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NullGuard;
using PcpUtilidades;
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
        public void FillZero()
        {
            var result = Utils.FillZerosLeftNumber(10, 56);
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

        [TestMethod()]
        public void DerivedTypePropertieAffectBaseTypeTest()
        {
            var instance = new DerivedType();
            instance.Nombre = "abdiel";

            var result = instance as BaseType;
            var value = result.Nombre;
        }

        internal class BaseType { 

            internal virtual string? Nombre { get; set; }
        }

        internal class DerivedType: BaseType
        { 
            internal override string? Nombre { get; set; }
        }

        private void allowNullParam(Cliente? cliente) { }
        private void DisallowNullParam([NotNull] Cliente cliente) { }


    }
}