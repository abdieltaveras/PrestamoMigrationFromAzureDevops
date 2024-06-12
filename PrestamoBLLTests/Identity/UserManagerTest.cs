using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevBox.Core.BLL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DevBox.Core.Classes.Identity;
using PrestamoBLL.Tests;


namespace DevBox.Core.BLL.Identity.Tests
{
    [TestClass()]
    public class UserManagerTest
    {
        [TestMethod()]
        public void ValidateCredentialsTest()
        {
            LoginCredentials credentials = new LoginCredentials { Password = "5892430", UserName = "pcprog", CompanyCode="C1" };
            var testUtils = new TestUtils();
            LoginResult result = new LoginResult();
            TestUtils.TryCatch(() =>
            {

                result = new UsersManager().ValidateCredentials(credentials, 10);

            }, out testUtils);

            
            Assert.IsTrue(result.Token != string.Empty, "no se pudo validar la credencial ");
        }
    }
}