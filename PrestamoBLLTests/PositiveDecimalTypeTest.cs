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
    public class PositiveDecimalTest
    {
        [TestMethod()]
        public void PositiveDecimalNotAcceptingNegativeValueTest()
        {
            int negativeValue = -30;
            var fail = TestUtil.ActionMustFail(() => { PositiveDecimal monto = negativeValue; });
            Assert.IsTrue(fail, $"el objeto PositiveDecimal no puede aceptar el valor {negativeValue} porque es negativo");
        }

        [TestMethod()]
        public void PositiveDecimalNotAcceptingNegativeOperationTest()
        {
            PositiveDecimal monto = 30;
            var fail = TestUtil.ActionMustFail(() => { monto = monto - 80; });
            Assert.IsTrue(fail, $"el objeto PositiveDecimal no puede aceptar la operacion monto = monto - 80");
        }

        [TestMethod()]
        public void EqualitySuccessTest()
        {
            PositiveDecimal expected = 30;
            PositiveDecimal monto = expected;
            var areEqual = monto == expected;
            Assert.IsTrue(areEqual, $"la comparacion de igualdad no fue exitosa");
        }
        [TestMethod()]
        public void EqualityFailTest()
        {
            decimal expected = 30;
            PositiveDecimal monto = 25;
            var areEqual = (monto == expected);
            Assert.IsTrue(!areEqual, $"la comparacion fallo porque es diferente y la detecto como igual");
        }
    }
}