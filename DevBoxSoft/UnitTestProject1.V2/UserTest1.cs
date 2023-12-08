using DevBox.Core.Access;
using DevBox.Core.BLL.Identity;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Action = DevBox.Core.Access.Action;
namespace UnitTestProject1.V2
{
    [TestClass]
    public class UserTest1
    {

        const string ActionXMLDiasFeriados = "<action id='{88AADDF0-9B98-45CA-90CD-ACC46152D407}' displayName='Días Feriados' description='Actualización Días Feriados' groupName='Configuración'  value='DíasFeriados' permissionLevel='Deny' />";

        const string ActionAndSubActionXMLForEstudiantes = @"<action id='{9498EF8F-4436-47F4-842A-C1CB1152DCE6}' displayName='Carnets' description='Asignaciones Carnets Estudiantes' groupName='Estudiantes'  value='AsignacionesCarnets' permissionLevel='Deny'>
    <subActions>
      <subAction displayName='Imprimir Carnet' description='Para la impresión de Carnets' value='ImprimirCarnet' permissionLevel='None' />
      <subAction displayName='Reimprimir Carnets' description='Reimprimir Carnets' value='ReImprimirCarnet' permissionLevel='None' />
      <subAction displayName='Eliminar Carnet' description='Permite eliminar Carnets' value='EliminarCarnet' permissionLevel='None' />
    </subActions>
  </action>";
        string ActionListXML = "";
        Guid SeventhGuid => Guid.Parse("{88AADDF0-9B98-45CA-90CD-ACC46152D407}");
        Guid SevenTeenthGuid => Guid.Parse("{9498EF8F-4436-47F4-842A-C1CB1152DCE6}");
        public UserTest1()
        {
            ActionListXML = File.ReadAllText("actionlist.xml");
        }
        [TestMethod]
        public void ActionCreation()
        {
            var actionXml = XElement.Parse(ActionXMLDiasFeriados);
            var action = new Action(actionXml);
            Assert.AreEqual(action.ID, SeventhGuid);
        }
        [TestMethod]
        public void ActionSubActionCreation()
        {
            var actionXml = XElement.Parse(ActionAndSubActionXMLForEstudiantes);
            var action = new Action(actionXml);
            Assert.AreEqual(action.ID, SevenTeenthGuid);
            Assert.AreEqual(action.SubActions.Count, 3);
            Assert.AreEqual(action.SubActions[0].DisplayName, "Imprimir Carnet");
            Assert.AreEqual(action.SubActions[1].Value, "ReImprimirCarnet");
            Assert.AreEqual(action.SubActions[2].Value, "EliminarCarnet");
        }
        [TestMethod]
        public void ActionListCreation()
        {
            var actionXml = XElement.Parse(ActionListXML);
            var actionList = new ActionList(actionXml);
            var actDiasFeriados = actionList["DíasFeriados"];
            Assert.AreEqual(actDiasFeriados.ID, SeventhGuid);
            var actCarnets = actionList["Carnets"];
            Assert.AreEqual(actCarnets.ID, SevenTeenthGuid);
            Assert.AreEqual(actCarnets.SubActions.Count, 3);
            Assert.AreEqual(actCarnets.SubActions[0].DisplayName, "Imprimir Carnet");
            Assert.AreEqual(actCarnets.SubActions[1].Value, "ReImprimirCarnet");
            Assert.AreEqual(actCarnets.SubActions[2].Value, "EliminarCarnet");
        }

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
                if (user==null)
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

            Assert.IsTrue(exceptionMessage == string.Empty, "no se hallaron acciones para el usuario guid, revise el error " + exceptionMessage);
        }

        private static List<CoreUser> GetAllUsers()
        {
            return new UsersManager().GetUsers(null, string.Empty, string.Empty, string.Empty, true);
        }
        [TestMethod()]
        public void ResetPasswordForUserFromBll()
        {

            new UsersManager().ResetUsrPass( Guid.Parse("12F1A9D7-2299-4706-BD4B-F7ABC1B138EB"), "limitado");

        }

