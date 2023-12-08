using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using DevBox.Core.BLL.Identity;
using DevBox.Core.Identity;
using DevBox.Core.Access;
using System.Diagnostics.CodeAnalysis;

namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class CoreUsuariosTests
    {
        [TestMethod()]
        public void GetUserByName()
        {
            string exceptionMessage = string.Empty;
            CoreUser user = null;
            try
            {
                var users = GetAllUsers();
                var firstUser = users.FirstOrDefault();
                user = new UsersManager().GetUser(firstUser.UserName);
                if (user == null)
                {
                    exceptionMessage = "algo fallo que no se pudo obtener el usuario por el nombre revisar";
                }
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            Assert.IsTrue(exceptionMessage == string.Empty, exceptionMessage);
        }
        [TestMethod()]
        public void GetFirstUserAndActions()
        {
            string exceptionMessage = string.Empty;
            
            try
            {
                var users = GetAllUsers();

                var firstUser = users.FirstOrDefault();
                if (firstUser != null)
                {
                    var actions = (firstUser.Actions ?? ActionList.Empty).Filter(ActionListFilters.Allowed);
                }
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            Assert.IsTrue(exceptionMessage==string.Empty,"no se hallaron acciones para el usuario guid, revise el error "+exceptionMessage);
        }

        private static List<CoreUser> GetAllUsers()
        {
            return new UsersManager().GetUsers(null, string.Empty, string.Empty, string.Empty, true);
        }

        [TestMethod()]
        public void LoginUserLimitado()
        {
            var user = new UsersManager().GetUser("limitado");

            Assert.Fail();
        }
        
    }
}