using DevBox.Core.Access;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Xml.Linq;
using Action = DevBox.Core.Access.Action;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        const string ActionXML = "<action id='{88AADDF0-9B98-45CA-90CD-ACC46152D407}' displayName='Días Feriados' description='Actualización Días Feriados' groupName='Configuración'  value='DíasFeriados' permissionLevel='Deny' />";
        const string SubActionXML = @"<action id='{9498EF8F-4436-47F4-842A-C1CB1152DCE6}' displayName='Carnets' description='Asignaciones Carnets Estudiantes' groupName='Estudiantes'  value='AsignacionesCarnets' permissionLevel='Deny'>
    <subActions>
      <subAction displayName='Imprimir Carnet' description='Para la impresión de Carnets' value='ImprimirCarnet' permissionLevel='None' />
      <subAction displayName='Reimprimir Carnets' description='Reimprimir Carnets' value='ReImprimirCarnet' permissionLevel='None' />
      <subAction displayName='Eliminar Carnet' description='Permite eliminar Carnets' value='EliminarCarnet' permissionLevel='None' />
    </subActions>
  </action>";
        string ActionListXML = "";
        Guid SeventhGuid => Guid.Parse("{88AADDF0-9B98-45CA-90CD-ACC46152D407}");
        Guid SevenTeenthGuid => Guid.Parse("{9498EF8F-4436-47F4-842A-C1CB1152DCE6}");
        public UnitTest1()
        {
            ActionListXML = File.ReadAllText("actionlist.xml");
        }
        [TestMethod]
        public void ActionCreation()
        {
            var actionXml = XElement.Parse(ActionXML);
            var action = new Action(actionXml);
            Assert.AreEqual(action.ID, SeventhGuid);
        }
        [TestMethod]
        public void ActionSubActionCreation()
        {
            var actionXml = XElement.Parse(SubActionXML);
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
            var actDiasFeriados = actionList["Días Feriados"];
            Assert.AreEqual(actDiasFeriados.ID, SeventhGuid);


            var actCarnets = actionList["Carnets"];
            Assert.AreEqual(actCarnets.ID, SevenTeenthGuid);
            Assert.AreEqual(actCarnets.SubActions.Count, 3);
            Assert.AreEqual(actCarnets.SubActions[0].DisplayName, "Imprimir Carnet");
            Assert.AreEqual(actCarnets.SubActions[1].Value, "ReImprimirCarnet");
            Assert.AreEqual(actCarnets.SubActions[2].Value, "EliminarCarnet");
        }
    }
}
