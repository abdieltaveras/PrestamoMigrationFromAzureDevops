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
            var result = EmailService.SendMail("AbdielTaveras@gmail.com", "prueba", "mensaje para cristian como usar obs");

            Assert.IsTrue(result,"el correo no pudo ser enviado");
        }
    }
}