        [TestMethod()]
        public void ExistEmailForOtherUser()
        {


            var allUsers = new UsersManager().GetAllUsersWithoutActions();
            var firstuser = allUsers.FirstOrDefault();
            var existingEmail = firstuser.Email;
            var firstuserWithNonExistingFirstUserName = firstuser;
            firstuserWithNonExistingFirstUserName.UserName = Guid.NewGuid().ToString();
            // Usuario nuevo, con un Email ya existente
            // resultado debe ser true
            var result1 = new UsersManager().ExistEmailForOtherUser(firstuserWithNonExistingFirstUserName);

            // Usuario nuevo, con un Email que no existe
            // resultado debe ser false
            var firstUserWithNonExistinEMail = firstuser;
            firstUserWithNonExistinEMail.Email = Guid.NewGuid().ToString() + "@email.com";
            
            var result2 = new UsersManager().ExistEmailForOtherUser(firstUserWithNonExistinEMail);

            // Usuario existente, con el mismo Email de el 
            // resultado debe ser false
            var result3 = new UsersManager().ExistEmailForOtherUser(firstuser);

            // Usuario existente, con el  Email de otro usuario que ya existe 
            // resultado debe ser true
            var user2 = allUsers.ToArray()[1];
            var existingOtherUserEmail = user2.Email;

            Assert.IsTrue(result1, "Fallo la condicion Usuario nuevo con un Email existente");
            Assert.IsFalse(result2, "Fallo la condicion Usuario nuevo con un Email que no existe");
            Assert.IsFalse(result3, "Fallo la condicion Usuario existente con su mismo Email");
        }

        [TestMethod()]
        public async Task Login()
        {
            LoginCredentials credentials = new LoginCredentials { Password = "pcp46232", UserName = "PcProg" };
            var result =  new UsersManager().ValidateCredentials(credentials, 1);
            
        }

        [TestMethod()]
        public void ExistNationalIdForOtherUser()
        {
            var allUsers = new UsersManager().GetAllUsersWithoutActions();
            
            Func<CoreUser, bool> condicion = user => !string.IsNullOrEmpty(user.NationalID) && !string.IsNullOrEmpty(user.Email); 
            var firstUser = allUsers.Where(condicion).FirstOrDefault();
            if (firstUser == null) 
            {
                Assert.Fail("La prueba no se puede realizar porque no existen usuarios creados o los que hay no tiene correo ni cedula registrada");
            }
            var newUserName = Guid.NewGuid().ToString();
            var testUser = firstUser;
            testUser.UserName = newUserName;
            // Usuario nuevo, con un NationalID ya existente
            // resultado debe ser true
            var NationaIDexistforOtherUser = new UsersManager().ExistNationalIDForOtherUser(testUser);

            // Usuario nuevo, con un NationalID que no existe
            // resultado debe ser false
            var NonExistingNationalId = Guid.NewGuid().ToString();
            testUser.NationalID = NonExistingNationalId;
            var NationalIdExist_2 = new UsersManager().ExistNationalIDForOtherUser(testUser);

            // Usuario existente, con el mismo NationalID de el 
            // resultado debe ser false
            var NationalIDExistForOtherUser_3 = new UsersManager().ExistNationalIDForOtherUser(firstUser);

            // Usuario existente, con el  NationalID de otro usuario que ya existe 
            // resultado debe ser true
            var user2 = allUsers.FirstOrDefault();
            var existingOtherUserNationalId = user2.NationalID;
            var NationalIDeExistForOtherUser4 = (user2.NationalID!=null && new UsersManager().ExistNationalIDForOtherUser(firstUser));

            Assert.IsTrue(NationaIDexistforOtherUser, "Fallo la condicion Usuario nuevo con un NationalID existente");
            Assert.IsFalse(NationalIdExist_2, "Fallo la condicion Usuario nuevo con un NationalID que no existe");
            Assert.IsFalse(NationalIDExistForOtherUser_3, "Fallo la condicion Usuario existente con su mismo NationalID");
            Assert.IsFalse(NationalIDeExistForOtherUser4, "Fallo la condicion Usuario existente con un NationalID de otro usuario");

        }

    }
}
