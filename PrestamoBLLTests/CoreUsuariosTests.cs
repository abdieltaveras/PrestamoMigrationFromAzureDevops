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
        public void GetUserByNameFromBll()
        {
            try
            {
                var user = new UsersManager();
                var us = user.GetUser("abdiel");
                    //user.GetUser(new Guid("4dca1e76-e8e1-ec11-9ffc-00155e016707"));
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
            
            Assert.Fail();
        }
        [TestMethod()]
        public void GetUserByIdFromDb()
        {
            string exceptionMessage = string.Empty;
            
            var users = new UsersManager().GetUsers(null, string.Empty, string.Empty, string.Empty, true);

            var user = users.FirstOrDefault();
            
            try
            {
                user = new UsersManager().GetUser(user.UserID);
                var actions = (user.Actions ?? ActionList.Empty).Filter(ActionListFilters.Allowed);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            Assert.IsTrue(exceptionMessage!=string.Empty,"no se hallaron acciones para el usuario guid ");
        }

        [TestMethod()]
        public void LoginUserLimitado()
        {
            var user = new UsersManager().GetUser("limitado");

            Assert.Fail();
        }
        
    }
}