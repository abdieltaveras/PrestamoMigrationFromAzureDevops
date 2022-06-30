using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcpSoft.System;
using System.Diagnostics;


namespace PrestamoBLL.Tests
{
    [TestClass()]
    public class DivisionTerritotialTest
    {
        

        [TestMethod()]
        public void GetDivisionesTerritorial()
        {
            var result = BLLPrestamo.Instance.GetDivisionesTerritoriales(new DivisionTerritorialGetParams());
            Assert.IsTrue(result.Count() > 0, "no se econtro dato para la division territorial");
        }

        [TestMethod()]
        public void CreateDivisionTerritoriosTreeTest()
        {
            var divisionTerritorialComponents = getDivisionesTerritorialesComponents();
            TreeBuilder divisionTerritorialTree = null;
            IEnumerable<ITreeNode> treeNodes = null;
            string errorMessage = string.Empty;
            try
            {
                divisionTerritorialComponents.First().IdDivisionTerritorialPadre = 0; // esto es para hacerlo el nodo raiz
                var treeItems = divisionTerritorialComponents.Select(item =>
                                new TreeItem(item.IdDivisionTerritorial, item.IdDivisionTerritorialPadre, item.Nombre));
                divisionTerritorialTree = new TreeBuilder(treeItems);
                treeNodes = divisionTerritorialTree.GetTreeNodes();
                treeNodes.DisplayTo((e) => Trace.WriteLine(e));
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            //DivisionTerritoriales.First().IdLocalidadPadre = 0;
            Assert.IsTrue(string.IsNullOrEmpty(errorMessage), "no se pudo generar el tree para la division territorial");
        }

        private static IEnumerable<DivisionTerritorial> getDivisionesTerritorialesComponents()
        {
            var result = BLLPrestamo.Instance.GetTiposDivisonTerritorial("test");
            var idDivisionTerritorialSearchComponent = result.FirstOrDefault().IdDivisionTerritorial;
            var search = new DivisionTerritorialComponentsGetParams() { idDivisionTerritorial = result.FirstOrDefault().IdDivisionTerritorial };
            var DivisionTerritoriales = BLLPrestamo.Instance.GetDivisionTerritorialComponents(search);
            return DivisionTerritoriales;
        }

        [TestMethod()]
        public void GetTiposDivisonTerritorialTest()
        {
            var result = BLLPrestamo.Instance.GetTiposDivisonTerritorial("test");
            Assert.IsTrue(result != null, "por alguna razon no se ejecuto la busqueda") ;
        }
    }
}