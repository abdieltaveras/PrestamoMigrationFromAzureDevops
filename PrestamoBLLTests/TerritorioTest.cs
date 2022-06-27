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
    public class TerritorioTest
    {
        [TestMethod()]
        public void TerritorioBuscarComponentesDivisionesTerritorialesTest()
        {
            var search = new DivisionSearchParams {IdDivisionTerritorialPadre=2, IdNegocio=1  };
            var result = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            Assert.IsTrue(result.Count()>0,"no se econtro dato para la division territorial");
        }

        [TestMethod()]
        public void GetDivisionesTerritorial()
        {
            var search = new DivisionSearchParams { IdDivisionTerritorialPadre = 2, IdNegocio = 1 };
            var result = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            var result2 = BLLPrestamo.Instance.GetDivisionesTerritoriales(new DivisionTerritorialGetParams());
            Assert.IsTrue(result.Count() > 0, "no se econtro dato para la division territorial");
        }

        [TestMethod()]
        public void DivisionTerritoriosTreeTest()
        {
            var search = new DivisionSearchParams { IdDivisionTerritorialPadre = 2, IdNegocio = 1 };
            var DivisionTerritoriales = BLLPrestamo.Instance.TerritorioBuscarComponentesDivisionesTerritoriales(search);
            var div2 = BLLPrestamo.Instance.GetDivisionesTerritoriales2(new DivisionTerritorialGetParams());
            TreeBuilder divisionTerritorialTree = null;
            IEnumerable<ITreeNode> treeNodes = null;
            string errorMessage = string.Empty;
            try
            {
                DivisionTerritoriales.First().IdDivisionTerritorialPadre=0; // esto es para hacerlo el nodo raiz
                var treeItems = DivisionTerritoriales.Select(item =>
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
        
    }
}