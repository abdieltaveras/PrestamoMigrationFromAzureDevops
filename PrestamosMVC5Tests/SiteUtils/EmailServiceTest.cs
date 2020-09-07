using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamosMVC5.SiteUtils;

namespace PrestamosMVC5Tests.SiteUtils
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void TestSendMail()
        {
            var result = EmailService.SendMail("armpmartinez@gmail.com", "prueba", "Una prueba");

            Assert.IsTrue(result);
        }
    }
}
